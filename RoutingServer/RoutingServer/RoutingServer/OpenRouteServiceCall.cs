using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RoutingServer.Itinerary;

namespace RoutingServer
{
    internal class OpenRouteServiceCall
    {
        // OpenRouteService API Key = 5b3ce3597851110001cf624875e1d0ec212b4bf9ac3004e35d344ef1
        static readonly string ORSKey = "5b3ce3597851110001cf6248e2a1647c8f414cb8954a61dd9617801es";

        static Position destinationCoordinates;
        static Position originCoordinates;

        static string destinationCity;
        static string originCity;

        public void FillUpDataFromLocation(string destinationLocation, string originLocation)
        {
            FillUp(destinationLocation, 0);
            FillUp(originLocation, 1);
            //noNull = false;
        }

        private async void FillUp(string location, int difference)
        {
            // Get the location position
            string locationData = await GetDataFromLocation(location);
            Rootobject orsObject = JsonSerializer.Deserialize<Rootobject>(locationData);

            Double latitude = Convert.ToDouble(orsObject.features[0].geometry.coordinates[0]);
            Double longitude = Convert.ToDouble(orsObject.features[0].geometry.coordinates[1]);

            if (difference == 0)
            {
                destinationCoordinates = new Position(latitude, longitude);
                destinationCity = orsObject.geocoding.query.parsed_text.city;
            }
            else
            {
                originCoordinates = new Position(latitude, longitude);
                originCity = orsObject.geocoding.query.parsed_text.city;
            }
        }

        private async Task<string> GetDataFromLocation(string location)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get the location position
                return await client.GetStringAsync("https://api.openrouteservice.org/geocode/search?api_key=" + ORSKey + "&text=" + location);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("Message :{0} ", e.Message);
                return "\nException Caught!";
            }
        }

        public string GetCity(int difference)
        {
            if (difference == 0) return destinationCity;
            else return originCity;
        }

        public Position GetCoordinates(int difference)
        {
            if (difference == 0) return destinationCoordinates;
            else return originCoordinates;
        }
    }
}
