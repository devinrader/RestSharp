using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp.Tests
{
    public class FileParameterComparer : IEqualityComparer<FileParameter>
    {
        public bool Equals(FileParameter x, FileParameter y)
        {
            if (string.Equals(x.Name, y.Name) && x.Data == y.Data && string.Equals(x.FileName, y.FileName))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(FileParameter obj)
        {
            return obj.GetHashCode();
        }
    }
}
