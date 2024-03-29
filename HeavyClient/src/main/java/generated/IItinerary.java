
package generated;

import javax.jws.WebMethod;
import javax.jws.WebParam;
import javax.jws.WebResult;
import javax.jws.WebService;
import javax.xml.bind.annotation.XmlSeeAlso;
import javax.xml.ws.RequestWrapper;
import javax.xml.ws.ResponseWrapper;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.3.2
 * Generated source version: 2.2
 * 
 */
@WebService(name = "IItinerary", targetNamespace = "http://tempuri.org/")
@XmlSeeAlso({
    ObjectFactory.class
})
public interface IItinerary {


    /**
     * 
     * @param destinationAddress address to which the user wants to go
     * @param originAddress address from which the user leaves
     *     returns java.lang.String
     */
    @WebMethod(operationName = "GetItinerary", action = "http://tempuri.org/IItinerary/GetItinerary")
    @WebResult(name = "GetItineraryResult", targetNamespace = "http://tempuri.org/")
    @RequestWrapper(localName = "GetItinerary", targetNamespace = "http://tempuri.org/", className = "generated.GetItinerary")
    @ResponseWrapper(localName = "GetItineraryResponse", targetNamespace = "http://tempuri.org/", className = "generated.GetItineraryResponse")
    void getItinerary(
        @WebParam(name = "destinationAddress", targetNamespace = "http://tempuri.org/")
        String destinationAddress,
        @WebParam(name = "originAddress", targetNamespace = "http://tempuri.org/")
        String originAddress);

}
