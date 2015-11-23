using System;
using System.Collections.Generic;
using System.Text;

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

            xmlBuilder.WriteStartTagFor(tag.name, tag.attributes);
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