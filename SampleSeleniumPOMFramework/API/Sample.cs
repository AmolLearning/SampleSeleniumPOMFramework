using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleSeleniumPOMFramework.API
{
    class Sample
    {

        static string baseUrl = "https://baseurl.com";
        static string resource = "/api/GetLocations";


        static public List<Location> GetLocations(string userName, string password)
        {
            IRestResponse response = APICommon.ResponseFromGETrequest(baseUrl, userName, password, resource);

            List<Location> allLocations = JsonConvert.DeserializeObject<List<Location>>(response.Content);
            return allLocations;

        }
    }
    class Location
    {
        int id { get; set; }
        string Name { get; set; }

    }

}
