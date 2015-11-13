namespace Simple.Xml.Structure.UnitTests
{
    public class NullObjectElement : IElement
    {
        public void AddChild(IElement child)
        {
        }

        public IElement NewChild(string childName)
        {
            return new NullObjectElement();
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