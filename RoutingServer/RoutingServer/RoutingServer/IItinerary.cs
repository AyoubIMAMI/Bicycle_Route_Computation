using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    [ServiceContract]
    public interface IItinerary
    {
        [OperationContract]
        Task<string> GetItinerary(string destinationAddress, string originAddress);

        [OperationContract]
        Task<string> TestJCD();
    }
}
