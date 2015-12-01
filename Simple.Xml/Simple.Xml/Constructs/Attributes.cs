using System.Collections.Generic;
using System.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class Attributes : Dictionary<string, object>
    {
        public void AddAttributesTo(IElementContainer element)
        {
            this.Aggregate(element, (elem, pair) =>
            {
                elem.AddAttribute(Map(pair));
                return elem;
            });
        }

        public IEnumerable<Attribute> Iterator()
        {
            return this.Select(Map);
        }

        private static Attribute Map(KeyValuePair<string, object> pair)
            => new Attribute(new ElementName(pair.Key, Namespaces.EmptyNamespaces), pair.Value.ToString());
    }
}
