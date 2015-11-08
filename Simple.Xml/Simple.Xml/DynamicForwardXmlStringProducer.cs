using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicForwardXmlStringProducer : DynamicObject
    {
        private readonly DynamicElement dynamicElement;

        public DynamicForwardXmlStringProducer(DynamicElement dynamicElement)
        {
            this.dynamicElement = dynamicElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicElement.TryGetMember(binder, out result);
        }

        public string ToXml()
        {
            var producer = new ForwardXmlStringProducer();
            dynamicElement.VisitElement(producer);
            return producer.ToString();
        }
    }
}