using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EndtoEnd.MoqTests
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public Uri BaseAddress
        {
            get
            {
                return _client.BaseAddress;
            }
        }
    }
}
