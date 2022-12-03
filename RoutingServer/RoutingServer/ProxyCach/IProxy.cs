using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
