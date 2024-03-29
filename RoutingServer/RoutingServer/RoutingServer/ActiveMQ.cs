﻿using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace RoutingServer
{
    /**
     * This class is used to enqueue messages via ActiveMQ
     */
    internal class ActiveMQ
    {
        /**
         * Put the text parameter in a queue
         */
        public void Queue(string text)
        {
            // Create a Connection Factory.
            Uri connecturi = new Uri("activemq:tcp://localhost:61616");
            ConnectionFactory connectionFactory = new ConnectionFactory(connecturi);

            // Create a single Connection from the Connection Factory.
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();

            // Create a session from the Connection.
            ISession session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue("DirectionsSteps");

            // Create a Producer targetting the selected queue.
            IMessageProducer producer = session.CreateProducer(destination);

            // You may configure everything to your needs, for instance:
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

            // Finally, to send messages:
            ITextMessage message = session.CreateTextMessage(text);
            producer.Send(message);

            Console.WriteLine("Message sent, check ActiveMQ web interface to confirm.");
            Console.ReadLine();

            // Don't forget to close your session and connection when finished.
            session.Close();
            connection.Close();
        }
    }
}
