using System.Collections.Generic;
using System.Xml.Linq;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class ForwardXElementProducer : IDownwardElementVisitor
    {
        private XElement root;
        private XElement actualElement;

        public void Visit(Tag tag, IEnumerable<IElement> children)
        {
            var xElementFromTag = tag.ToXElement();
            if (root == null)
            {
                root = xElementFromTag;
                this.actualElement = root;
            }
            else
            {
                this.actualElement.Add(xElementFromTag);
            }
            var previous = this.actualElement;
            this.actualElement = xElementFromTag;
            ChildrenTags(children);
            this.actualElement = previous;
        }

        public void Visit(string content)
        {
            this.actualElement.Value = content;
        }

        public void Visit(Namespaces namespaces)
        {
        }

        public XElement ToXElement()
        {
            return this.root;
        }

        private void ChildrenTags(IEnumerable<IElement> children)
        {
            foreach (var child in children)
            {
                child.Accept(this);
            }
        }
    }
}