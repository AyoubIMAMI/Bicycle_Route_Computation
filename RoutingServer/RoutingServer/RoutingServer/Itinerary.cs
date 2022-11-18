using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Itinerary : IItinerary
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        public async Task<double> GetItinerary(string destinationAddress, string originAddress)
        {
            float[] destinationCoordinates = await OpenRouteServiceCall.GetCoordinatesFromLocation(destinationAddress);

            float[] originCoordinates = await OpenRouteServiceCall.GetCoordinatesFromLocation(originAddress);

            return destinationCoordinates[0];
        }

        public async Task<string> TestJCD()
        {
            return await JCDecauxCall.CallTest();
        }
    }
}
