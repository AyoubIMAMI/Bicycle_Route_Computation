
package generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour anonymous complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="destinationAddress" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="originAddress" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "destinationAddress",
    "originAddress"
})
@XmlRootElement(name = "GetItinerary")
public class GetItinerary {

    @XmlElementRef(name = "destinationAddress", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> destinationAddress;
    @XmlElementRef(name = "originAddress", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> originAddress;

    /**
     * Obtient la valeur de la propriété destinationAddress.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getDestinationAddress() {
        return destinationAddress;
    }

    /**
     * Définit la valeur de la propriété destinationAddress.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setDestinationAddress(JAXBElement<String> value) {
        this.destinationAddress = value;
    }

    /**
     * Obtient la valeur de la propriété originAddress.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getOriginAddress() {
        return originAddress;
    }

    /**
     * Définit la valeur de la propriété originAddress.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setOriginAddress(JAXBElement<String> value) {
        this.originAddress = value;
    }

}
