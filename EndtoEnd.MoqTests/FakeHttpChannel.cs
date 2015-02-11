using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EndtoEnd.MoqTests
{
    public class FakeHttpChannel<T> : HttpClientChannel
    {
        private readonly T _responseObject;
        public FakeHttpChannel(T responseObject)
        {
            this._responseObject = responseObject;
        }

        protected HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return new HttpResponseMessage()
            {
                RequestMessage = request,
                Content = new StreamContent(this.GetContentStream())
            };
        }

        private Stream GetContentStream()
        {
            var serializer = new DataContractSerializer(typeof(T));
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, this._responseObject);
            stream.Position = 0;
            return stream;
        }
    }
}
