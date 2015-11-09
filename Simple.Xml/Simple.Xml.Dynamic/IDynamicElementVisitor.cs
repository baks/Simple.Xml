using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public interface IDynamicElementVisitor
    {
        void Visit(IElement element);
    }
}