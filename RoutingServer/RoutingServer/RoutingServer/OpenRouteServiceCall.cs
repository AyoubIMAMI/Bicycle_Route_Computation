using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RoutingServer.Itinerary;

namespace RoutingServer
{
    internal class OpenRouteServiceCall
    {
        // OpenRouteService API Key = 5b3ce3597851110001cf624875e1d0ec212b4bf9ac3004e35d344ef1
        static string ORSKey = "5b3ce3597851110001cf624875e1d0ec212b4bf9ac3004e35d344ef1";

        public static async Task<string> GetPositionFromLocation(string location)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get the location position
                string position = await client.GetStringAsync("https://api.openrouteservice.org/geocode/search?api_key=" + ORSKey + "&text=" + location);
                return position;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "\nException Caught!";
            }
        }
    }
}
