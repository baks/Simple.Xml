using System.Collections.Generic;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure
{
    public interface IDownwardElementVisitor
    {
        void Visit(Tag tag, IEnumerable<IElement> children);

        void Visit(string content);

        void Visit(Namespaces namespaces);
    }
}