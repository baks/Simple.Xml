using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Xml
{
    public class Document : BaseElement
    {
        private readonly List<IElement> elements;

        public Document()
        {
            this.elements = new List<IElement>();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var element = XmlElement(binder.Name);
            elements.Add(element);
            result = element;
            return true;
        }

        public override void Accept(IUpwardElementVisitor visitor)
        {
        }

        public override string ToXml()
        {
            var producer = new ForwardXmlStringProducer();

            foreach (var element in elements)
            {
                element.Accept(producer);
            }

            return producer.ToString();
        }

        private IElement XmlElement(string name) => XmlBuilder.DecorateElement(new Element(name, this));
    }
}