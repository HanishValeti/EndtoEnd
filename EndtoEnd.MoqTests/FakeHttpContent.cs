using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EndtoEnd.MoqTests
{
    public class FakeHttpContent : HttpContent
    {
        public object Content { get; set; }

        public FakeHttpContent(object content)
        {
            Content = content;
        }

        protected async override Task SerializeToStreamAsync(Stream stream,
            TransportContext context)
        {
            MemoryStream ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, Content);
            await ms.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = Content.ToString().Length;
            return true;
        }
    }
}
