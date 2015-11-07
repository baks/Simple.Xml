namespace Simple.Xml.UnitTests
{
    public class NullObjectElement : IElement
    {
        public void Accept(IUpwardElementVisitor visitor)
        {
        }

        public string ToXml()
        {
            return string.Empty;
        }
    }
}