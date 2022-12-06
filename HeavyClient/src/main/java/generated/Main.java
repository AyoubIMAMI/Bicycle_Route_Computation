package generated;

import org.apache.activemq.ActiveMQConnectionFactory;
import javax.jms.*;
import java.util.Scanner;


/**
 * Launch the client:
 * Ask the user for destination and departure addresses
 * Call RoutingServer to compute the itinerary that will be enqueued
 * Dequeued and print the message which corresponds to the steps of the itinerary
 *
 * @author Ayoub IMAMI
 */
public class Main {

    public static void main(String[] args) {

        // Instance of Itinerary and IItinerary to call a methode from the RoutingServer
        Itinerary itinerary = new Itinerary();
        IItinerary iItinerary = itinerary.getBasicHttpBindingIItinerary();

        // Scanner to read the user inputs
        Scanner scanner = new Scanner(System.in);

        // Ask for the destination address
        System.out.println("Enter your destination address:");
        String destinationAddress = scanner.nextLine();
        lineBreak();

        // Ask for the departure address
        System.out.println("Enter your departure address:");
        String originAddress = scanner.nextLine();
        lineBreak();

        // Call the method from the RoutingServer
        iItinerary.getItinerary(destinationAddress, originAddress);

        // Retrieve the itinerary steps from the queue
        try {

            // Create a ConnectionFactory
            ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://localhost:61616");

            // Create a Connection
            Connection connection = connectionFactory.createConnection();
            connection.start();

            // Create a Session
            Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

            // Create the destination (Topic or Queue)
            Destination destination = session.createQueue("DirectionsSteps");

            // Create a MessageConsumer from the Session to the Topic or Queue
            MessageConsumer consumer = session.createConsumer(destination);

            // Wait for a message
            Message message = consumer.receive();

            if (message instanceof TextMessage textMessage) {
                String text = textMessage.getText();
                System.out.println("Received: \n" + text);
            } else {
                System.out.println("Received: \n" + message);
            }

            consumer.close();
            session.close();
            connection.close();
        } catch (Exception e) {
            System.out.println("Caught: " + e);
            e.printStackTrace();
        }
    }

    /**
     * Break a line in the output for more visibility
     */
    private static void lineBreak() {
        System.out.println("\n");
    }
}
