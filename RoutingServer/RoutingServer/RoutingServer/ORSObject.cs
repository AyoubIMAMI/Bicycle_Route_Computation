/**
 * Different classes needed to build OpenRouteService objects
 */
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

    public Properties properties { get; set; }
}

public class Geometry
{
    public float[] coordinates { get; set; }
}


// DIRECTIONS

public class ORSDirections
{
    public FeatureDirections[] features { get; set; }
    public Metadata metadata { get; set; }
}

public class Metadata
{
    public QueryDirections query { get; set; }
}

public class QueryDirections
{
    public string profile { get; set; }
}

public class FeatureDirections
{
    public Properties properties { get; set; }
}

public class Properties
{
    public Segment[] segments { get; set; }

    public string locality { get; set; }
}

public class Segment
{
    public float distance { get; set; }
    public float duration { get; set; }
    public Step[] steps { get; set; }
}

public class Step
{
    public float distance { get; set; }
    public float duration { get; set; }
    public string instruction { get; set; }
    public string name { get; set; }
}
