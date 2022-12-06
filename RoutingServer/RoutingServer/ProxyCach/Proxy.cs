using System.Collections.Generic;

/**
 * @author Ayoub IMAMI
 */

namespace ProxyCach
{
    /**
     * Proxy cache to avoid unecessary repetitives calls the to the JCDecaux API
     * Check if the needed data are in the cache, otherwise call the API
     */
    internal class Proxy : IProxy
    {
        JsonManager serializer = new JsonManager();
        readonly string contractKey = "contracts";

        public string GetStationsFromContract(string contract)
        {
            GenericProxyCache<JCDItemStation> gpcStation = new GenericProxyCache<JCDItemStation>();

            object[] arguments = { contract };
            JCDItemStation jCDItemStation = gpcStation.Get(contract, arguments);
            List<JCDStation> stations = jCDItemStation.GetStations();
            return serializer.GetJsonFromStationsList(stations);
        }

        public string GetContracts()
        {
            GenericProxyCache<JCDItemContract> gpcContract = new GenericProxyCache<JCDItemContract>();

            object[] arguments = { };
            JCDItemContract jCDItemContract = gpcContract.Get(contractKey, arguments);
            List<JCDContract> contracts = jCDItemContract.GetContracts();
            return serializer.GetJsonFromContractsList(contracts);
        }

    }
}
