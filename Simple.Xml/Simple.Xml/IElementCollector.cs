using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IElementCollector
    {
        void AddElement(IElement child);

        IEnumerable<IElement> ChildrenFor(IElement element);
    }
}