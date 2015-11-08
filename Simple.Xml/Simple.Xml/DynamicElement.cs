using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicElement : DynamicObject
    {
        private readonly IElement element;

        public DynamicElement(IElement element)
        {
            this.element = element;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var child = new Element(binder.Name, this.element);
            element.AddChild(child);
            result = new DynamicElement(child);
            return true;
        }

        public string ToXml()
        {
            var producer = new BackwardXmlStringProducer();
            element.Accept(producer);
            return producer.ToString();
        }
    }
}
