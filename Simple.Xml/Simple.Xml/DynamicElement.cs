using System;
using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicElement : DynamicObject
    {
        private readonly IElement element;

        public DynamicElement(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            this.element = element;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new DynamicBackwardXmlStringProducer(new DynamicElement(element.NewChild(binder.Name)));
            return true;
        }

        public void VisitElement(IUpwardElementVisitor visitor) => element.Accept(visitor);

        public void VisitElement(IDownwardElementVisitor visitor) => element.Accept(visitor);
    }

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
            var producer = new BackwardXmlStringProducer();
            this.dynamicElement.VisitElement(producer);
            return producer.ToString();
        }
    }
}
