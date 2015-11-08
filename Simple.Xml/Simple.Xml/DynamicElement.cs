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

        public void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);
    }
}
