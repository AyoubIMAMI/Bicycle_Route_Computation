using System.ServiceModel;

namespace RoutingServer
{
    [ServiceContract]
    public interface IItinerary
    {
        [OperationContract]
        void GetItinerary(string destinationAddress, string originAddress);
    }
}
