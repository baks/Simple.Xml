using System;

namespace Simple.Xml.Structure.Constructs
{
    public class NamespacePrefix
    {
        public static readonly NamespacePrefix EmptyNamespacePrefix = new NamespacePrefix();

        private readonly Namespaces namespaces;

        public readonly string prefix;

        private NamespacePrefix()
        {
            this.prefix = string.Empty;
            this.namespaces = new Namespaces();
        }

        public NamespacePrefix(string prefix, Namespaces namespaces)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            this.prefix = prefix;
            this.namespaces = namespaces;
        }

        public override string ToString()
        {
            return prefix;
        }

        public string Prefix()
        {
            return prefix;
        }

        public string NamespaceName()
        {
            return namespaces.ContainsKey(prefix) ? namespaces[prefix] : prefix;
        }
    }
}