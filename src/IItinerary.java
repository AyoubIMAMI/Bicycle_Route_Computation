import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IItinerary extends Remote {

    /**
     * Get itinerary between an origin and a destination
     * @param destinationAddress user destination address
     * @param originAddress user origin address
     * @throws RemoteException exception
     */
    void getItinerary(String destinationAddress, String originAddress) throws RemoteException;
}
