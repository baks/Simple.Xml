using System.Collections.Generic;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure
{
    public interface IElementCollector
    {
        void AddElement(IElement child);

        void AddAttribute(Attribute attr);

        IEnumerable<IElement> ChildrenFor(IElement element);

        IEnumerable<Attribute> AttributesFor(IElement element);
    }
}