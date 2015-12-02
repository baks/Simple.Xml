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
                var newRoot = tag.ToXElement();
                newRoot.Add(this.root);
                this.root = newRoot;
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