package generated;

public class Main {

    public static void main(String[] args) {
        Itinerary itinerary = new Itinerary();
        IItinerary iItinerary = itinerary.getBasicHttpBindingIItinerary();

        final String footDestinationAddress = "Polytech Nice-Sophia, 06410 Biot";
        final String footOriginAddress = "Lycée Polyvalent Leonard de Vinci, 06600 Antibes";

        String steps = iItinerary.getItinerary(footDestinationAddress, footOriginAddress);
        System.out.println(steps);
    }
}
