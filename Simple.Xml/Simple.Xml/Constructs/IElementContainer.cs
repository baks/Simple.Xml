namespace Simple.Xml.Structure.Constructs
{
    public interface IElementContainer : IElement
    {
        void AddChild(IElement child);

        void AddAttribute(Attribute attr);
    }
}