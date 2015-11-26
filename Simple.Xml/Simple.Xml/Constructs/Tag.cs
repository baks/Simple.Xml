using System;
using System.Collections.Generic;
using System.Linq;
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
            return XName.Get(name, namespacePrefix.NamespaceName());
        }
    }

    public class Tag
    {
        public readonly TagName tagName;
        public readonly IEnumerable<Attribute> attributes;

        public Tag(TagName tagName, IEnumerable<Attribute> attributes)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException(nameof(tagName));
            }
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }
            this.tagName = tagName;
            this.attributes = attributes;
        }

        public override string ToString()
        {
            return attributes.Any() ? $"{tagName} {string.Join(" ", attributes)}" : $"{tagName}";
        }

        public XElement ToXElement()
        {
            return new XElement(tagName.ToXName(), attributes.Select(attr => attr.ToXAttribute()));
        }
    }
}