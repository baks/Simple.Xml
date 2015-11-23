using System;

namespace Simple.Xml.Structure
{
    public class ElementName
    {
        private readonly string name;

        private string prefix;
        private string tagName;

        public ElementName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.name = name;
            Parse();
        }

        public string Name()
        {
            return tagName;
        }

        public NamespacePrefix NamespacePrefix()
        {
            return string.IsNullOrEmpty(prefix)
                ? Structure.NamespacePrefix.EmptyNamespacePrefix
                : new NamespacePrefix(prefix);
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
    }
    public class Element : IElement
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
            =>
                visitor.Visit(new Tag(elementName.Name(), elementName.NamespacePrefix(), collector.AttributesFor(this)),
                    collector.ChildrenFor(this));

        public void Accept(IUpwardElementVisitor visitor)
            => visitor.Visit(elementName.Name(), this.parent, collector.ChildrenFor(this));
    }
}