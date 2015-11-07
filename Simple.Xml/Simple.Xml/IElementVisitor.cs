using System.Collections.Generic;

namespace Simple.Xml
{
    public interface IElementVisitor
    {
        void Visit(string name, IElement parent, IEnumerable<IElement> children);
    }
}