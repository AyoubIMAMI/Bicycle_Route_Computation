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

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Itinerary : IItinerary
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        public async Task<string> GetItinerary(string destinationAddress, string originAddress)
        {
            OpenRouteServiceCall openRouteServiceCall = new OpenRouteServiceCall();
            JCDecauxCall jCDecauxCall = new JCDecauxCall();

            await openRouteServiceCall.FillUpDataFromLocation(destinationAddress, originAddress);

            string destinationCity = openRouteServiceCall.GetCity(0);
            string originCity = openRouteServiceCall.GetCity(1);

            Position destinationCoordinates = openRouteServiceCall.GetCoordinates(0);
            Position originCoordinates = openRouteServiceCall.GetCoordinates(1);

            ORSDirections foot = await openRouteServiceCall.GetStepData(originCoordinates, destinationCoordinates, "foot-walking");

            List<JCDStation> stations = await jCDecauxCall.GetStationsFromContract(destinationCity);

            if (stations == null)
            {
                return StepsByFoot(originAddress, destinationAddress, foot);
            }

            JCDStation closestStationFromDestination = ClosestStationFromLocation(stations, destinationCoordinates);
            JCDStation closestStationFromOrigin;

            if (!destinationCity.Equals(originCity))
            {
                List<JCDStation> originStations = await jCDecauxCall.GetStationsFromContract(originCity);
                if (originStations == null)
                {
                    return StepsByFoot(originAddress, destinationAddress, foot);
                }
                closestStationFromOrigin = ClosestStationFromLocation(originStations, originCoordinates);
            }
            else
                closestStationFromOrigin = ClosestStationFromLocation(stations, originCoordinates);

            ORSDirections originStation = await openRouteServiceCall.GetStepData(originCoordinates, closestStationFromOrigin.position, "foot-walking");
            ORSDirections stationToStation = await openRouteServiceCall.GetStepData(closestStationFromOrigin.position, closestStationFromDestination.position, "cycling-regular");
            ORSDirections stationDestination = await openRouteServiceCall.GetStepData(closestStationFromDestination.position, destinationCoordinates, "foot-walking");

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

        private string Steps(ORSDirections directions)
        {
            string allSteps = "";
            Step[] steps = directions.features[0].properties.segments[0].steps;
            double totalDistance = directions.features[0].properties.segments[0].distance;
            double totalDuration = directions.features[0].properties.segments[0].duration;

            foreach (Step step in steps)
            {
                allSteps += step.instruction;
                allSteps += "\n     Distance to do (m): " + step.distance + "(time (s): " + step.duration + ")";
                allSteps += "\n     Distance left  (m): " + (totalDistance - step.distance);
                allSteps += "\n     Time left      (s): " + (totalDuration - step.duration);
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
