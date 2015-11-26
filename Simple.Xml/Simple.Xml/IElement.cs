using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure
{
    public interface IElement
    {
        void AddChild(IElement child);

        void AddAttribute(Attribute attr);

        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);
    }
}