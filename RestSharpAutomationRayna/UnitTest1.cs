using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace RestSharpAutomationRayna
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            /**
             1. Create the client
                -open restSharp DLL -> in MSTestProject we search for Rest Sharp > Rest Sharp again > Rest CLient class
                -implements IRestClient interface
            2. Create the request 
                 -open restSharp DLL -> in MSTestProject we search for Rest Sharp > Rest Sharp again > Rest Request class
                -implements IRestRequest interface
            3. Send the request using the client
            4. Capture the response 
            */

            IRestClient restClient= new RestClient();   
            IRestRequest restRequest= new RestRequest();

            //GetRequest 

        }
    }
}
