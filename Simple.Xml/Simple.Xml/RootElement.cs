using System;
using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class RootElement : IElement
    {
        private readonly IElementCollector collector;
        private readonly List<IElement> children;

        public RootElement()
        {
            children = new List<IElement>();
        }

        public RootElement(IElementCollector collector) : this()
        {
            if (collector == null)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            this.collector = collector;
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

        public void Accept(IDownwardElementVisitor visitor)
        {
            foreach (var child in children)
            {
                child.Accept(visitor);
            }
        }

        public void Accept(IUpwardElementVisitor visitor) { }
    }
}