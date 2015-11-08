namespace Simple.Xml.UnitTests
{
    public class NullObjectElement : IElement
    {
        public void AddChild(IElement child)
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