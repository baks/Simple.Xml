using System;

namespace Simple.Xml.Structure.Constructs
{
    public class ElementName
    {
        private readonly string name;
        private readonly Namespaces namespaces;

        private string prefix;
        private string tagName;

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

        public string Name()
        {
            return tagName;
        }

        public NamespacePrefix NamespacePrefix()
        {
            return string.IsNullOrEmpty(prefix)
                ? Constructs.NamespacePrefix.EmptyNamespacePrefix
                : new NamespacePrefix(prefix, namespaces);
        }

        private void Parse()
        {
            var splitted = name.Split('_');
            if (splitted.Length > 1)
            {
                prefix = splitted[0];
                tagName = splitted[1];
            }
            else
            {
                tagName = splitted[0];
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(prefix) ? $"{tagName}" : $"{prefix}:{tagName}";
        }
    }
}