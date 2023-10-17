using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatestRayna.APIHelper;
using RestSharpLatestRayna.APIHelper.Client;
using RestSharpLatestRayna.APIHelper.Command;
using System.Collections.Generic;
using FluentAssertions;
using System.Net;
using RestSharpLatest.APIHelper.APIRequest;

namespace RestSharpLatestRayna.GetRequest
{
    [TestClass]
    public class TestApiHelperGet
    {
        private IClient _client;
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";
        //private RestApiExecutor executor; // we initialize this variable in the SetUp method

        public void SetUp()
        {
            _client = new DefaultClient();
           // executor = new RestApiExecutor();

        }

        [TestMethod]
        public void GetRequestWithApiHelper() 
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept", "application/json" }
            };
            //as we are using the builder pattern we can invoke all methods using a chain
            AbstractRequest abstractRequest = new GetRequestBuilder().
                WithUrl(getUrl).WithHeaders(headers);
            //to retrieve the instance of the get request we should invoke the Build()
            //to get the instance of the RestRequest
            ICommand getCommand = new RequestCommand(abstractRequest, _client);
            //in order to execute the getCommand we need to invoke the SetUp method
            //executor.SetCommand(getCommand);
            //var response = executor.ExecuteRequest(); //and then store the response
            //                                          //of this method into a variable
            //response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);
        }

        public void TearDown()
        {
            _client?.Dispose();
        }
    }
}
