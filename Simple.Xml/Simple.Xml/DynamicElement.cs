using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Xml
{
    public class Element : IElement
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

        public void AddChild(IElement child) => children.Add(child);

        public void Accept(IDownwardElementVisitor visitor) => visitor.Visit(this.name, this.children);

        public void Accept(IUpwardElementVisitor visitor) => visitor.Visit(this.name, this.parent, this.children);

        public string ToXml()
        {
            var producer = new BackwardXmlStringProducer();
            this.Accept(producer);
            return producer.ToString();
        }
    }
    public class DynamicElement : DynamicObject
    {
        private readonly IElement element;

        public DynamicElement(IElement element)
        {
            this.element = element;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var child = new Element(binder.Name, this.element);
            element.AddChild(child);
            result = new DynamicElement(child);
            return true;
        }

        public string ToXml()
        {
            var producer = new BackwardXmlStringProducer();
            element.Accept(producer);
            return producer.ToString();
        }
    }
}
