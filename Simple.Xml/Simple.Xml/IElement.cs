namespace Simple.Xml
{
    public interface IElement
    {
        void Accept(IUpwardElementVisitor visitor);

        string ToXml();
    }
}