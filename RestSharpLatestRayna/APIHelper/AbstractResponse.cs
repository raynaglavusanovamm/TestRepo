using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model;
using RestResponse = RestSharp.RestResponse;

namespace RestSharpLatestRayna.APIHelper
{
    public abstract class AbstractResponse : IResponse
    {
        private readonly RestResponse _restResponse;
        //we will use the constructor inside the AbstractResponse class to 
        //initialize the _restResponse variable 

        public AbstractResponse(RestSharp.RestResponse restResponse)
        {
            _restResponse = restResponse;
        }
        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }

        public abstract string GetResponseData();
    }

    public abstract class AbstractResponse<T> : IResponse<T>
    {
        private readonly RestResponse<T> _restResponse;
        //we will use the constructor inside the AbstractResponse class to 
        //initialize the _restResponse variable 

        public AbstractResponse(RestResponse<T> restResponse)
        {
            _restResponse = restResponse;
        }
        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }

        public abstract T GetResponseData();

    }
}
