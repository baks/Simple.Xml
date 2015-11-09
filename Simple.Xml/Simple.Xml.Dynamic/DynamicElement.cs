using System;
using System.Dynamic;

namespace Simple.Xml.Dynamic
{
    public class DynamicElement : BaseDynamicElement
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
            result =
                XmlBuilder.DecorateElement(
                    new DynamicToXmlBackwardHandler(new DynamicElement(element.NewChild(binder.Name))));
            return true;
        }

        public override void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);
    }
}
