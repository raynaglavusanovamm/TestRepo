using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.APIHelper
{
    public class RestApiExecutor
        //the responsibility of this class is to execute the given command 

        //(RequestCommand)
    {
        private ICommand Command;

        public void SetCommand(ICommand _command) 
        
        { 
            Command = _command;
        
        }
        //This will be the class for executing the request and receiving the response
        public IResponse ExecuteRequest()
        {
            return Command.ExecuteRequest();
        } //for response in string format
        public IResponse<T> ExecuteRequest<T>()
        {
            return Command.ExecuteRequest<T>();

        }//for the deserialized object from response

        public byte[] DownloadData()
        {
            return Command.DownloadData();  //we delegate the method to the Command class implementation

        }

        public Task<byte[]> DownloadDataAsync()
        {
            return Command.DownloadDataAsync();  //we delegate the method to the Command class implementation

        }
    }
}
