using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    internal class Geocode
    {
        public Geocode Features { get; }
    }

    internal class Features
    {
        public Geometry Geometry { get; }
    }

    internal class Geometry
    {
        public Coordinates Coordinates { get; }
    }

    internal class Coordinates
    {
        public double latitude { get; }
        public double longitude { get; }
    }
}
