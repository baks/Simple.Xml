using System;
using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class RootElement : IElement
    {
        private readonly IElementCollector collector;
        private readonly List<IElement> children;

        public RootElement(IElementCollector collector)
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
            var child = new Element(childName, this, new ElementCollector());
            collector.AddElement(child);
            return child;
        } 

        public void Accept(IDownwardElementVisitor visitor)
        {
            foreach (var child in collector.ChildrenFor(this))
            {
                child.Accept(visitor);
            }
        }

        public void Accept(IUpwardElementVisitor visitor) { }
    }
}