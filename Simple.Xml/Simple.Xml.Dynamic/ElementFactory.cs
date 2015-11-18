using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public class ElementFactory : IElementFactory
    {
        public IElement CreateElementWithNameForParent(string name, IElement parent)
        {
            return new Element(name, parent, new ElementCollector());
        }

        public IElement CreateElementWithContentForParent(string content, IElement parent)
        {
            return new ContentElement(content);
        }
    }
}
