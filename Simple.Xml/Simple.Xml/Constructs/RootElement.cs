using System;

namespace Simple.Xml.Structure.Constructs
{
    public class RootElement : IElement
    {
        private readonly Namespaces namespaces;
        private readonly IElementCollector collector;

        public RootElement(Namespaces namespaces, IElementCollector collector)
        {
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            this.namespaces = namespaces;
            this.collector = collector;
        }

        public void AddChild(IElement child)
        {
            collector.AddElement(child);
        }

        public void AddAttribute(Attribute attr)
        {
        }

        public void Accept(IDownwardElementVisitor visitor)
        {
            visitor.Visit(namespaces);
            foreach (var child in collector.ChildrenFor(this))
            {
                child.Accept(visitor);
            }
        }

        public void Accept(IUpwardElementVisitor visitor) { }
    }
}