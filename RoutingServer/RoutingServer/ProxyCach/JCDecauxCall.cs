using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProxyCach
{

    /**
     * Call the JCDecaux API
     */
    internal class JCDecauxCall
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        private readonly HttpClient client = new HttpClient();
        JsonManager deserializer = new JsonManager();

        //JCDecaux API Key = 29383ef5f8094df302e81d893499258dc7f08a5b
        private readonly string JCDKey = "29383ef5f8094df302e81d893499258dc7f08a5b";

        public async Task<List<JCDStation>> GetStationsFromContract(string city)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string stations = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/stations?apiKey=" + JCDKey + "&contract=" + city);
                // Get all the stations from the contract
                return deserializer.GetStationsListFromJson(stations);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }

        public async Task<List<JCDContract>> GetContracts()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string contracts = await client.GetStringAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + JCDKey);
                // Get all the contracts
                return deserializer.GetContractsListFromJson(contracts);
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
