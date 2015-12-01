using System.Collections.Generic;
using System.Xml.Linq;

namespace Simple.Xml.Structure.UnitTests
{
    public class XAttributeComparer : IEqualityComparer<XAttribute>
    {
        public bool Equals(XAttribute x, XAttribute y)
        {
            return x.Name == y.Name && x.Value == y.Value;
        }

        public int GetHashCode(XAttribute obj)
        {
            return obj.Name.GetHashCode() ^ obj.Value.GetHashCode();
        }
    }
}