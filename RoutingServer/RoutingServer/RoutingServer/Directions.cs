
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
