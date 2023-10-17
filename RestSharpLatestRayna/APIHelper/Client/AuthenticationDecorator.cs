using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.APIHelper.Client
{
     class AuthenticationDecorator : IClient
    {
        private readonly IClient _client;
        private readonly AuthenticatorBase _authenticatorBase; //initialized inside the constructor

        public AuthenticationDecorator(IClient client, AuthenticatorBase authenticator) //Defautl CLient //Tracer Client
        {
            _client = client;
            _authenticatorBase = authenticator;
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            //1. Invoke _client.GetClient() API

            var newClient = _client.GetClient(); //Plain Rest Client//Rest CLient + Tracer

            //2. Add the auth configuration
            newClient.Authenticator = _authenticatorBase;
            //3. return the new client
            return newClient;//Plain Rest Client + Auth Config
            //RestCLient + tracer + Auth Config
        }
    }
}
