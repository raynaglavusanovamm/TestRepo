using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.APIHelper.Client
{
    public abstract class AbstractRequest

    {
        public abstract RestRequest Build(); //idea is to provide instance of the rest request
        
        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }
        protected virtual void WithHeaders(Dictionary<string,string>header,RestRequest restRequest)
        {
            foreach(string key in header.Keys)
            {
                restRequest.AddOrUpdateHeader(key, header[key]);
            }
        }
        //public abstract void WithUrl();
        //public abstract void WithQueryParam();       
        //public abstract void WithUrlSegments();

        //Query params
        protected virtual void WithQueryParameters(Dictionary<string, string> parameters, RestRequest restRequest)
        {
            foreach (string key in parameters.Keys)
            {
                restRequest.AddParameter(key, parameters[key]);
            }
        }
    }

 
}
