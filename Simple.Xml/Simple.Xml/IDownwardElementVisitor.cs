using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IDownwardElementVisitor
    {
        void Visit(string name, IEnumerable<IElement> children);

        void Visit(string content);
    }
}