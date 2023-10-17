using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Authenticators;
using RestSharpLatestRayna.APIHelper.APIRequest;
using RestSharpLatestRayna.APIHelper.Client;
using RestSharpLatestRayna.APIHelper.Command;
using RestSharpLatestRayna.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FluentAssertions;
using RestSharp;

namespace RestSharpLatestRayna.Dropbox
{
    [TestClass]
    public class TestFileDownloadUsingFramework
    {

        private static RestApiExecutor restApiExecutor;
        private static IClient client;
        private static IClient authClient;
        private static string Token = "<Your Token>";
        private readonly string BasePath = "https://content.dropboxapi.com/2";

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new TracerClient();
            authClient = new AuthenticationDecorator(client, new JwtAuthenticator(Token));
            restApiExecutor = new RestApiExecutor();

        }

        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void DownloadFile_UsingFramework()
        {
            var fileName = "Video.mp4";
            var contextPath = "/files/download";
            var location = "{\"path\":\"/" + fileName + "\"}";

            var postRequest = new PostRequestBuilder().WithUrl(BasePath + contextPath).
                WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", location } });

            var command = new DownloadRequestCommand(postRequest, authClient);
            restApiExecutor.SetCommand(command);
            var data = restApiExecutor.DownloadData();
            File.WriteAllBytes("New_" + fileName, data);
            File.Exists("New_" + fileName).Should().BeTrue();
        }

        [TestMethod]
        public void DownloadInParallel_UsingFramework()
        {
            var contextPath = "/files/download";

            var fileNameOne = "Video.mp4";
            var locationOne = "{\"path\":\"/" + fileNameOne + "\"}";

            var fileNameTwo = "Part1.mp3";
            var locationTwo = "{\"path\":\"/" + fileNameTwo + "\"}";

            var postRequestOne = new PostRequestBuilder().WithUrl(BasePath + contextPath).
            WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationOne } });

            var postRequestTwo = new PostRequestBuilder().WithUrl(BasePath + contextPath).
            WithHeaders(new Dictionary<string, string>() { { "Dropbox-API-Arg", locationTwo } });

            //var command = new DownloadRequestCommand(postRequest, authClient);
            //restApiExecutor.SetCommand(command);

            var requestOneCommand = new DownloadRequestCommand(postRequestOne, authClient);
            restApiExecutor.SetCommand(requestOneCommand);
            var taskOne = restApiExecutor.DownloadDataAsync();

            var requestTwoCommand = new DownloadRequestCommand(postRequestOne, authClient);
            restApiExecutor.SetCommand(requestTwoCommand);
            var taskTwo = restApiExecutor.DownloadDataAsync();



            Task.WaitAll(taskOne, taskTwo);

            File.WriteAllBytes("New_" + fileNameOne, taskOne.Result);
            File.WriteAllBytes("New_" + fileNameTwo, taskTwo.Result);

            File.Exists("New_" + fileNameOne).Should().BeTrue();
            File.Exists("New_" + fileNameTwo).Should().BeTrue();
        }

        [TestMethod]
        public void DownloadFile()
        {
            
            //var client = new RestClient()
            //{
            //    Authenticator = new JwtAuthenticator(Token)
            //};

            //var request = new RestRequest()
            //{
            //    Resource = BasePath + contextPath,
            //    Method = Method.Post
            //};

            ////request.AddHeader("Dropbox-API-Arg", location);
            //var data = client.DownloadData(request);
            //File.WriteAllBytes(fileName, data);
            //client.Dispose();
        }


    }
}
