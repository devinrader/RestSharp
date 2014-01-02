using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RestSharp.Tests
{
    public class AssertExtensions : Xunit.Assert
    {
        public static void Fail(string reason)
        {
            throw new AssertException(reason);
        }

        public static void Incomplete()
        {
            Fail("Incomplete Test");
        }

    }
}
