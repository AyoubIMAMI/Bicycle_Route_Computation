using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Device.Location;
using System.Globalization;
using RoutingServer.ServiceReference1;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Itinerary : IItinerary
    {
        //TODO DELETE HTTPCLIENT
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        public async Task<string> GetItinerary(string destinationAddress, string originAddress)
        {
            OpenRouteServiceCall openRouteServiceCall = new OpenRouteServiceCall();
            Deserializer deserializer = new Deserializer();
            ProxyClient proxyClient = new ProxyClient();

            string destinationData = await openRouteServiceCall.GetDataFromLocation(destinationAddress);
            string originData = await openRouteServiceCall.GetDataFromLocation(originAddress);
            ORSGeocode orsDestination = deserializer.GetORSGeocodeObject(destinationData);
            ORSGeocode orsOrigin = deserializer.GetORSGeocodeObject(originData);

            string destinationCity = orsDestination.geocoding.query.parsed_text.city;
            string originCity = orsOrigin.geocoding.query.parsed_text.city;

            Position destinationCoordinates = positionFromOrsObject(orsDestination);
            Position originCoordinates = positionFromOrsObject(orsOrigin);

            string footStepData = await openRouteServiceCall.GetStepData(originCoordinates, destinationCoordinates, "foot-walking");
            ORSDirections foot = deserializer.GetStepData(footStepData);

            string destinationStationsData = await jCDecauxCall.GetStationsFromContract(destinationCity);
            List<JCDStation> stations = deserializer.GetStationsList(destinationStationsData);

            if (stations == null)
            {
                return StepsByFoot(originAddress, destinationAddress, foot);
            }

            JCDStation closestStationFromDestination = ClosestStationFromLocation(stations, destinationCoordinates);
            JCDStation closestStationFromOrigin;

            if (!destinationCity.Equals(originCity))
            {
                string originStationsData = await jCDecauxCall.GetStationsFromContract(originCity);
                List<JCDStation> originStations = deserializer.GetStationsList(originStationsData);
                if (originStations == null)
                {
                    return StepsByFoot(originAddress, destinationAddress, foot);
                }
                closestStationFromOrigin = ClosestStationFromLocation(originStations, originCoordinates);
            }
            else
                closestStationFromOrigin = ClosestStationFromLocation(stations, originCoordinates);

            string originStationData = await openRouteServiceCall.GetStepData(originCoordinates, closestStationFromOrigin.position, "foot-walking");
            ORSDirections originStation = deserializer.GetStepData(originStationData);
            string stationToStationData = await openRouteServiceCall.GetStepData(closestStationFromOrigin.position, closestStationFromDestination.position, "cycling-regular");
            ORSDirections stationToStation = deserializer.GetStepData(stationToStationData);
            string stationDestinationData = await openRouteServiceCall.GetStepData(closestStationFromDestination.position, destinationCoordinates, "foot-walking");
            ORSDirections stationDestination = deserializer.GetStepData(stationDestinationData);

            double originStationDuration = originStation.features[0].properties.segments[0].duration;
            double stationToStationDuration = stationToStation.features[0].properties.segments[0].duration;
            double stationDestinationDuration = stationDestination.features[0].properties.segments[0].duration;

            double footDuration = foot.features[0].properties.segments[0].duration;

            if (footDuration < (originStationDuration + stationToStationDuration + stationDestinationDuration))
            {
                return StepsByFoot(originAddress, destinationAddress, foot);
            }

            //return string bike steps
            return StepsByBike(originAddress, destinationAddress, closestStationFromOrigin, closestStationFromDestination, originStation, stationToStation, stationDestination);
        }

        private Position positionFromOrsObject(ORSGeocode orsObject)
        {
            float[] coordinates = orsObject.features[0].geometry.coordinates;
            Double longitude = Convert.ToDouble(coordinates[0]);
            Double latitude = Convert.ToDouble(coordinates[1]);
            return new Position(latitude, longitude);
        }

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

        private string StepsByFoot(string originAddress, string destinationAddress, ORSDirections foot)
        {
            string footDirections = "--- Steps from " + originAddress + " to " + destinationAddress + " by foot ---\n";
            return (footDirections + Steps(foot));
        }

        private string StepsByBike(string originAddress, string destinationAddress, JCDStation closestOriginStation, JCDStation closestDestinationStation, ORSDirections originStation, ORSDirections stationToStation, ORSDirections stationDestination)
        {
            string bikeDirections = "--- Steps from " + originAddress + " to " + closestOriginStation.name + " by foot ---\n";
            bikeDirections += Steps(originStation);

            bikeDirections += "\n\n--- Steps from " + closestOriginStation.name + " to " + closestDestinationStation.name + " by bike ---\n";
            bikeDirections += Steps(stationToStation);

            bikeDirections += "\n\n--- Steps from " + closestDestinationStation.name + " to " + destinationAddress + " by foot ---\n";
            bikeDirections += Steps(stationDestination);

            return bikeDirections;
        }

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
    }
}
