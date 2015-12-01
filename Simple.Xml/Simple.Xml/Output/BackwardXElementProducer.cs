using System.Collections.Generic;
using System.Xml.Linq;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class BackwardXElementProducer : IUpwardElementVisitor
    {
        private XElement root;

        public void Visit(Tag tag, IElement parent, IEnumerable<IElement> children)
        {
            if (this.root == null)
            {
                this.root = tag.ToXElement();
            }
            else
            {
                var tagXElement = tag.ToXElement();
                tagXElement.Add(this.root);
                this.root = tagXElement;
            }
        }

        public void Visit(string content)
        {
            this.root.Value = content;
        }

        public XElement ToXElement()
        {
            return this.root;
        }
    }
}