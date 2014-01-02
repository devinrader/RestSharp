using RestSharp.Serializers;
using RestSharp.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestSharp.Tests.RestSharp_Http
{
    [Trait("Unit", "When Configuring A RestRequest")]
    public class When_Configuring_A_RestRequest
    {
        ParameterComparer _parameterComparer = new ParameterComparer();
        FileParameterComparer _fileParameterComparer = new FileParameterComparer();

        [Fact]
        public void Then_Adding_An_Accept_Header_Overrides_The_Default_Accept_Header()
        {
            var knownAccept = "application/json+hal";

            List<Parameter> mockParameters = new List<Parameter>();
            mockParameters.Add(new Parameter() { Name = "Accept", Type = ParameterType.HttpHeader, Value = knownAccept });

            var request = new RestRequest();
            request.AddHeader("Accept", knownAccept);

            Assert.Equal(mockParameters, request.Parameters, _parameterComparer);

            var param = mockParameters.FirstOrDefault();

            Assert.Equal(param.Type, ParameterType.HttpHeader);
        }

        [Fact]
        public void Then_Adding_A_Body_Adds_A_RequestBody_Parameter()
        {
            var name = new { First = "Jon", Last = "Doe" };

            JsonSerializer serializer = new JsonSerializer();
            var data = serializer.Serialize(name);

            List<Parameter> mockParameters = new List<Parameter>();
            mockParameters.Add(new Parameter() { Name=serializer.ContentType, Type=ParameterType.RequestBody, Value=data});

            var request = new RestRequest();
            request.AddBody(name);

            Assert.Equal(mockParameters, request.Parameters, _parameterComparer);
        }

        [Fact]
        public void Then_Adding_A_File_Adds_A_File_Parameter()
        {
            var knownBytes = new byte[0];

            List<FileParameter> mockParameters = new List<FileParameter>();
            mockParameters.Add(new FileParameter() { Name="unit", Data=knownBytes, FileName="test.ext" });

            var request = new RestRequest();
            request.AddFile("unit",knownBytes, "test.ext");

            Assert.Equal(mockParameters, request.Files, _fileParameterComparer);
        }

        [Fact]
        public void Then_Adding_An_Object_With_Two_Public_Properties_Adds_Two_Parameters()
        {
            List<Parameter> mockParameters = new List<Parameter>();
            mockParameters.Add(new Parameter() { Name="First", Value="Jon", Type=ParameterType.GetOrPost });
            mockParameters.Add(new Parameter() { Name = "Last", Value = "Doe", Type = ParameterType.GetOrPost });

            var name = new { First = "Jon", Last = "Doe" };

            var request = new RestRequest();
            request.AddObject(name);

            Assert.Equal(mockParameters, request.Parameters, _parameterComparer);
        }

        [Fact]
        public void Then_Adding_An_Object_With_Two_Public_Properties_And_A_Whitelist_Adds_One_Parameter()
        {
            List<Parameter> mockParameters = new List<Parameter>();
            mockParameters.Add(new Parameter() { Name = "First", Value = "Jon", Type = ParameterType.GetOrPost });

            var name = new { First = "Jon", Last = "Doe" };

            var request = new RestRequest();
            request.AddObject(name, new string[] { "First" });

            Assert.Equal(mockParameters, request.Parameters, _parameterComparer);
        }

        [Fact]
        public void Then_Adding_A_Default_Parameter()
        {
            AssertExtensions.Incomplete();
        }

        [Fact]
        public void Then_Adding_A_Default_UrlSegment()
        {
            AssertExtensions.Incomplete();
        }

        [Fact]
        public void Then_Adding_A_Cookie()
        {
            AssertExtensions.Incomplete();
        }

        //client.AddDefaultHeader();
        //client.AddHandler();
    }

}
