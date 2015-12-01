using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public interface IDynamicElementVisitor
    {
        void Visit(IElement element);
    }
}