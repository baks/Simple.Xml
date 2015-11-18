using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IDownwardElementVisitor
    {
        void Visit(string name, IEnumerable<IElement> children, IEnumerable<Attribute> attributes);

        void Visit(string content);
    }
}