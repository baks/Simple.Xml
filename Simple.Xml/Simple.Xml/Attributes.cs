using System.Collections.Generic;
using System.Linq;

namespace Simple.Xml.Structure
{
    public class Attributes : Dictionary<string, string>
    {
        public void AddAttributesTo(IElement element)
        {
            this.Aggregate(element, (elem, pair) =>
            {
                elem.AddAttribute(new Attribute(pair.Key, pair.Value));
                return elem;
            });
        }
    }
}
