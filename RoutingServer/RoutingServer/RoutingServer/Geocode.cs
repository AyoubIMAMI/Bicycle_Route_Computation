
public class Geocode
{
    public Feature[] features { get; set; }
}

public class Feature
{
    public Geometry geometry { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public float[] coordinates { get; set; }
}