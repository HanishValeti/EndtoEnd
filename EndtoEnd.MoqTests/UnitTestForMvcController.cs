using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Mvc;
using EndtoEnd.Controllers;
using EndtoEnd.Entity;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;


namespace EndtoEnd.MoqTests
{
    [TestFixture]
    public class UnitTestForMvcController
    {
        [Test]
        public void TestActionListAll()
        {
            var uobj = new UnitTestForApiController();
            var testobj = uobj.GetsecuritiesMfsList();
            
            var responseMessage = new HttpResponseMessage
            {
                Content = new FakeHttpContent(testobj)
            };
            var messageHandler = new FakeHttpMessageHandler(responseMessage);
            HttpServer server = new HttpServer(messageHandler);
            using (var client = new HttpClient(new InMemoryHttpContentSerializationHandler(server)))
            {
                var uri = new System.Uri("http://localhost:55893/api/");
                client.BaseAddress = uri;
                var controller = new SecuritiesMfMvcController(client);
                var result = controller.Listall();
                Assert.IsNotNull(result);
            }
            
        }

        [Test]
        public void TestActionSelectById()
        {
            var config = new HttpConfiguration()
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            //use the configuration that the web application has defined
            WebApiConfig.Register(config);
            HttpServer server = new HttpServer(config);
            //create a client with a handler which makes sure to exercise the formatters
            using (var client = new HttpClient(new InMemoryHttpContentSerializationHandler(server)))
            {
                System.Uri uri = new System.Uri("http://localhost:55893/api/");
                client.BaseAddress = uri;
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response =
                //    client.GetAsync(ConfigurationManager.AppSettings["ApiUrl"] + "SecuritiesApiMf/GetById/" + 2155)
                //        .Result;
                //response.EnsureSuccessStatusCode();
                //SecurityMutualFundDto result = response.Content.ReadAsAsync<SecurityMutualFundDto>().Result;
                
                var controller = new SecuritiesMfMvcController(client);
                var result = controller.Select(2155);
                Assert.IsNotNull(result);
            }

        }

        [Test]
        public void TestActionNugetSelectById()
        {
            var mockHttp = new MockHttpMessageHandler();
            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://localhost:55893/api/")
                    .Respond("application/json", GenerateJsonArrayofSecurityMf()); // Respond with JSON

            // Inject the handler or client into your application code
            using (var client = new HttpClient(mockHttp))
            {
                System.Uri uri = new System.Uri("http://localhost:55893/api/");
                client.BaseAddress = uri;
                var controller = new SecuritiesMfMvcController(client);
                var result = controller.Select(2155);
                Assert.IsNotNull(result);
            }
        }

        [Test]
        public void TestActionFakeHandlerSelectById()
        {
            var uobj = new UnitTestForApiController();
            var testobj= uobj.GetsecuritiesMfsList();
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, testobj);
            var response = new HttpResponseMessage(HttpStatusCode.OK) 
            {Content = new StreamContent(stream)};

            using (var httpClient = new HttpClient(new FakeHandler
            {
                Response = response,
                InnerHandler = new HttpClientHandler()
            }))
            {
                System.Uri uri = new System.Uri("http://localhost:55893/api/");
                httpClient.BaseAddress = uri;
                var controller = new SecuritiesMfMvcController(httpClient);
                var result = controller.Select(2155) as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual(result.ViewName,"Select");
            }

            
        }
        private string GenerateJsonArrayofSecurityMf()
        {
            var uobj = new UnitTestForApiController();
            var testobj= uobj.GetsecuritiesMfsList();
            var jobj = JsonConvert.SerializeObject(testobj);
            return jobj;
        }
    }
}
