using System.Dynamic;

namespace Simple.Xml
{
    public class Document : DynamicObject
    {
        private readonly IElement topElement;

        public Document(IElement topElement)
        {
            this.topElement = topElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var element = XmlElement(binder.Name);
            topElement.AddChild(element);
            result = new DynamicElement(element);
            return true;
        }

        public string ToXml()
        {
            var producer = new ForwardXmlStringProducer();

            topElement.Accept(producer);
            return producer.ToString();
        }

        private IElement XmlElement(string name) => new Element(name, topElement);
    }
}