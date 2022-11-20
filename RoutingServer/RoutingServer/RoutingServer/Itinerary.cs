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


namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Itinerary : IItinerary
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        Double originDestinationDistance;
        Double originStationDistace;
        Double stationToStationDistance;
        Double stationDestinationDistance;

        public async Task<Double> GetItinerary(string destinationAddress, string originAddress)
        {
            OpenRouteServiceCall openRouteServiceCall = new OpenRouteServiceCall();
            JCDecauxCall jCDecauxCall = new JCDecauxCall();

            openRouteServiceCall.FillUpDataFromLocation(destinationAddress, originAddress);

            string destinationCity = openRouteServiceCall.GetCity(0);
            string originCity = openRouteServiceCall.GetCity(1);

            Position destinationCoordinates = openRouteServiceCall.GetCoordinates(0);
            Position originCoordinates = openRouteServiceCall.GetCoordinates(1);

            GeoCoordinate destinationGeo = new GeoCoordinate(destinationCoordinates.latitude, destinationCoordinates.longitude);
            GeoCoordinate originGeo = new GeoCoordinate(originCoordinates.latitude, originCoordinates.longitude);
            originDestinationDistance = originGeo.GetDistanceTo(destinationGeo);

            List<JCDStation> stations = await jCDecauxCall.GetStationsFromContract(destinationCity);
            JCDStation closestStationFromDestination = ClosestStationFromLocation(stations, destinationCoordinates);
            JCDStation closestStationFromOrigin;

            if (!destinationCity.Equals(originCity))
            {
                List<JCDStation> originStations = await jCDecauxCall.GetStationsFromContract(originCity);
                closestStationFromOrigin = ClosestStationFromLocation(originStations, originCoordinates);
            }
            else
                closestStationFromOrigin = ClosestStationFromLocation(stations, originCoordinates);

            originStationDistace = ComputeDistances(new Position(originCoordinates.latitude, originCoordinates.longitude), new Position(closestStationFromOrigin.position.latitude, closestStationFromOrigin.position.longitude));
            stationToStationDistance = ComputeDistances(new Position(closestStationFromOrigin.position.latitude, closestStationFromOrigin.position.longitude), new Position(closestStationFromDestination.position.latitude, closestStationFromDestination.position.longitude));
            stationDestinationDistance = ComputeDistances(new Position(closestStationFromDestination.position.latitude, closestStationFromDestination.position.longitude), new Position(destinationCoordinates.latitude, destinationCoordinates.longitude));

            Double totalDistance = originStationDistace + stationToStationDistance + stationDestinationDistance;

            return totalDistance;
        }

        private Double ComputeDistances(Position start, Position arrival)
        {
            GeoCoordinate geoStart = new GeoCoordinate(start.latitude, start.longitude);
            GeoCoordinate geoArrival = new GeoCoordinate(arrival.latitude, arrival.longitude);
            return geoStart.GetDistanceTo(geoArrival);
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
