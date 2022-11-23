public class ORSGeocode
{
    public Geocoding geocoding { get; set; }
    public Feature[] features { get; set; }
}

public class Geocoding
{
    public Query query { get; set; }
}

public class Query
{
    public Parsed_Text parsed_text { get; set; }
}

public class Parsed_Text
{
    public string city { get; set; }
}

public class Feature
{
    public Geometry geometry { get; set; }
}

public class Geometry
{
    public float[] coordinates { get; set; }
}