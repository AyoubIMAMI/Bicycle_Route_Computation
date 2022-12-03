using System;
using System.Collections.Generic;
using ProxyCach;

public class JCDItemStation
{
    List<JCDStation> stations;
    JCDecauxCall jCDecauxCall = new JCDecauxCall();

    public JCDItemStation(string contracts)
    {
        stations = jCDecauxCall.GetStationsFromContract(contracts).Result;
    }

    public List<JCDStation> GetStations()
    {
        return stations;
    }
}

public class JCDItemContract
{
    List<JCDContract> contracts;
    JCDecauxCall jCDecauxCall = new JCDecauxCall();

    public JCDItemContract()
    {
        contracts = jCDecauxCall.GetContracts().Result;
    }

    public List<JCDContract> GetContracts()
    {
        return contracts;
    }
}

public class JCDContract
{
    public string name { get; set; }

    public string[] cities { get; set; }
}

public class JCDStation
{
    public int number { get; set; }
    public string name { get; set; }
    public Position position { get; set; }
}

public class Position
{
    public Position(Double latitude, Double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }
    public Double latitude { get; set; }
    public Double longitude { get; set; }
}