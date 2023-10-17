using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAutomationRayna.HelperClass.Request
{
    public class RestClientHelper
    {
        private IRestClient GetRestClient()
        {
            IRestClient restClient = new RestClient();
            return restClient;
        }

        private IRestRequest GetRestRequest(string url, Dictionary<string, string> headers, Method method)
        {
            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };

            foreach(string key in headers.Keys) 
            { 
                restRequest.AddHeader(key, headers[key]);
            
            }
            return restRequest;
        }

        private IRestResponse SendRequest(IRestRequest restRequest)
            //zashto davam agrument Interface interfaceinstance, a ne direktno restRequest???
        {
            IRestClient restClient = GetRestClient();
            IRestResponse restResponse = restClient.Execute(restRequest);//returns untyped response
            return restResponse;

            //RestResponse restResponse = new RestResponse()
            //pyrvo mi suggestna restResponse da si napravq nova instanciq?
        }

        //we will create another version of the send request we will send the Type parameter SendRequest<>
        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) where T : new()
        {
            IRestClient restClient = GetRestClient();
            IRestResponse<T> restResponse = restClient.Execute<T>(restRequest);//returns typed response
            return restResponse;
        }

    }
}
