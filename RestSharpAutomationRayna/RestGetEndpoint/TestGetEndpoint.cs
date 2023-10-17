using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharpAutomationRayna.RestGetEndpoint
{
    [TestClass]
    public class TestGetEndpoint
    {
        private string getUrl = "http://localhost:8081/laptop-bag/webapi/api/all";

        [TestMethod]
        public void TestGetUsingRestSharp()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);

            //we created the private string with the url which we found in 
            // file:///C:/Users/rayna.glavusanova/Desktop/RestSharp%20Auto%20Course/Course_Resource/Course_Resource/java-doc/index.html
            //Rebuild and run 
            //To capture the status code I will create a variable ... and store the value returned by the get method 
            IRestResponse restResponse = restClient.Get(restRequest);
            //Console.WriteLine(restResponse.IsSuccessful);
            //Console.WriteLine(restResponse.StatusCode);
            //Console.WriteLine(restResponse.ErrorMessage);
            //Console.WriteLine(restResponse.ErrorException);

            //Capture the response using 'IRestResponse' interface to extract the reponse content 
            //we comment the code above and add a conditional statement 

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Responde Content " + restResponse.Content);
                //response is coming in json format
            }
        }

        //how to receive the response content in different format 
        // I need to tell the app that I need special format data 
        //In postman it will be added as a header "Accept: application/json or application/xml
        //we need to attach this info as header 
        //there is an AddHeader method in this Interface (IRestRequest)
        [TestMethod]
        public void TestGetInXmlFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Responde Content " + restResponse.Content);
                //response is coming in xml format
            }
        }
        [TestMethod]

        public void TestGetInJsonFormat()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                Console.WriteLine("Responde Content " + restResponse.Content);
                //response is coming in xml format
            }
        }

        //Deserialization of the response content; 
        //Good practice is to deserialize the content in an object and use it after for validation 
        //RestSharp framework has the Feature of automatic deserialization
        //we need to specify the model only 
        /** RestSharpAutomationRayna has dependency on WebServiceAutomation; We will add this dependency in the
         * References section in the current project by right click > Add reference > 
         * Projects > select WebServiceAutomation > OK
         * You cam access all classes from the WebServiceAutomation 

*/
        [TestMethod]

        public void TestGetWithJson_Deserialize()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<List<JsonRootObject>> restResponse = restClient.Get<List<JsonRootObject>>(restRequest);
            //the return type of this method above is the type that was passed along with the method 

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                //first validation is on the status code AreEqual(Expected,(typecast)Actual)
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                //second one will be on the data/object attributes
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                //the property will return us the model after deserialization
                Console.WriteLine("Size of List " + restResponse.Data.Count);
                List<JsonRootObject> data = restResponse.Data;
                JsonRootObject jsonRootObject = data.Find((x) => //this is anonymous function 
                 {
                     return x.Id == 3; //this function will get executed for every entry
                                       //in list and return single object if the condition is true
                 });
                //Add validation of Json response 
                Assert.AreEqual("Latitude1", jsonRootObject.LaptopName);
                Assert.IsTrue(jsonRootObject.Features.Feature.Contains("Rayna's Feature"), "Element is not present");
            }
            else
            {
                Console.WriteLine("Error Msg " + restResponse.ErrorMessage);
                Console.WriteLine("Stack Trace " + restResponse.ErrorException);
            }
        }
        [TestMethod]

        public void TestGetWithXml_Deserialize()
        {
            //this time we will change the headers to accept XML
            //we will also change te model to XML model 
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);
            restRequest.AddHeader("Accept", "application/xml");
            //we need to add additional steps to deserialize xml format response
            //and we will use a deserializer method under the headers 

            var dotNetXmlDeserializer = new RestSharp.Deserializers.DotNetXmlDeserializer();
            //IRestResponse<LaptopDetailss> restResponse = restClient.Get<LaptopDetailss>(restRequest);
            //the return type of this method above is the type that was passed along with the method 
            IRestResponse restResponse = restClient.Get(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Status Code " + restResponse.StatusCode);
                //first validation is on the status code AreEqual(Expected,(typecast)Actual)
                Assert.AreEqual(200, (int)restResponse.StatusCode);
                //second one will be on the data/object attributes

                LaptopDetailss data = dotNetXmlDeserializer.Deserialize<LaptopDetailss>(restResponse);
                Console.WriteLine("Size of List " + data.Laptop.Count);

                Laptop laptop = data.Laptop.Find((x) =>
                  {
                      return x.Id.Equals("1", StringComparison.OrdinalIgnoreCase);
                      //this function will get evaluated for every entry in the list.
                      //And return the single object when the anonymous function for an entry returns true
                  });
                Assert.AreEqual("Latitude1", laptop.LaptopName);
                Assert.IsTrue(laptop.Features.Feature.Contains("1TB Hard Drive"), "Element is not present");


            }
            else
            {
                Console.WriteLine("Error Msg " + restResponse.ErrorMessage);
                Console.WriteLine("Stack Trace " + restResponse.ErrorException);
            }
        }

        //Request and Response using Execute API
        //2 versions; Execute <T> with type parameter will automatically deserialize the response content 

        [TestMethod]

        public void TestGetWithExecute()
        {
            //this time we will change the headers to accept XML
            //we will also change te model to XML model 
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest()
            {
                Method = Method.GET, //probably there is a difference? Maybe we ...
                Resource = getUrl
            };

            restRequest.AddHeader("Accept", "application/json");
            IRestResponse<List<Laptop>> restResponse = restClient.Execute<List<Laptop>>(restRequest);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsNotNull(restResponse.Data, "Response is null");

        }
    }
}
