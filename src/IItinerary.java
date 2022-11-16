import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IItinerary extends Remote {

    void getItinerary(String destinationAddress, String originAddress) throws RemoteException;
}
