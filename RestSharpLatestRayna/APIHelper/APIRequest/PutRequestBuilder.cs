using RestSharp;
using RestSharpLatestRayna.APIHelper.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.APIHelper.APIRequest
{
    public  class PutRequestBuilder : AbstractRequest
    {
         
        private readonly RestRequest _restrequest;
        public PutRequestBuilder()
        {
            _restrequest = new RestRequest()
            {
                Method = Method.Put
            };
        }
        public override RestRequest Build()
        {
            return _restrequest;
        }
        //URL
        public PutRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restrequest);
            return this;
        }

        //Headers 
        public PutRequestBuilder WithHeaders(Dictionary<string, string> headers)
        {
            WithHeaders(headers, _restrequest);
            return this;
        }

        //Body 
        public PutRequestBuilder WithBody<T>(T body,
            RequestBodyType bodyType, string contentType =
            RestSharp.Serializers.ContentType.Json) where T : class
        {
            //String 
            //Object
            switch (bodyType)
            {
                case RequestBodyType.STRING:
                    _restrequest.AddStringBody(body.ToString(), contentType);
                    break;
                case RequestBodyType.JSON:
                    _restrequest.AddJsonBody<T>(body);
                    break;
                case RequestBodyType.XML:
                    _restrequest.AddXmlBody<T>(body);
                    break;
            }
            return this;
        }
        //Query parameter
        public PutRequestBuilder WithQueryParameters(Dictionary
            <string, string> parameters)
        {
            WithQueryParameters(parameters, _restrequest);
            return this;
        }
        protected override void WithQueryParameters(Dictionary<string, string>
            parameters, RestRequest restRequest)
        {
            foreach (string key in parameters.Keys)
            {
                restRequest.AddQueryParameter(key, parameters[key]);
            }

        }

    }
}

