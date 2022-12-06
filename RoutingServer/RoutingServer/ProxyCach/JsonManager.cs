using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ProxyCach
{
    /**
     * Serialize and deserialize json
     */
    internal class JsonManager
    {
        /**
         * Retrieve a list of stations
         */
        public List<JCDStation> GetStationsListFromJson(String json)
        {
            return JsonSerializer.Deserialize<List<JCDStation>>(json);
        }

        /**
         * Convert a list of stations into a json
         */
        public string GetJsonFromStationsList(List<JCDStation> stations)
        {
            return JsonSerializer.Serialize(stations);
        }

        /**
         * Retrieve a list of contracts
         */
        public List<JCDContract> GetContractsListFromJson(String json)
        {
            return JsonSerializer.Deserialize<List<JCDContract>>(json);
        }

        /**
         * Convert a list of contracts into a json
         */
        public string GetJsonFromContractsList(List<JCDContract> contracts)
        {
            return JsonSerializer.Serialize(contracts);
        }
    }
}
