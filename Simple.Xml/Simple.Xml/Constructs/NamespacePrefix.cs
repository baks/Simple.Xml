using System;

namespace Simple.Xml.Structure
{
    public class NamespacePrefix
    {
        public static readonly NamespacePrefix EmptyNamespacePrefix = new NamespacePrefix();

        public readonly string prefix;

        private NamespacePrefix()
        {
            this.prefix = string.Empty;
        }

        public NamespacePrefix(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }
            this.prefix = prefix;
        }

        public override string ToString()
        {
            return prefix;
        }
    }
}