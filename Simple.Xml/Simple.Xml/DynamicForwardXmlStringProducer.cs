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
            var creator = new DynamicForwardXmlStringCreator();
            this.dynamicElement.Accept(creator);

            return creator.ToString();
        }
    }

    public class DynamicForwardXmlStringCreator : IDynamicElementVisitor
    {
        private ForwardXmlStringProducer producer;

        public void Visit(IElement element)
        {
            producer = new ForwardXmlStringProducer();
            element.Accept(producer);
        }

        public override string ToString()
        {
            return producer.ToString();
        }
    }
}