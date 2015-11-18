using System;
using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class Element : IElement
    {
        private readonly string name;
        private readonly IElement parent;
        private readonly IElementCollector collector;

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
        }

        public void AddChild(IElement child)
        {
            collector.AddElement(child);
        }

        public void Accept(IDownwardElementVisitor visitor) => visitor.Visit(this.name, collector.ChildrenFor(this));

        public void Accept(IUpwardElementVisitor visitor) => visitor.Visit(this.name, this.parent, collector.ChildrenFor(this));
    }
}