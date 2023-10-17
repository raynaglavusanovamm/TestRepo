using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpLatestRayna.APIHelper;
using RestSharpLatestRayna.APIHelper.Client;
using RestSharpLatestRayna.APIHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Diagnostics;
using RestSharpLatest.APIHelper.APIRequest;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Contexts;

namespace RestSharpLatestRayna.GetRequest
{
    [TestClass]
    public class TestGetWithFramework

    {
        //one static var for IClient 
        private static IClient client;
        //one static var for RestAPiExecutor
        private static RestApiExecutor executor;
        //they are static because we will use a static method for initializing them 
        private readonly string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";
        //private readonly RestApiExecutor executor; // we initialize this variable in the SetUp method

        //this method should be executed first->
        [ClassInitialize]
        //[ClassInitialize] identifies a method that contains code that must
        //be used before any of
        //the tests in the test class have run and to allocate
        //resources to be used by the test class
        public static void SetUp(TestContext testContext)
        {
            client = new DefaultClient();
            executor = new RestApiExecutor();
        }

        [TestMethod]
        public void GetRequest()
        {
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl);

            ICommand getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            var response = executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var typeResponse = executor.ExecuteRequest<List<JsonRootObject>>();
            var rootObject = typeResponse.GetResponseData();
            var  rootObjectData =rootObject.Find((item) =>
            {
                return 1 == item.Id;
            });
            rootObjectData.BrandName.Should().NotBeNull();
            rootObjectData.LaptopName.Should().NotBeNull();
            //var response = client.Execute<List<JsonRootObject>>(getRequest);
            //we store the response of the request in a variable
            //var is used to declare implicitly typed local variable;
            //Means it tells the compiler to figure out the type
            //of the variable at compilation time

            //we will extract part the content in another variable 
            //var content = response.Data;
            //because the var content is of type List<JsonRootObject>
            //we extract a single object and add validation on that
      

        }
        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }
    }
}
