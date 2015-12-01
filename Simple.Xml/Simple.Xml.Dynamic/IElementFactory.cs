using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public interface IElementFactory
    {
        IElementContainer CreateElementWithNameForParent(string name, IElement parent);
        IElement CreateElementWithContentForParent(string content, IElement parent);
    }
}