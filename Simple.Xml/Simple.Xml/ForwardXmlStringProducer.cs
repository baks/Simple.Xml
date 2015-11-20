using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple.Xml.Structure
{
    public class ForwardXmlStringProducer : IDownwardElementVisitor
    {
        private readonly IXmlBuilder xmlBuilder;

        public ForwardXmlStringProducer(IXmlBuilder xmlBuilder)
        {
            if (xmlBuilder == null)
            {
                throw new ArgumentNullException(nameof(xmlBuilder));
            }
            this.xmlBuilder = xmlBuilder;
        }

        public void Visit(string name, IEnumerable<IElement> children, IEnumerable<Attribute> attributes)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            xmlBuilder.WriteStartTagFor(name, attributes);
            ChildrenTags(children);
            xmlBuilder.WriteEndTag();
        }

        public void Visit(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            xmlBuilder.WriteContent(content);
        }

        public override string ToString()
        {
            return xmlBuilder.ToString();
        }

        private void ChildrenTags(IEnumerable<IElement> children)
        {
            foreach (var child in children)
            {
                child.Accept(this);
            }
        }
    }
}