namespace Simple.Xml.Structure.Constructs
{
    public interface IElement
    {
        void Accept(IUpwardElementVisitor visitor);

        void Accept(IDownwardElementVisitor visitor);
    }
}