namespace Simple.Xml
{
    public interface IElement
    {
        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);

        string ToXml();
    }
}