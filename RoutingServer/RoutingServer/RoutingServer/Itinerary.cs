using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Device.Location;
using RoutingServer.ProxyCachCall;
using System.Linq;


/**
 * @author Ayoub IMAMI
 */

namespace RoutingServer
{
    public class Itinerary : IItinerary
    {

        OpenRouteServiceCall openRouteServiceCall = new OpenRouteServiceCall(); // make calls to OpenRouteService API
        ProxyClient proxyClient = new ProxyClient(); // make calls to the ProxyCache
        Deserializer deserializer = new Deserializer(); // deserialize jsons
        ActiveMQ activeMQ = new ActiveMQ(); // send messages through the queue

        /**
         * Main method which computes the itenerary to enqueue
         */
        public async void GetItinerary(string destinationAddress, string originAddress)
        {
            // retrive data from destination and origin
            string destinationData = await openRouteServiceCall.GetDataFromLocation(destinationAddress);
            string originData = await openRouteServiceCall.GetDataFromLocation(originAddress);
            ORSGeocode orsDestination = deserializer.GetORSGeocodeObject(destinationData);
            ORSGeocode orsOrigin = deserializer.GetORSGeocodeObject(originData);

            // get the cities to find eventual contracts
            string destinationCity = orsDestination.features[0].properties.locality;
            if (destinationCity == null)
                destinationCity = orsDestination.geocoding.query.parsed_text.city;

            string originCity = orsOrigin.features[0].properties.locality;
            if (originCity == null)
                originCity = orsOrigin.geocoding.query.parsed_text.city;

            // get coordinates from destination and origin
            Position destinationCoordinates = positionFromOrsObject(orsDestination);
            Position originCoordinates = positionFromOrsObject(orsOrigin);




            // retrieve the foot directions object
            string footStepData = await openRouteServiceCall.GetStepData(originCoordinates, destinationCoordinates, "foot-walking");
            ORSDirections foot = deserializer.GetStepData(footStepData);

            // retrieve destination stations whith the help of ProxyCache
            string destinationStationsData = proxyClient.GetStationsFromContract(GetContractOfACity(destinationCity));
            List<JCDStation> destinationStations = deserializer.GetStationsList(destinationStationsData);
            List<JCDStation> originStations;

            // if the user moves in only one city
            if (originCity.Equals(destinationCity))
            {
                originStations = destinationStations;
                if (destinationStations == null) // no stations available, foot itinerary is computed
                    activeMQ.Queue(StepsByFoot(originAddress, destinationAddress, foot));

                // stations available, foot + bike itinerary is computed
                activeMQ.Queue(await GetTraveling(destinationStations, originStations, destinationCoordinates, originCoordinates, foot, destinationAddress, originAddress));
            }

            // the user moves from a city to another
            else
            {
                // retrieve origin stations whith the help of ProxyCache
                string originStationsData = proxyClient.GetStationsFromContract(GetContractOfACity(originCity));
                originStations = deserializer.GetStationsList(originStationsData);

                if (destinationStations == null && originStations == null) // no stations available, foot itinerary is computed
                    activeMQ.Queue(StepsByFoot(originAddress, destinationAddress, foot));

                else if (destinationStations == null && originStations != null) // only origin stations available, foot + bike itinerary is computed
                    activeMQ.Queue(await GetTraveling(originStations, originStations, destinationCoordinates, originCoordinates, foot, destinationAddress, originAddress)); 

                else if (destinationStations != null && originStations == null) // only destination stations available, foot + bike itinerary is computed
                    activeMQ.Queue(await GetTraveling(destinationStations, destinationStations, destinationCoordinates, originCoordinates, foot, destinationAddress, originAddress));

                else // both origin and destinations stations available, foot + bike itinerary is computed
                    activeMQ.Queue(await GetTravelingUsingFourStations(destinationStations, originStations, destinationCoordinates, originCoordinates, foot, destinationAddress, originAddress));
            }
        }

        /**
         * Compute itineraty depending on what is worth between walking and cycling
         */
        private async Task<string> GetTraveling(List<JCDStation> destinationStations, List<JCDStation> originStations,
            Position destinationCoordinates, Position originCoordinates, ORSDirections foot, string destinationAddress, string originAddress)
        {
            // get data
            JCDStation closestStationFromDestination = ClosestStationFromLocation(destinationStations, destinationCoordinates);
            JCDStation closestStationFromOrigin = ClosestStationFromLocation(originStations, originCoordinates);

            string originStationData = await openRouteServiceCall.GetStepData(originCoordinates, closestStationFromOrigin.position, "foot-walking");
            ORSDirections originStation = deserializer.GetStepData(originStationData);

            string stationToStationData = await openRouteServiceCall.GetStepData(closestStationFromOrigin.position, closestStationFromDestination.position, "cycling-regular");
            ORSDirections stationToStation = deserializer.GetStepData(stationToStationData);

            string stationDestinationData = await openRouteServiceCall.GetStepData(closestStationFromDestination.position, destinationCoordinates, "foot-walking");
            ORSDirections stationDestination = deserializer.GetStepData(stationDestinationData);


            double originStationDuration = originStation.features[0].properties.segments[0].duration;
            double stationToStationDuration = stationToStation.features[0].properties.segments[0].duration;
            double stationDestinationDuration = stationDestination.features[0].properties.segments[0].duration;

            // check what is worth between walking and cycling
            double footDuration = foot.features[0].properties.segments[0].duration;
            if (footDuration < (originStationDuration + stationToStationDuration + stationDestinationDuration))
            {
                return StepsByFoot(originAddress, destinationAddress, foot);
            }

            return StepsByBike(originAddress, destinationAddress, closestStationFromOrigin, closestStationFromDestination, originStation, stationToStation, stationDestination);
        }

        /**
         * Compute itineraty depending on what is worth between walking and cycling
         * Using 4 stations in case if the cities departure and arrival are different
         */
        private async Task<string> GetTravelingUsingFourStations(List<JCDStation> destinationStations, List<JCDStation> originStations,
            Position destinationCoordinates, Position originCoordinates, ORSDirections foot, string destinationAddress, string originAddress)
        {
            // get data
            JCDStation closestStationFromDestination = ClosestStationFromLocation(destinationStations, destinationCoordinates);
            JCDStation closestStationFromLastStation = ClosestStationFromLocation(destinationStations, closestStationFromDestination.position);

            JCDStation closestStationFromOrigin = ClosestStationFromLocation(originStations, originCoordinates);
            JCDStation closestStationFromFirstStation = ClosestStationFromLocation(destinationStations, closestStationFromOrigin.position);

            ORSDirections originToStation = deserializer.GetStepData(await openRouteServiceCall.GetStepData(originCoordinates, closestStationFromOrigin.position, "foot-walking"));
            ORSDirections stationToStationOrigin = deserializer.GetStepData(await openRouteServiceCall.GetStepData(closestStationFromOrigin.position, closestStationFromFirstStation.position, "cycling-regular"));

            ORSDirections stationToStation = deserializer.GetStepData(await openRouteServiceCall.GetStepData(closestStationFromFirstStation.position, closestStationFromLastStation.position, "foot-walking"));

            ORSDirections stationToStationDestination = deserializer.GetStepData(await openRouteServiceCall.GetStepData(closestStationFromLastStation.position, closestStationFromDestination.position, "cycling-regular"));
            ORSDirections stationToDestination = deserializer.GetStepData(await openRouteServiceCall.GetStepData(closestStationFromDestination.position, destinationCoordinates, "foot-walking"));


            double originToStationDuration = originToStation.features[0].properties.segments[0].duration;
            double stationToStationOriginDuration = stationToStationOrigin.features[0].properties.segments[0].duration;
            double stationToStationDuration = stationToStation.features[0].properties.segments[0].duration;
            double stationToStationDestinationDuration = stationToStationDestination.features[0].properties.segments[0].duration;
            double stationDestinationDuration = stationToDestination.features[0].properties.segments[0].duration;

            // check what is worth between walking and cycling
            double footDuration = foot.features[0].properties.segments[0].duration;
            if (footDuration < (originToStationDuration + stationToStationOriginDuration + stationToStationDuration + stationToStationDestinationDuration + stationDestinationDuration))
            {
                return StepsByFoot(originAddress, destinationAddress, foot);
            }

            return StepsByBikeUsingFourStations(originAddress, destinationAddress,
                closestStationFromOrigin, closestStationFromFirstStation,
                closestStationFromLastStation, closestStationFromDestination,
                originToStation, stationToStationOrigin, stationToStation, stationToStationDestination, stationToDestination);
        }

        /**
         * Convert coordinates into a position
         */
        private Position positionFromOrsObject(ORSGeocode orsObject)
        {
            float[] coordinates = orsObject.features[0].geometry.coordinates;
            Double longitude = Convert.ToDouble(coordinates[0]);
            Double latitude = Convert.ToDouble(coordinates[1]);
            return new Position(latitude, longitude);
        }

        /**
         * Build a string with the itinerary steps
         */
        private string Steps(ORSDirections directions)
        {
            string allSteps = "";
            Step[] steps = directions.features[0].properties.segments[0].steps;
            double totalDistance = directions.features[0].properties.segments[0].distance;
            double totalDuration = directions.features[0].properties.segments[0].duration;

            foreach (Step step in steps)
            {
                allSteps += step.instruction;
                allSteps += "\n     Distance to do (m): " + step.distance + "       time (s): " + step.duration;
                allSteps += "\n     Distance left  (m): " + Math.Max(0, Math.Round((totalDistance -= step.distance), 2));
                allSteps += "\n     Time left      (s): " + Math.Max(0, Math.Round((totalDuration -= step.duration), 2)) + "\n";
            }

            return allSteps;
        }

        /**
         * Add a title with "by foot" mention to the steps itinerary
         */
        private string StepsByFoot(string originAddress, string destinationAddress, ORSDirections foot)
        {
            string directions = "--- Steps from " + originAddress + " to " + destinationAddress + " by foot ---\n";
            return (directions + Steps(foot));
        }

        /**
         * Add titles with "by foot" and "by bike" mention to the steps itinerary
         */
        private string StepsByBike(string originAddress, string destinationAddress,
            JCDStation closestOriginStation, JCDStation closestDestinationStation,
            ORSDirections originStation, ORSDirections stationToStation, ORSDirections stationDestination)
        {
            string directions = "--- Steps from " + originAddress + " to " + closestOriginStation.name + " by foot ---\n";
            directions += Steps(originStation);

            directions += "\n\n--- Steps from " + closestOriginStation.name + " to " + closestDestinationStation.name + " by bike ---\n";
            directions += Steps(stationToStation);

            directions += "\n\n--- Steps from " + closestDestinationStation.name + " to " + destinationAddress + " by foot ---\n";
            directions += Steps(stationDestination);

            return directions;
        }

        /**
         * Add titles with "by foot" and "by bike" mention to the steps itinerary
         * In case if 4 stations are used
         */
        private string StepsByBikeUsingFourStations(string originAddress, string destinationAddress,
            JCDStation closestOriginStation, JCDStation closestStationFromFirstStation,
            JCDStation closestStationFromLastStation, JCDStation closestDestinationStation,
            ORSDirections originToStation, ORSDirections stationToStationOrigin, ORSDirections stationToStation,
            ORSDirections stationToStationDestination, ORSDirections stationToDestination)
        {
            string directions = "--- Steps from " + originAddress + " to " + closestOriginStation.name + " by foot ---\n";
            directions += Steps(originToStation);

            directions += "\n\n--- Steps from " + closestOriginStation.name + " to " + closestStationFromFirstStation.name + " by bike ---\n";
            directions += Steps(stationToStationOrigin);

            directions += "\n\n--- Steps from " + closestStationFromFirstStation.name + " to " + closestStationFromLastStation.name + " by foot ---\n";
            directions += Steps(stationToStation);

            directions += "\n\n--- Steps from " + closestStationFromLastStation.name + " to " + closestDestinationStation.name + " by bike ---\n";
            directions += Steps(stationToStationDestination);

            directions += "\n\n--- Steps from " + closestDestinationStation.name + " to " + destinationAddress + " by foot ---\n";
            directions += Steps(stationToDestination);

            return directions;
        }

        /**
         * Get the closest station from a location
         */
        private JCDStation ClosestStationFromLocation(List<JCDStation> stations, Position coordinates)
        {
            GeoCoordinate destinationCoordinates = new GeoCoordinate(coordinates.latitude, coordinates.longitude);

            JCDStation closestStation = stations[0];
            GeoCoordinate possibleStation = new GeoCoordinate(closestStation.position.latitude, closestStation.position.longitude);

            Double minDistance = destinationCoordinates.GetDistanceTo(possibleStation);

            foreach (JCDStation station in stations)
            {
                possibleStation = new GeoCoordinate(station.position.latitude, station.position.longitude);

                Double distance = destinationCoordinates.GetDistanceTo(possibleStation);

                if (distance < minDistance)
                {
                    closestStation = station;
                    minDistance = distance;
                }
            }

            return closestStation;
        }

        /**
         * Get the contract of a city
         */
        private string GetContractOfACity(string city)
        {
            string jsonContracts = proxyClient.GetContracts();
            List<JCDContract> contracts = deserializer.GetContractsList(jsonContracts);

            foreach (JCDContract contract in contracts)
            {
                if (contract.cities != null) // Besancon case : has null attributes (not only works for Besancon but also for all this kind of issue)
                    if (contract.cities.Contains(city))
                        return contract.name;
            }

            return city;
        }
    }
}
