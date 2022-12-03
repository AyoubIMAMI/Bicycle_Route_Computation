using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
    internal class Deserializer
    {
        public List<JCDStation> GetStationsList(String json)
        {
            return JsonSerializer.Deserialize<List<JCDStation>>(json);
        }

        public List<JCDContract> GetContractsList(String json)
        {
            return JsonSerializer.Deserialize<List<JCDContract>>(json);
        }

        public ORSGeocode GetORSGeocodeObject(string json)
        {
            ORSGeocode orsObject = JsonSerializer.Deserialize<ORSGeocode>(json);
            return orsObject;
        }

        internal ORSDirections GetStepData(string json)
        {
            return JsonSerializer.Deserialize<ORSDirections>(json);
        }
    }
}
