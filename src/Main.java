import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        try {
            // Retrieve RMI registry
            Registry registry = LocateRegistry.getRegistry(2000);

            // Searching the remote object stub on the registry
            IItinerary itineraryStub = (IItinerary) registry.lookup("IItinerary");

            // Adding user interface
            UserInterface userInterface = new UserInterface();

            // Will ask the user for his destination and origin
            List<String> addresses = userInterface.askForOriginAndDestination();
            String destination = addresses.get(0);
            String origin = addresses.get(1);

            // Get an itinerary
            itineraryStub.getItinerary(destination, origin);
        }
        catch (Exception e) {
            System.err.println(e.toString());
            e.printStackTrace();
        }
    }
}
