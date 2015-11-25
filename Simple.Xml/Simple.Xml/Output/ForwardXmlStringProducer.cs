using System;
using System.Collections.Generic;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
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

        public void Visit(Tag tag, IEnumerable<IElement> children)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            xmlBuilder.WriteStartTagFor(tag);
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

        public void Visit(Namespaces namespaces)
        {
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            xmlBuilder.UseNamespaces(namespaces);
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