using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using static RoutingServer.Itinerary;
using System.Text.Json;

namespace RoutingServer
{
    internal class JCDecauxCall
    {
        //JCDecaux API Key = 29383ef5f8094df302e81d893499258dc7f08a5b
        static readonly string JCDKey = "29383ef5f8094df302e81d893499258dc7f08a5b";

        public async Task<List<JCDStation>> GetStationsFromContract(string city)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get all the stations
                string stationsData = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?apiKey=" + JCDKey + "&contract=" + city);
                List<JCDStation> sations = JsonSerializer.Deserialize<List<JCDStation>>(stationsData);
                return sations;
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
