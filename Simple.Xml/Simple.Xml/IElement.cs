namespace Simple.Xml
{
    public interface IElement
    {
        void Accept(IElementVisitor visitor);

        string ToXml();
    }
}