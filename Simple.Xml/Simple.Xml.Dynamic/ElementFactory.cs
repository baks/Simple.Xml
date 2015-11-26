using System;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class ElementFactory : IElementFactory
    {
        private readonly Namespaces namespaces;

        public ElementFactory(Namespaces namespaces)
        {
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            this.namespaces = namespaces;
        }

        public IElement CreateElementWithNameForParent(string name, IElement parent)
        {
            return new Element(new ElementName(name, namespaces), parent, new ElementCollector());
        }

        public IElement CreateElementWithContentForParent(string content, IElement parent)
        {
            return new ContentElement(content);
        }
    }
}
