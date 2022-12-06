﻿using System;
using System.ServiceModel.Description;
using System.ServiceModel;

namespace RoutingServer
{
    internal class SelfHosted
    {
        /**
         * Self Host
         */
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/RoutingServer/Service1/");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(Itinerary), httpUrl);

            //Add a service endpoint
            host.AddServiceEndpoint(typeof(IItinerary), new BasicHttpBinding(), "");

            //Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();

            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press <Enter> key to stop");
            Console.ReadLine();

        }

    }
}
