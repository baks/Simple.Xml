using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IDownwardElementVisitor
    {
        void Visit(Tag tag, IEnumerable<IElement> children);

        void Visit(string content);
    }
}