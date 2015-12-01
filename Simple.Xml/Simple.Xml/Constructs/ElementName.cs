using System;
using System.Xml.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class ElementName
    {
        private readonly string name;
        private readonly Namespaces namespaces;

        private string prefix;

        public ElementName(string name, Namespaces namespaces)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            this.name = name;
            this.namespaces = namespaces;
            Parse();
        }

        public string Name { get; private set; }

        public NamespacePrefix NamespacePrefix => string.IsNullOrEmpty(prefix)
            ? NamespacePrefix.EmptyNamespacePrefix
            : new NamespacePrefix(prefix, namespaces);

        public override string ToString() => string.IsNullOrEmpty(prefix) ? $"{Name}" : $"{prefix}:{Name}";

        public XName ToXName() => XName.Get(Name, NamespacePrefix.NamespaceName);

        public TagName ToTagName() => new TagName(Name, NamespacePrefix);

        private void Parse()
        {
            var splitted = name.Split('_');
            if (splitted.Length > 1)
            {
                prefix = splitted[0];
                Name = splitted[1];
            }
            else
            {
                Name = splitted[0];
            }
        }
    }
}