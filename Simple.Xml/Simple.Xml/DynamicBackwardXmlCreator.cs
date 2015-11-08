using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicBackwardXmlStringProducer : DynamicObject
    {
        private readonly DynamicElement dynamicElement;

        public DynamicBackwardXmlStringProducer(DynamicElement dynamicElement)
        {
            this.dynamicElement = dynamicElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicElement.TryGetMember(binder, out result);
        }

        public string ToXml()
        {
            var creator = new DynamicBackwardXmlCreator();
            this.dynamicElement.Accept(creator);

            return creator.ToString();
        }
    }

    public class DynamicBackwardXmlCreator : IDynamicElementVisitor
    {
        private BackwardXmlStringProducer producer;
        public void Visit(IElement element)
        {
            producer = new BackwardXmlStringProducer();
            element.Accept(producer);
        }

        public override string ToString()
        {
            return producer.ToString();
        }
    }
}