using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using FluentAssertions;
using System.Diagnostics;
using RestSharp.Serializers;
using WebServiceAutomation.Model.JsonModel;
using RestSharpLatestRayna.APIModel.JsonApiModel;
using RestSharpLatestRayna.APIHelper;
using RestSharpLatestRayna.APIHelper.Client;
using RestSharpLatestRayna.APIHelper.APIRequest;
using RestSharpLatestRayna.APIHelper.Command;
using WebServiceAutomation.Model.XmlModel;
using System.Collections.Generic;

namespace RestSharpLatestRayna.PostRequest
{
    [TestClass]
    public class TestPostWithTracerClient
    {
        private string postUrl = "http://localhost:8081/laptop-bag/webapi/api/add";
        private Random random = new Random();
        private static IClient _client;
        private static RestApiExecutor _executor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _client = new TracerClient();
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public void TestPostRequestWithStringBody()
        {
            int id = random.Next(1000);
            string jsonData = "{\"BrandName\":\"Dell1\",\"Features\":{\"Feature\":[\"8GBRAM\"" +
                ",\"Rayna'sFeature\",\"1TBHardDrive\"]},\"Id\":" + id + "," +
                "\"LaptopName\":\"Latitude1\"}";
            //Create the Client
            RestClient client = new RestClient();

            //Create the request 
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };
            //Add the body to the request

            request.AddStringBody(jsonData, DataFormat.Json);
            //Send the request

            RestResponse response = client.ExecutePost(request);
            //store the response from this call in a variable
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            //response.Should().NotBeNull();  
            Debug.WriteLine(response.Content);

        }
        [TestMethod]
        public void TestPostRequestWithJsonObject()
        {
            int id = random.Next(1000);

            //RestSharp framework will 
            //1. Serialize the object into the JSON or XML representation
            //2. Add the Content-Type header
            //3. Add the serialized obejct to the request

            //------Old model-----
            //var payload = new JsonRootObjectBuilder().WithId(id).WithBrandName("RaynaTest").
            //    WithLaptopName("GoodLaptop").WithFeatures(new System.Collections.Generic.List<string>()
            //    { "Feature1", "Feature2" }).Build();

            var payload = new JsonModelBuilder().WithId(id).WithBrandName("RaynaTest").
               WithLaptopName("GoodLaptop").WithFeatures(new System.Collections.Generic.List<string>()
               { "Feature1", "Feature2" }).Build();

            RestClient client = new RestClient();
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };
            request.AddJsonBody(payload);
            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);


        }
        [TestMethod]
        public void TestPostRequestWithFramework_Json()
        {
            //create object for the request payload
            int id = random.Next(1000);
            var payload = new JsonModelBuilder().WithId(id).WithBrandName("RaynaTest").
              WithLaptopName("GoodLaptop").WithFeatures(new System.Collections.Generic.List<string>()
              { "Feature1", "Feature2" }).Build();

            //create the POST request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<JsonModel>
                (payload, RequestBodyType.JSON);

            //Command

            var command = new RequestCommand(request, _client);
            //SetCommand

            _executor.SetCommand(command);
            //Execute the request
            //File>Advanced>Format document
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("GoodLaptop");

        }
        [TestMethod]
        public void TestPostRequestWithFramework_String()
        {
            //create object for the request payload
            int id = random.Next(1000);
            string jsonData = "{\"BrandName\":\"Dell1\",\"Features\":{\"Feature\":[\"8GBRAM\"" +
                ",\"Rayna'sFeature\",\"1TBHardDrive\"]},\"Id\":" + id + "," +
                "\"LaptopName\":\"Latitude1\"}";

            //create the POST request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>
                (jsonData, RequestBodyType.STRING);

            //Command

            var command = new RequestCommand(request, _client);
            //SetCommand

            _executor.SetCommand(command);
            //Execute the request
            //File>Advanced>Format document
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Rayna'sFeature");

            var responseType = _executor.ExecuteRequest<JsonModel>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("Rayna'sFeature");

        }
        [TestMethod]
        public void TestPostRequestWithXML_String()
        {
            //create object for the request payload
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit Bulgarian</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";
            //Create the Client
            RestClient client = new RestClient();

            //Create the request 
            RestRequest request = new RestRequest()
            {
                Resource = postUrl,
                Method = Method.Post
            };

            request.AddStringBody(xmlData, DataFormat.Xml);
            request.AddHeader("Accept", "application/xml");


            var response = client.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);
            client.Dispose();
        }
        [TestMethod]
        public void TestPostRequestWithFramework_Xml()
        {
            //create object for the request payload
            int id = random.Next(1000);
            var xmlPayload = new XmlModelBuilder().WithId(id).WithBrandName("TestBrand Name").
                WithFeatures(new List<string>()
           {"Feature 1", "Feature 2" }).WithLaptopName("Test Laptop Name").Build();

            //create the POST request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<Laptop>
                (xmlPayload, RequestBodyType.XML).WithHeaders(new Dictionary<string, string>()
                { { "Accept", "application/xml"} });

            //Command

            var command = new RequestCommand(request, _client);
            //SetCommand

            _executor.SetCommand(command);
            //Execute the request
            //File>Advanced>Format document
            var response = _executor.ExecuteRequest();
            //response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test Laptop Name");

        }
        [TestMethod]
        public void TestPostRequestWithFrameworkXML_String()
        {
            //create object for the request payload
            int id = random.Next(1000);
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit Bulgarian</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id> " + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";

            //create the POST request

            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>
                (xmlData, RequestBodyType.STRING, ContentType.Xml).WithHeaders(new Dictionary<string, string>()
                { { "Accept", "application/xml"} }); ;

            //Command

            var command = new RequestCommand(request, _client);
            //SetCommand

            _executor.SetCommand(command);
            //Execute the request
            //File>Advanced>Format document
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Windows 10 Home 64 - bit Bulgarian");

            var responseType = _executor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("Windows 10 Home 64 - bit Bulgarian");

        }
    }
}
