using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSeleniumPOMFramework.API
{
    class APICommon
    {

        static public IRestResponse ResponseFromGETrequest(string baseURL, string userName, string passWord, string resourceAndQuery)
        {

            var request = GETRequest(resourceAndQuery);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;
        }

        static public IRestResponse ResponseFromPostRequest(string baseURL, string userName, string passWord, string resourceAndQuery, string json)
        {

            var request = PostRequest(resourceAndQuery, json);
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;

        }

        static public IRestResponse ResponseFromPatchRequest(string baseURL, string userName, string passWord, string resourceAndQuery)
        {

            var request = PatchRequest(resourceAndQuery);
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;

        }


        static public IRestResponse ResponseFromDeleteRequest(string baseURL, string userName, string passWord, string resource)
        {

            var request = DeleteRequest(resource);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;
        }


        static public IRestResponse ResponseFromPutRequest(string baseURL, string userName, string passWord, string resource, string jsonString)
        {

            var request = PutRequest(resource, jsonString);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;
        }

        static public RestRequest DeleteRequest(string resource)
        {

            var request = new RestRequest(Method.DELETE);
            request.Resource = resource;
            request.AddHeader("content-type", "application/json");

            return request;
        }

        static public RestRequest PutRequest(string resource, string jsonString)
        {

            var request = new RestRequest(Method.PUT);
            request.Resource = resource;
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            request.AddHeader("content-type", "application/json");

            return request;
        }


        static public RestRequest GETRequest(string resourceAndQuery)
        {

            var request = new RestRequest(Method.GET);
            request.Resource = resourceAndQuery;
            request.AddHeader("content-type", "application/json");
            return request;
        }

        static public RestRequest PatchRequest(string resourceAndQuery)
        {

            var request = new RestRequest(Method.PATCH);
            request.Resource = resourceAndQuery;
            request.AddHeader("content-type", "application/json");
            return request;
        }

        static public RestRequest PostRequest(string resourceAndQuery, string json)
        {

            var request = new RestRequest(Method.POST);
            request.Resource = resourceAndQuery;
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            return request;
        }


        static public RestClient GetRestClient(string baseURL, string userName, string passWord)
        {


            var client = new RestClient();
            client.BaseUrl = new Uri(baseURL);
            //client.Authenticator = new NtlmAuthenticator(userName, passWord);
            client.Authenticator = new NtlmAuthenticator();

            return client;

        }


        static public IRestResponse ResponseFromPostRequestUsingJsonString(string baseURL, string userName, string passWord, string resource, string json)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = resource;
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            //Execute Request
            IRestResponse response = GetRestClient(baseURL, userName, passWord).Execute(request);
            return response;
        }


    }

}
