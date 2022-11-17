using ClientTest.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start\n");

            ItineraryClient itineraryClient = new ItineraryClient();

            Console.WriteLine("Enter your destination:");
            string destination = Console.ReadLine();

            Console.WriteLine("\nEnter your location:");
            string location = Console.ReadLine();

            Console.WriteLine("\n" + itineraryClient.GetItinerary(destination, location) + "\n");

            // Fermez toujours le client.
            itineraryClient.Close();

            Console.WriteLine("End\nPress enter to exit\n");
            Console.ReadLine();
        }
    }
}
