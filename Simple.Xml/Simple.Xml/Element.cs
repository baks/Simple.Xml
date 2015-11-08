using System;
using System.Collections.Generic;

namespace Simple.Xml
{
    public class Element : IElement
    {
        private readonly List<IElement> children;
        private readonly string name;
        private readonly IElement parent;

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

        public void AddChild(IElement child) => children.Add(child);

        public void Accept(IDownwardElementVisitor visitor) => visitor.Visit(this.name, this.children);

        public void Accept(IUpwardElementVisitor visitor) => visitor.Visit(this.name, this.parent, this.children);
    }
}