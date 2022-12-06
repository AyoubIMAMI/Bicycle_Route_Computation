using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RoutingServer
{
    /**
     * Deserialize json
     */
    internal class Deserializer
    {
        /**
         * Retrieve a list of stations
         */
        public List<JCDStation> GetStationsList(String json)
        {
            return JsonSerializer.Deserialize<List<JCDStation>>(json);
        }


        /**
         * Retrieve a list of conracts
         */
        public List<JCDContract> GetContractsList(String json)
        {
            return JsonSerializer.Deserialize<List<JCDContract>>(json);
        }

        /**
         * Retrieve an object allowing to get positions data
         */
        public ORSGeocode GetORSGeocodeObject(string json)
        {
            ORSGeocode orsObject = JsonSerializer.Deserialize<ORSGeocode>(json);
            return orsObject;
        }

        /**
         * Retrieve an object allowing to get directions data
         */
        internal ORSDirections GetStepData(string json)
        {
            return JsonSerializer.Deserialize<ORSDirections>(json);
        }
    }
}
