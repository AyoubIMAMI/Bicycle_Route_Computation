using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClient.ServiceReference1;

namespace TestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ItineraryClient itineraryClient = new ItineraryClient();

            itineraryClient.testJCD();
        }
    }
}
