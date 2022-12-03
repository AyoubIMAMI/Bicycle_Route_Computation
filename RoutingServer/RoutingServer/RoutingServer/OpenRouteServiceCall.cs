using System;
using System.Collections.Generic;
using System.Globalization;
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
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        // OpenRouteService API Key = 5b3ce3597851110001cf624875e1d0ec212b4bf9ac3004e35d344ef1
        static readonly string ORSKey = "5b3ce3597851110001cf6248e2a1647c8f414cb8954a61dd9617801es";

        public async Task<string> GetDataFromLocation(string location)
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
                return null;
            }
        }

        public async Task<string> GetStepData(Position start, Position end, string transport)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {//
                // Get the location position
                string apiKeyQuery = "?api_key=" + ORSKey;
                string startQuery = "&start=" + start.longitude.ToString(CultureInfo.InvariantCulture) + "," + start.latitude.ToString(CultureInfo.InvariantCulture);
                string endQuery = "&end=" + end.longitude.ToString(CultureInfo.InvariantCulture) + "," + end.latitude.ToString(CultureInfo.InvariantCulture);
                string query = "https://api.openrouteservice.org/v2/directions/" + transport + apiKeyQuery + startQuery + endQuery;
                return await client.GetStringAsync(query);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine();
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
