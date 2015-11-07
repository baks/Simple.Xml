namespace Simple.Xml.UnitTests
{
    public class NullObjectElement : IElement
    {
        public void Accept(IElementVisitor visitor)
        {
        }

        public string ToXml()
        {
            return string.Empty;
        }
    }
}