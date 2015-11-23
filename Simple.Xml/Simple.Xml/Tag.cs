using System;
using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class Tag
    {
        public readonly string name;
        public readonly IEnumerable<Attribute> attributes;
        public readonly NamespacePrefix namespacePrefix;

        public Tag(string name, NamespacePrefix namespacePrefix, IEnumerable<Attribute> attributes)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (namespacePrefix == null)
            {
                throw new ArgumentNullException(nameof(namespacePrefix));
            }
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }
            this.name = name;
            this.namespacePrefix = namespacePrefix;
            this.attributes = attributes;
        }
    }
}