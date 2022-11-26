package generated;

public class Main {

    public static void main(String[] args) {
        Itinerary itinerary = new Itinerary();
        IItinerary iItinerary = itinerary.getBasicHttpBindingIItinerary();

        final String footDestinationAddress = "Polytech Nice-Sophia, 06410 Biot";
        final String footOriginAddress = "Lycée Polyvalent Leonard de Vinci, 06600 Antibes";

        final String bikeDestinationAddress = "Rouen";
        final String bikeOriginAddress = "Besancon";

        String footSteps = iItinerary.getItinerary(footDestinationAddress, footOriginAddress);
        System.out.println(footSteps);

        String bikeSteps = iItinerary.getItinerary(bikeDestinationAddress, bikeOriginAddress);
        System.out.println(bikeSteps);
    }
}
