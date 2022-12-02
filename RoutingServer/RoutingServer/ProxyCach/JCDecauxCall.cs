using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ProxyCach.Proxy;

namespace ProxyCach
{
    internal class JCDecauxCall
    {
        //JCDecaux API Key = 29383ef5f8094df302e81d893499258dc7f08a5b
        static readonly string JCDKey = "29383ef5f8094df302e81d893499258dc7f08a5b";

        public async Task<string> GetStationsFromContract(string city)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // Get all the stations
                return await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?apiKey=" + JCDKey + "&contract=" + city);
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
