using System;

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