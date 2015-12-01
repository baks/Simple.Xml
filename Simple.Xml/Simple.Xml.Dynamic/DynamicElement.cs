using System;
using System.Dynamic;
using System.Linq;
using Simple.Xml.Dynamic.Output;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class DynamicElement : BaseDynamicElement
    {
        private readonly IElementContainer element;
        private readonly IElementFactory elementFactory;
        private readonly Func<BaseDynamicElement, BaseDynamicElement> graphDecorator;

        public DynamicElement(IElementContainer element, IElementFactory elementFactory,
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
            var child = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(child);
            result = DynamicElementFor(child);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var child = elementFactory.CreateElementWithNameForParent(binder.Name, element);
            element.AddChild(child);
            child.AddChild(elementFactory.CreateElementWithContentForParent(value?.ToString() ?? string.Empty, child));
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var noArguments = args.Length == 0;
            if (noArguments)
            {
                var child = elementFactory.CreateElementWithNameForParent(binder.Name, element);
                element.AddChild(child);
                result = DynamicElementFor(child);
                return true;
            }

            var attributes = args.Cast<Attributes>();
            var argsAreAttributes = attributes.Any();
            if (argsAreAttributes)
            {
                var child = elementFactory.CreateElementWithNameForParent(binder.Name, element);
                element.AddChild(child);
                attributes.First().AddAttributesTo(child);

                result = DynamicElementFor(child);
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public override void Accept(IDynamicElementVisitor visitor) => visitor.Visit(element);

        private BaseDynamicElement DynamicElementFor(IElementContainer child) => graphDecorator(
            new DynamicToXElementBackwardHandler(
                new DynamicToXmlBackwardHandler(new DynamicElement(child, elementFactory, graphDecorator))));
    }
}
