using System;
using System.Dynamic;
using System.Linq;
using Simple.Xml.Dynamic.Output;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Attribute = Simple.Xml.Structure.Constructs.Attribute;

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
            newElement.AddChild(elementFactory.CreateElementWithContentForParent(value.ToString(), newElement));
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var attributes = args.Cast<Attributes>();
            if (attributes.Any())
            {
                var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
                element.AddChild(newElement);
                attributes.First().AddAttributesTo(newElement);

                result = graphDecorator(
                    new DynamicToXElementBackwardHandler(
                        new DynamicToXmlBackwardHandler(new DynamicElement(newElement, elementFactory, graphDecorator))));
                return true;
            }
            if (args.Length == 0)
            {
                var newElement = elementFactory.CreateElementWithNameForParent(binder.Name, element);
                element.AddChild(newElement);
                result =
                    graphDecorator(
                        new DynamicToXElementBackwardHandler(
                            new DynamicToXmlBackwardHandler(new DynamicElement(newElement, elementFactory, graphDecorator))));
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }

        public override void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);
    }
}
