using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;
using EndtoEnd.Controllers;
using EndtoEnd.Entity;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using UrlHelper = System.Web.Mvc.UrlHelper;

namespace EndtoEnd.MoqTests
{
    [TestFixture]
    public class UnitTestForMvcController
    {
        //protected Mock<HttpClient> Httpclientmock;
        //protected IHttpClientWrapper Httpclientwrapper;
        //protected HttpClient Httpclientmock;

        /*[TestFixtureSetUp]
        public void TestInitialize()
        {
            Httpclientmock = new Mock<HttpClient>();
            Httpclientwrapper = new HttpClientWrapper();
        }*/

        protected SecurityMfMvcController SetupController()
        {
            System.Uri uri = new System.Uri("http://localhost:55893/api/");
            /*Httpclientmock.Setup(
                x => x.GetAsync("http://localhost:55893/api/" + "SecuritiesWebApiMf").Result);*/
            //var fakeHandler = new FakeHttpMessageHandler();
            //Httpclient = new HttpClient(fakeHandler) {BaseAddress = uri};
            //Httpclientmock.Setup(x => x.BaseAddress).Returns(Httpclientwrapper.BaseAddress);
            //Httpresponsemessagemock = new Mock<HttpResponseMessage>();
            MockHttpMessageHandler helper = new MockHttpMessageHandler(
            () => new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                //Content = new StringContent(JsonConvert.SerializeObject(document))
            }
            );
            using (HttpClient client = new HttpClient(helper))
            {
                var controller = new SecurityMfMvcController(client);
                return controller;
            }
        }

        [Test]
        public void ActionAllSecuritiesHttpStatusCodeOkay()
        {
            
            var controller = SetupController();
            var result = controller.SecuritiesMf();
            //Assert.AreEqual(Httpresponsemessagemock.Object.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;
            Assert.IsTrue(viewResult != null && viewResult.Model is List<SecurityMutualFundDto>);
            //Httpclientmock.VerifyAll();
            //Httpresponsemessagemock.VerifyAll();
        }

        class MockHttpMessageHandler : HttpMessageHandler
        {
            private Func<HttpRequestMessage, System.Threading.CancellationToken, Task<HttpResponseMessage>> funcToOverride { get; set; }
 
            private MockHttpMessageHandler() { }
 
            public MockHttpMessageHandler(Func<HttpResponseMessage> sendFunc)
            {
                funcToOverride = (httpRequestMessage, cancellationToken) =>
                                 {
                                     return Task<HttpResponseMessage>.Factory.StartNew(sendFunc);
                                 };
            }
 
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                return funcToOverride.Invoke(request, cancellationToken);
            }
         }
    }
}
