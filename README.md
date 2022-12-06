# Go_Biking

## Author: Ayoub IMAMI

## Concept: The goal of the Let’s Go Biking! project is to develop a small app to compute itineraries by reducing as much as possible the distance to cover by foot (by using bikes instead).

### **How to launch the project:**

*First* - Open a cmd and run activemq with the "activemq start" command

*Then* - Run the self hosted RoutingServer and ProxyCach servers using the .exe

*Finally* - Open the HeavyClient java project in a IDE and Run the HeavyClient\src\main\java\generated\Main.java class

### **How to use the project:**
The app will ask for your destination and departure addresses, respectively. After computing the best itinerary, it will simply writes to you the different itinerary steps from your departure to your arrival, precising the moments where you have to walk and where you can use a bike.
