using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Xml
{
    public class Element : BaseElement
    {
        private readonly List<IElement> children;
        private readonly string name;
        private readonly IElement parent;

        public Element(string name, IElement parent)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            this.name = name;
            this.parent = parent;
            this.children = new List<IElement>();
        }

        public override string ToXml()
        {
            var producer = new BackwardXmlStringProducer();
            this.Accept(producer);
            return producer.ToString();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var element = XmlBuilder.DecorateElement(new Element(binder.Name, this));
            children.Add(element);
            result = element;
            return true;
        }

        public override void Accept(IElementVisitor visitor)
        {
            visitor.Visit(this.name, this.parent, this.children);
        }
    }
}
