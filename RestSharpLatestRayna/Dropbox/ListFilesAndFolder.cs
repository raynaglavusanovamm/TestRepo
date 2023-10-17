using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using RestSharp;
using RestSharpLatestRayna.Dropbox.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RestSharpLatestRayna.APIHelper;
using RestSharpLatestRayna.APIHelper.Client;
using RestSharpLatestRayna.APIHelper.APIRequest;
using RestSharpLatestRayna.APIHelper.Command;

namespace RestSharpLatestRayna.Dropbox
{
    [TestClass]
    public class ListFilesAndFolder
    {
        public readonly string BaseUrl = "https://api.dropboxapi.com/2";
        public static readonly string Token = "sl.BnCGl79N5obQ7lGNeK9TPT5th0E1Pm-aeh06J9n2dyaiNxlEd5oikoE62XIGL7zasaYSRV7LkCDogD28fqWz_p2fL7Z8X3De3jAqamth3U-3EwPbhFvETGw6O1wfQVHQ2F4B6XYe93Xi";
        private static  IClient client; 
        private static IClient authClient;
        private static  RestApiExecutor apiExecutor;

        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            client = new TracerClient();
            authClient = new AuthenticationDecorator(client,new
                JwtAuthenticator(Token));
            //authClient = new AuthenticationDecorator(client, new
            //HttpBasicAuthenticator(" "," ")); For basic authentication

            apiExecutor = new RestApiExecutor();
        }

        [ClassCleanup]

        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        
        public void GetAllFilesAndFolder()
        {
            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false,\"include_has_explicit_shared_members\":false,\"include_media_info\":false,\"include_mounted_folders\":true,\"include_non_downloadable_files\":true,\"path\":\"\",\"recursive\":false}";

            var client = new RestClient()
            {
                Authenticator = new JwtAuthenticator(Token)
            };

            var request = new RestRequest()
            {
                Resource = BaseUrl + contextPath,
                Method = Method.Post
            };
            request.AddStringBody(requestBody, DataFormat.Json);
            var response = client.ExecutePost<Root>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            client.Dispose();
        }

        [TestMethod]
        public void GetAllFilesAndFolder_with_Framework ()
        {
            //request body

            var contextPath = "/files/list_folder";

            var requestBody = "{\"include_deleted\":false,\"include_has_explicit_shared_members\":false,\"include_media_info\":false,\"include_mounted_folders\":true,\"include_non_downloadable_files\":true,\"path\":\"\",\"recursive\":false}";

            //post request builder 
            var postrequest = new PostRequestBuilder().WithUrl
                (BaseUrl + contextPath).WithBody(requestBody, RequestBodyType.STRING);

            //request command
            var command = new RequestCommand(postrequest, authClient);
            //set the command on api executor
            apiExecutor.SetCommand(command);
            //execute the request
            var response = apiExecutor.ExecuteRequest<Root>();
            //we will deserialize the response into an object of type Root
            //validate the response status
            response.GetHttpStatusCode().Should().Be
                (System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void CreateFolder_with_framework()
        {
            //request body

            var contextPath = "/files/create_folder_v2";

            var requestBody = "{\"autorename\":true,\"path\":\"/TestFolder\"}";
            //post request builder 
            var postrequest = new PostRequestBuilder().WithUrl
                (BaseUrl + contextPath).WithBody(requestBody, RequestBodyType.STRING);

        //request command
        var command = new RequestCommand(postrequest, authClient);
        //set the command on api executor
        apiExecutor.SetCommand(command);
            //execute the request
            var response = apiExecutor.ExecuteRequest<Root>();
        //we will deserialize the response into an object of type Root
        //validate the response status
        response.GetHttpStatusCode().Should().Be
            (System.Net.HttpStatusCode.OK);
    }
    }
}
