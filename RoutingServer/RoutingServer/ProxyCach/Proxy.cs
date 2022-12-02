using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProxyCach
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Proxy : IProxy
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        public static readonly HttpClient client = new HttpClient();

        public void GetData(int value)
        {

        }
    }
}
