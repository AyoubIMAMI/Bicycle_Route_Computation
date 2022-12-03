using ProxyCach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyCach
{
    internal class JsonManager
    {
        public List<JCDStation> GetStationsListFromJson(String json)
        {
            return JsonSerializer.Deserialize<List<JCDStation>>(json);
        }

        public string GetJsonFromStationsList(List<JCDStation> stations)
        {
            return JsonSerializer.Serialize(stations);
        }


        public List<JCDContract> GetContractsListFromJson(String json)
        {
            return JsonSerializer.Deserialize<List<JCDContract>>(json);
        }

        public string GetJsonFromContractsList(List<JCDContract> contracts)
        {
            return JsonSerializer.Serialize(contracts);
        }
    }
}
