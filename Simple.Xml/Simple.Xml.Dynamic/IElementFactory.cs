using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public interface IElementFactory
    {
        IElement CreateElementWithNameForParent(string name, IElement parent);
        IElement CreateElementWithContentForParent(string content, IElement parent);
    }
}