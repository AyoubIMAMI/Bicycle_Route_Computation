package generated;

import org.apache.activemq.ActiveMQConnectionFactory;

import javax.jms.*;
import java.util.Scanner;

import static java.lang.Thread.sleep;

public class Main {

    //Addresses examples:
    /*
    "Polytech Nice-Sophia, 06410 Biot"
    "Lycée Polyvalent Leonard de Vinci, 06600 Antibes"
    "Rouen"
    "Besancon"
    "Dieweg 69, 1180 Uccle, Belgique"
    */

    public static void main(String[] args) {

        Itinerary itinerary = new Itinerary();
        IItinerary iItinerary = itinerary.getBasicHttpBindingIItinerary();

        Scanner scanner = new Scanner(System.in);

        System.out.println("Enter your destination address:");
        String destinationAddress = scanner.nextLine();
        lineBreak();

        System.out.println("Enter your departure address:");
        String originAddress = scanner.nextLine();
        lineBreak();

        iItinerary.getItinerary(destinationAddress, originAddress);

        try {

            // let some time to the server to compute and enqueued the directions steps
            sleep(1000);

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
     * Break a line for more visibility
     */
    private static void lineBreak() {
        System.out.println("\n");
    }
}
