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
        static string ORSKey = "5b3ce3597851110001cf6248e2a1647c8f414cb8954a61dd9617801es";
        static int latitude = 0;
        static int longitude = 1;

        public static async Task<float[]> GetCoordinatesFromLocation(string location)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get the location position
                string locationData = await client.GetStringAsync("https://api.openrouteservice.org/geocode/search?api_key=" + ORSKey + "&text=" + location);
                Geocode destinationGeocode = JsonSerializer.Deserialize<Geocode>(locationData);
                float[] coordinates = destinationGeocode.features[0].geometry.coordinates;

                return coordinates;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
