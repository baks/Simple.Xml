using System;
using System.Collections.Generic;
using System.Text;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class BackwardXmlStringProducer : IUpwardElementVisitor
    {
        private readonly IXmlBuilder xmlBuilder;
        private int parentCount = 0;
        private int tagsCreated = 0;

        public BackwardXmlStringProducer(IXmlBuilder xmlBuilder)
        {
            if (xmlBuilder == null)
            {
                throw new ArgumentNullException(nameof(xmlBuilder));
            }
            this.xmlBuilder = xmlBuilder;
        }

        public void Visit(Tag tag, IElement parent, IEnumerable<IElement> children)
        {
            TraverseToRoot(parent);

            StartTag(tag);

            if (GotBackToStartElement())
            {
                CloseAllTagsCreated();
            }
        }

        public void Visit(string content)
        {
        }

        public override string ToString()
        {
            return xmlBuilder.ToString();
        }

        private void TraverseToRoot(IElement parent)
        {
            parentCount++;
            parent.Accept(this);
        }

        private void StartTag(Tag tag)
        {
            this.xmlBuilder.WriteStartTagFor(tag);
            tagsCreated++;
        }

        private bool GotBackToStartElement()
        {
            return --parentCount == 0;
        }

        private void CloseAllTagsCreated()
        {
            while (tagsCreated-- > 0)
            {
                this.xmlBuilder.WriteEndTag();
            }
        }
    }
}