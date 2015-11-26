using System;
using System.Dynamic;
using Simple.Xml.Dynamic.Output;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class DynamicElement : BaseDynamicElement
    {
        private readonly IElement element;
        private readonly IElementFactory elementFactory;
        private readonly Func<BaseDynamicElement, BaseDynamicElement> graphDecorator;

        public DynamicElement(IElement element, IElementFactory elementFactory,
            Func<BaseDynamicElement, BaseDynamicElement> graphDecorator)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            if (elementFactory == null)
            {
                throw new ArgumentNullException(nameof(elementFactory));
            }
            if (graphDecorator == null)
            {
                throw new ArgumentNullException(nameof(graphDecorator));
            }
            this.element = element;
            this.elementFactory = elementFactory;
            this.graphDecorator = graphDecorator;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(newElement);
            result =
                graphDecorator(
                    new DynamicToXElementBackwardHandler(
                        new DynamicToXmlBackwardHandler(new DynamicElement(newElement, elementFactory, graphDecorator))));
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(newElement);
            if (value is Attributes)
            {
                ((Attributes)value).AddAttributesTo(newElement);
            }
            else
            {
                newElement.AddChild(elementFactory.CreateElementWithContentForParent(value.ToString(), newElement));
            }
            return true;
        }

        public override void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);
    }
}
