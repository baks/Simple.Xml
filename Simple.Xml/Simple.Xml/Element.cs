using System;
using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class Element : IElement
    {
        private readonly List<IElement> children;
        private readonly string name;
        private readonly IElement parent;
        private readonly IElementCollector collector;

        public Element(string name, IElement parent)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            this.name = name;
            this.parent = parent;
            this.children = new List<IElement>();
        }

        public Element(string name, IElement parent, IElementCollector collector)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            this.name = name;
            this.parent = parent;
            this.collector = collector;
            this.children = new List<IElement>();
        }

        public void AddChild(IElement child)
        {
            collector.AddElement(child);
        }

        public IElement NewChild(string childName)
        {
            var child = new Element(childName, this);
            children.Add(child);
            return child;
        }

        public void Accept(IDownwardElementVisitor visitor) => visitor.Visit(this.name, this.children);

        public void Accept(IUpwardElementVisitor visitor) => visitor.Visit(this.name, this.parent, this.children);
    }
}