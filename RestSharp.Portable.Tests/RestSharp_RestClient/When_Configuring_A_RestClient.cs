using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace RestSharp.Tests.RestSharp_RestClient
{
    [Trait("Unit", "When Configuring A RestClient")]
    public class When_Configuring_A_RestClient
    {
        [Fact]
        public void Then_A_Default_Accept_Header_Is_Created()
        {
            var knownAccept = "application/json+hal";

            var request = new RestRequest();
            request.AddHeader("Accept", knownAccept);

            //Assert.NotNull(header);
            //Assert.Equal("Accept", header.Name);
            //Assert.Equal(knownAccept, header.Value.FirstOrDefault());
        }
    }
}
