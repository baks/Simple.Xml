using System;
using System.Dynamic;
using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public class DynamicElement : BaseDynamicElement
    {
        private readonly IElement element;
        private readonly IElementFactory elementFactory;

        public DynamicElement(IElement element, IElementFactory elementFactory)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (elementFactory == null)
            {
                throw new ArgumentNullException(nameof(elementFactory));
            }
            this.element = element;
            this.elementFactory = elementFactory;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(newElement);
            result =
                XmlBuilder.DecorateElement(
                    new DynamicToXmlBackwardHandler(new DynamicElement(newElement, elementFactory)));
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(newElement);
            newElement.AddChild(elementFactory.CreateElementWithContentForParent(value.ToString(), newElement));
            return true;
        }

        public override void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);
    }
}
