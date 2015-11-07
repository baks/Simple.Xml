using System.Collections.Generic;

namespace Simple.Xml
{
    public interface IUpwardElementVisitor
    {
        void Visit(string name, IElement parent, IEnumerable<IElement> children);
    }
}