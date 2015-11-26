using System.Collections.Generic;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure
{
    public class ElementCollector : IElementCollector
    {
        private readonly List<IElement> children = new List<IElement>(); 
        private readonly List<Attribute> attributes = new List<Attribute>(); 

        public void AddElement(IElement child)
        {
            children.Add(child);
        }

        public void AddAttribute(Attribute attr)
        {
            attributes.Add(attr);
        }

        public IEnumerable<IElement> ChildrenFor(IElement element)
        {
            return children;
        }

        public IEnumerable<Attribute> AttributesFor(IElement element)
        {
            return attributes;
        }
    }
}
