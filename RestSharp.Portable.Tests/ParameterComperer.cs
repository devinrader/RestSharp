using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp.Tests
{
    public class ParameterComparer : IEqualityComparer<Parameter>
    {
        public bool Equals(Parameter x, Parameter y)
        {
            if (string.Equals(x.Name, y.Name) && (x.Type == y.Type))
            {
                if (x.Value is string && string.Equals(x.Value, y.Value))
                {
                    return true;
                }
                else if (x.Value == y.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetHashCode(Parameter obj)
        {
            return obj.GetHashCode();
        }
    }
}
