import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class UserInterface {
    private final Scanner scanner;

    UserInterface() {
        scanner = new Scanner(System.in);
    }

    /**
     * Ask the user to enter his destination and origin addresses
     * @return a list containing the destination and the origin addresses
     */
    public List<String> askForOriginAndDestination() {
        List<String> addresses = new ArrayList<>();
        String originAddress = "";
        String destinationAddress = "";

        while(!originAddress.equals(":q") && !destinationAddress.equals(":q")) {
            System.out.println("Please enter your destination : (press :q to quit)");
            destinationAddress = scanner.nextLine();
            addresses.add(destinationAddress);

            System.out.println("Please enter your origin : (press :q to quit)");
            originAddress = scanner.nextLine();
            addresses.add(originAddress);
        }

        return addresses;
    }
}
