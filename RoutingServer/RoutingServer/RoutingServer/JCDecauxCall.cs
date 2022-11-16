using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace RoutingServer
{
    internal class JCDecauxCall
    {
        //JCDecaux API Key = 29383ef5f8094df302e81d893499258dc7f08a5b

        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        public static async Task<string> CallTest()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get all the stations
                string allStationsList = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?contract=besancon&apiKey=29383ef5f8094df302e81d893499258dc7f08a5b");
                return allStationsList;
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
