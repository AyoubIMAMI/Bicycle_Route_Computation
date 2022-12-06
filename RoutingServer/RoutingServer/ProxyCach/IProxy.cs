using System.ServiceModel;

namespace ProxyCach
{
    [ServiceContract]
    internal interface IProxy
    {
        [OperationContract]
        string GetStationsFromContract(string contract);

        [OperationContract]
        string GetContracts();
    }
}
