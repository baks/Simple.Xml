using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicDocument : DynamicObject
    {
        private readonly IElement topElement;

        public DynamicDocument(IElement topElement)
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

        public void VisitElement(IDownwardElementVisitor visitor) => topElement.Accept(visitor);

        private IElement XmlElement(string name) => new Element(name, topElement);
    }

    public class DynamicForwardXmlStringProducer : DynamicObject
    {
        private readonly DynamicDocument dynamicDocument;

        public DynamicForwardXmlStringProducer(DynamicDocument dynamicDocument)
        {
            this.dynamicDocument = dynamicDocument;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicDocument.TryGetMember(binder, out result);
        }

        public string ToXml()
        {
            var producer = new ForwardXmlStringProducer();
            dynamicDocument.VisitElement(producer);
            return producer.ToString();
        }
    }
}