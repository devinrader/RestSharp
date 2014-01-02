using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp.Tests
{
    public class RestResponseCookieComparer : IEqualityComparer<RestResponseCookie>
    {
        public bool Equals(RestResponseCookie x, RestResponseCookie y)
        {
            if (string.Equals(x.Name, y.Name) && string.Equals(x.Value, y.Value))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(RestResponseCookie obj)
        {
            return obj.GetHashCode();
        }

    }
}
