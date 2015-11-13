namespace Simple.Xml.Structure
{
    public interface IElement
    {
        void AddChild(IElement child);

        IElement NewChild(string childName);

        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);
    }
}