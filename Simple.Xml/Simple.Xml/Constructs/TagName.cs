using System;
using System.Xml.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class TagName
    {
        public readonly string name;
        public readonly NamespacePrefix namespacePrefix;

        public TagName(string name, NamespacePrefix namespacePrefix)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (namespacePrefix == null)
            {
                throw new ArgumentNullException(nameof(namespacePrefix));
            }
            this.name = name;
            this.namespacePrefix = namespacePrefix;
        }

        public override string ToString()
        {
            return namespacePrefix != NamespacePrefix.EmptyNamespacePrefix ? $"{namespacePrefix}:{name}" : $"{name}";
        }

        public XName ToXName()
        {
            return XName.Get(name, namespacePrefix.NamespaceName);
        }
    }
}