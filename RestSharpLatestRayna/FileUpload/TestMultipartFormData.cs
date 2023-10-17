using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.FileUpload
{
    [TestClass]
    public class TestMultipartFormData
    {
        private readonly string BasePath = "http://localhost:9191/";

        [TestMethod]
        public void File_Upload()
        {
            var client = new RestClient(BasePath);
            var fileUploadRequest = new RestRequest()
            {
                Resource = "/normal/webapi/upload",
                Method = Method.Post
            };
            var fileContent = File.ReadAllBytes(@"C:\Data\log\TestData.xlsx");
            //I am probably skipping some lectures and I will ... I don't know
            fileUploadRequest.AddFile("file", fileContent, "TestData.xlsx", "multipart/form-data");
            var response = client.Execute(fileUploadRequest);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
    //Here I will paste the answer to the assignemn before lectrue 199.
//    Use the AddFile() API to upload the file from the local system.In the AddFile() API pass the complete path to the file you wish to upload.

//using FluentAssertions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using RestSharp;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RestSharpLatest.Assignment.File_Upload_using_RestSharp_api
//    {
//        [TestClass]
//        public class Assignment_Post_File_Upload
//        {
//            private readonly string BasePath = "http://localhost:9191/";

//            // Assignment - File Upload from the local file system
//            [TestMethod]
//            public void File_Upload()
//            {
//                // File to upload
//                var filePath = @"C:\Data\log\TestData.xlsx";

//                // Create the client
//                var client = new RestClient(BasePath);

//                // Create the Request
//                var fileUploadRequest = new RestRequest()
//                {
//                    Resource = "normal/webapi/upload",
//                    Method = Method.Post
//                };

//                // call the Add file api and pass complete path to the file.
//                fileUploadRequest.AddFile("file", filePath, "multipart/form-data");

//                // send the request
//                var resposne = client.Execute(fileUploadRequest);

//                // verify the response status code.
//                resposne.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

//                // Release the resource acquired by the client.
//                client.Dispose();
//            }
 //      }
 //   }
}
