using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestSharp.Tests.RestSharp_HttpConverter
{
    [Trait("Unit", "When Converting To And From HttpRequests")]
    public class When_Converting_To_An_HttpRequest
    {
        RestClient restClient = new RestClient();
        RestRequest restRequest = new RestRequest();

        HttpConverter _converter;

        public When_Converting_To_An_HttpRequest()
        {
            restClient.BaseUrl = "http://example.com";

            _converter = new HttpConverter();
        }

        [Fact]
        public void Then_Convert_Client_Proxy()
        {
            var knownProxy = new WebProxy();
            restClient.Proxy = knownProxy;

            var httpRequest = _converter.ConvertTo(restClient, restRequest);

            Assert.NotNull(httpRequest.Proxy);
            Assert.Equal(knownProxy, httpRequest.Proxy);
        }

        [Fact]
        public void Then_Convert_Request_Timeout()
        {
            var knownTimeout = 10000;
            restRequest.Timeout = knownTimeout;

            var httpRequest = _converter.ConvertTo(restClient, restRequest);

            Assert.NotNull(httpRequest.Timeout);
            Assert.Equal(knownTimeout, httpRequest.Timeout);
        }

        [Fact]
        public void Then_Convert_Client_Timeout()
        {
            var knownTimeout = 10000;
            restClient.Timeout = knownTimeout;

            var httpRequest = _converter.ConvertTo(restClient, restRequest);

            Assert.NotNull(httpRequest.Timeout);
            Assert.Equal(knownTimeout, httpRequest.Timeout);
        }

        [Fact]
        public void Then_Convert_Credentials()
        {
            var knownCredentials = new NetworkCredential();
            restRequest.Credentials = knownCredentials;

            var httpRequest = _converter.ConvertTo(restClient, restRequest);

            Assert.NotNull(httpRequest.Credentials);
            Assert.Equal(knownCredentials, httpRequest.Credentials);
        }

        [Fact]
        public void Then_Convert_Authenticator()
        {
            var knownAuthenticator = new HttpBasicAuthenticator("unit", "test");
            restClient.Authenticator = knownAuthenticator;

            var httpRequest = _converter.ConvertTo(restClient, restRequest);

            //Check to see if the parameters collection contain the Authorization header
            //Assert.NotNull(httpRequest.Credentials);
            //Assert.Equal(knownAuthenticator, httpRequest.Au);
        }

        [Fact]
        public void Then_Convert_Request_Headers()
        {
            restRequest.AddHeader("request", "header");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var header = httpRequest.Headers.FirstOrDefault(p => p.Name == "request");

            Assert.NotNull(header);
            Assert.Equal("request", header.Name);
            Assert.Contains("header", header.Value);
        }

        [Fact]
        public void Then_Convert_Client_And_Request_Headers()
        {
            restClient.AddDefaultHeader("client", "header");
            restRequest.AddHeader("request", "header");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var clientHeader = httpRequest.Headers.FirstOrDefault(p => p.Name == "client");
            var requestHeader = httpRequest.Headers.FirstOrDefault(p => p.Name == "request");

            Assert.NotNull(requestHeader);

            Assert.NotNull(clientHeader);
            Assert.Equal("client", clientHeader.Name);
            Assert.Contains("header", clientHeader.Value);
        }

        [Fact]
        public void Then_Request_Header_Overwrites_Client_Header()
        {
            restClient.AddDefaultHeader("header", "client");
            restRequest.AddHeader("header", "request");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var requestHeader = httpRequest.Headers.FirstOrDefault(p => p.Name == "header");

            Assert.NotNull(requestHeader);
            Assert.Equal("header", requestHeader.Name);
            Assert.Contains("request", requestHeader.Value);
        }

        [Fact]
        public void Then_Convert_Request_Cookies()
        {
            restRequest.AddCookie("request", "cookie");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var cookie = httpRequest.Cookies.FirstOrDefault(p => p.Name == "request");

            Assert.NotNull(cookie);
            Assert.Equal("request", cookie.Name);
            Assert.Contains("cookie", cookie.Value);
        }

        [Fact]
        public void Then_Convert_Client_And_Request_Cookies()
        {
            restClient.AddDefaultParameter(new Parameter() { Name = "client", Value = "cookie", Type = ParameterType.Cookie });
            restRequest.AddCookie("request", "cookie");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var clientCookie = httpRequest.Cookies.FirstOrDefault(p => p.Name == "client");
            var requestCookie = httpRequest.Cookies.FirstOrDefault(p => p.Name == "request");

            Assert.NotNull(requestCookie);

            Assert.NotNull(clientCookie);
            Assert.Equal("client", clientCookie.Name);
            Assert.Contains("cookie", clientCookie.Value);
        }

        [Fact]
        public void Then_Request_Header_Overwrites_Client_Cookie()
        {
            restClient.AddDefaultParameter(new Parameter() { Name = "cookie", Value = "client", Type = ParameterType.Cookie });
            restRequest.AddCookie("cookie", "request");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var requestCookie = httpRequest.Cookies.FirstOrDefault(p => p.Name == "cookie");

            Assert.NotNull(requestCookie);
            Assert.Equal("cookie", requestCookie.Name);
            Assert.Contains("request", requestCookie.Value);
        }

        [Fact]
        public void Then_Convert_Request_GetPost_Parameters()
        {
            restRequest.AddParameter("request", "getorpost");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var getOrPost = httpRequest.Parameters.FirstOrDefault(p => p.Key == "request");

            Assert.NotNull(getOrPost);
            Assert.Equal("request", getOrPost.Key);
            Assert.Contains("getorpost", getOrPost.Value);
        }

        [Fact]
        public void Then_Convert_Client_And_Request_GetPost_Parameters()
        {
            restClient.AddDefaultParameter("client", "getorpost");
            restRequest.AddParameter("request", "getorpost");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var clientGetOrPost = httpRequest.Parameters.FirstOrDefault(p => p.Key == "client");
            var requestGetOrPost = httpRequest.Parameters.FirstOrDefault(p => p.Key == "request");

            Assert.NotNull(requestGetOrPost);

            Assert.NotNull(clientGetOrPost);
            Assert.Equal("client", clientGetOrPost.Key);
            Assert.Contains("getorpost", clientGetOrPost.Value);
        }

        [Fact]
        public void Then_Request_Header_Overwrites_Client_GetPost_Parameters()
        {
            restClient.AddDefaultParameter("getorpost", "client");
            restRequest.AddParameter("getorpost", "request");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var requestGetOrPost = httpRequest.Parameters.FirstOrDefault(p => p.Key == "getorpost");

            Assert.NotNull(requestGetOrPost);
            Assert.Equal("getorpost", requestGetOrPost.Key);
            Assert.Contains("request", requestGetOrPost.Value);
        }

        [Fact]
        public void Then_Convert_Request_Files()
        {
            var knownData = new byte[0];

            restRequest.AddFile("unit", knownData, "test.ext");

            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var file = httpRequest.Files.FirstOrDefault(p => p.Name == "unit");

            Assert.NotNull(file);
            Assert.Equal("unit", file.Name);
            Assert.Equal("test.ext", file.FileName);
            Assert.Equal(knownData, file.Data);
        }

        [Fact]
        public void Then_Convert_Request_Body()
        {
            var knownObject = new { FirstName="jon", LastName="doe" };
            var knownBody = SimpleJson.SerializeObject(knownObject);

            restRequest.AddBody(knownObject);
            
            var converter = new HttpConverter();
            var httpRequest = converter.ConvertTo(restClient, restRequest);

            var body = httpRequest.RequestBody;
            var contentType = httpRequest.RequestContentType;

            Assert.NotNull(body);
            Assert.Equal(knownBody, body);

            Assert.NotNull(contentType);
        }
    }
}
