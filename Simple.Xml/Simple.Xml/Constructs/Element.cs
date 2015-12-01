using System;

namespace Simple.Xml.Structure.Constructs
{
    public class Element : IElementContainer
    {
        private readonly ElementName elementName;
        private readonly IElement parent;
        private readonly IElementCollector collector;

        public Element(ElementName elementName, IElement parent, IElementCollector collector)
        {
            if (elementName == null)
            {
                throw new ArgumentNullException(nameof(elementName));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            this.elementName = elementName;
            this.parent = parent;
            this.collector = collector;
        }

        public void AddChild(IElement child)
        {
            collector.AddElement(child);
        }

        public void AddAttribute(Attribute attr)
        {
            collector.AddAttribute(attr);
        }

        public void Accept(IDownwardElementVisitor visitor)
            => visitor.Visit(ConvertElementNameToTag(), collector.ChildrenFor(this));

        public void Accept(IUpwardElementVisitor visitor)
            => visitor.Visit(ConvertElementNameToTag(), this.parent, collector.ChildrenFor(this));

        private Tag ConvertElementNameToTag()
        {
            return new Tag(elementName.ToTagName(), collector.AttributesFor(this));
        }
    }
}