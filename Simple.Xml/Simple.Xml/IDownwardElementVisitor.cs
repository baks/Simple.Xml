using System.Collections.Generic;

namespace Simple.Xml
{
    public interface IDownwardElementVisitor
    {
        void Visit(string name, IEnumerable<IElement> children);
    }
}