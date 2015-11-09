namespace Simple.Xml.Structure
{
    public interface IElement
    {
        IElement NewChild(string childName);

        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);
    }
}