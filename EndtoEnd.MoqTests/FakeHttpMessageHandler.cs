using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EndtoEnd.MoqTests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            this._response = response;
        }

        protected override Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request,
                        CancellationToken cancellationToken)
        {
            var responseTask =
                new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(_response);

            return responseTask.Task;
        }
    }

    public class InMemoryHttpContentSerializationHandler : DelegatingHandler
    {
        public InMemoryHttpContentSerializationHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Content = await ConvertToStreamContentAsync(request.Content);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            response.Content = await ConvertToStreamContentAsync(response.Content);

            return response;
        }

        private async Task<StreamContent> ConvertToStreamContentAsync(HttpContent originalContent)
        {
            if (originalContent == null)
            {
                return null;
            }

            StreamContent streamContent = originalContent as StreamContent;

            if (streamContent != null)
            {
                return streamContent;
            }

            MemoryStream ms = new MemoryStream();
            await originalContent.CopyToAsync(ms);

            // Reset the stream position back to 0 as in the previous CopyToAsync() call,
            // a formatter for example, could have made the position to be at the end
            ms.Position = 0;

            streamContent = new StreamContent(ms);

            // copy headers from the original content
            foreach (KeyValuePair<string, IEnumerable<string>> header in originalContent.Headers)
            {
                streamContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return streamContent;
        }
    }
}
