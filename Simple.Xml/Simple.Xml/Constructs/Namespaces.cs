using System.Collections.Generic;
using System.Linq;

namespace Simple.Xml.Structure.Constructs
{

        public class Namespaces : Dictionary<string, string>
        {
            public static readonly Namespaces EmptyNamespaces = new Namespaces();

            public override string ToString()
            {
                return string.Join(" ", this.Select(pair => $"xmlns:{pair.Key}=\"{pair.Value}\""));
            }
        }
}