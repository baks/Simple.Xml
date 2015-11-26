using System.Collections.Generic;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure
{
    public interface IUpwardElementVisitor
    {
        void Visit(Tag tag, IElement parent, IEnumerable<IElement> children);

        void Visit(string content);
    }
}