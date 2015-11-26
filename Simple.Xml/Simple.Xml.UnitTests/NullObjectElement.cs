using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.UnitTests
{
    public class NullObjectElement : IElement
    {
        public void AddChild(IElement child)
        {
        }

        public void AddAttribute(Attribute attr)
        {
        }

        public void Accept(IUpwardElementVisitor visitor)
        {
        }

        public void Accept(IDownwardElementVisitor visitor)
        {
        }

        public string ToXml()
        {
            return string.Empty;
        }
    }
}