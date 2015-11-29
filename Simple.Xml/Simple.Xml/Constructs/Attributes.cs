using System.Collections.Generic;
using System.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class Attributes : Dictionary<string, object>
    {
        public void AddAttributesTo(IElement element)
        {
            this.Aggregate(element, (elem, pair) =>
            {
                elem.AddAttribute(new Attribute(new ElementName(pair.Key, Namespaces.EmptyNamespaces),
                    pair.Value.ToString()));
                return elem;
            });
        }

        public IEnumerable<Attribute> Iterator()
        {
            return
                this.Select(
                    pair => new Attribute(new ElementName(pair.Key, Namespaces.EmptyNamespaces), pair.Value.ToString()));
        }
    }
}
