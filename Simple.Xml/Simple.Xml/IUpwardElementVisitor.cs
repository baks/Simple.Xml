using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IUpwardElementVisitor
    {
        void Visit(string name, IElement parent, IEnumerable<IElement> children);
    }
}