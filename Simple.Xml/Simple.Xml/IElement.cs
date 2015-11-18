namespace Simple.Xml.Structure
{
    public interface IElement
    {
        void AddChild(IElement child);

        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);
    }
}