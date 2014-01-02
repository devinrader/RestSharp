using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestSharp.Tests.RestSharp_HttpConverter
{
    [Trait("Unit", "When Converting To And From HttpRequests")]
    public class When_Converting_From_An_HttpResponse
    {
        HttpConverter _converter;
        HttpResponse _httpResponse;

        public When_Converting_From_An_HttpResponse()
        {
            _httpResponse = new HttpResponse();

            _converter = new HttpConverter();
        }

        [Fact]
        public void Then_Convert_Headers()
        {
            var knownContentType = new string[] { "application/json" };

            List<Parameter> mockParameters = new List<Parameter>();
            mockParameters.Add(new Parameter() { Name = "Content-Type", Type = ParameterType.HttpHeader, Value = knownContentType });

            _httpResponse.Headers.Add(new HttpHeader() { Name = "Content-Type", Value = knownContentType });

            var restResponse = _converter.ConvertFrom(_httpResponse);

            Assert.Equal(mockParameters, restResponse.Headers, new ParameterComparer());
        }

        [Fact]
        public void Then_Convert_Cookies()
        {
            //NOTE: Maybe not the most robust test for cookies
            List<RestResponseCookie> mockParameters = new List<RestResponseCookie>();
            mockParameters.Add(new RestResponseCookie() { Name = "ThisIsATestCookie", Value = "YummyCookies" });

            _httpResponse.Cookies.Add(new HttpCookie() { Name = "ThisIsATestCookie", Value = "YummyCookies" });

            var restResponse = _converter.ConvertFrom(_httpResponse);

            Assert.Equal(mockParameters, restResponse.Cookies, new RestResponseCookieComparer());
        }
    }
}
