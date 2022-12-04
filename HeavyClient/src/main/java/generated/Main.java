package generated;

import org.apache.activemq.ActiveMQConnectionFactory;

import javax.jms.*;

public class Main {

    public static void main(String[] args) {
        Itinerary itinerary = new Itinerary();
        IItinerary iItinerary = itinerary.getBasicHttpBindingIItinerary();

        final String footDestinationAddress = "Polytech Nice-Sophia, 06410 Biot";
        final String footOriginAddress = "Lycée Polyvalent Leonard de Vinci, 06600 Antibes";

        final String bikeDestinationAddress = "Rouen"; //"Dieweg 69, 1180 Uccle, Belgique";
        final String bikeOriginAddress = "Besancon";

        iItinerary.getItinerary(footDestinationAddress, footOriginAddress);
        iItinerary.getItinerary(bikeDestinationAddress, bikeOriginAddress);

        try {

            // Create a ConnectionFactory
            ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://localhost:61616");

            // Create a Connection
            Connection connection = connectionFactory.createConnection();
            connection.start();

            //connection.setExceptionListener(this);

            // Create a Session
            Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

            // Create the destination (Topic or Queue)
            Destination destination = session.createQueue("DirectionsSteps");

            // Create a MessageConsumer from the Session to the Topic or Queue
            MessageConsumer consumer = session.createConsumer(destination);

            // Wait for a message
            Message message = consumer.receive(1000);

            if (message instanceof TextMessage) {
                TextMessage textMessage = (TextMessage) message;
                String text = textMessage.getText();
                System.out.println("Received: " + text);
            } else {
                System.out.println("Received: " + message);
            }

            consumer.close();
            session.close();
            connection.close();
        } catch (Exception e) {
            System.out.println("Caught: " + e);
            e.printStackTrace();
        }
    }

    public synchronized void onException(JMSException ex) {
        System.out.println("JMS Exception occured.  Shutting down client.");
    }
}
