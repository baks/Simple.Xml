using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public class ElementCollector : IElementCollector
    {
        private readonly List<IElement> children = new List<IElement>(); 

        public void AddElement(IElement child)
        {
            children.Add(child);
        }

        public IEnumerable<IElement> ChildrenFor(IElement element)
        {
            return children;
        }
    }
}
