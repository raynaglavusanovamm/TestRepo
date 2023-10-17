using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.APIHelper
{
    public interface IClient : IDisposable //from System .Net framework
        //Dispose method in the Disposable interface provides
        //a mechanism for releasing unmanaged resources 
    {
        RestClient GetClient();
      //  void UseXml();
    }
}
