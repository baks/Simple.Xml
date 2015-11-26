using System.Collections.Generic;
using System.Xml.Linq;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class ForwardXElementProducer : IDownwardElementVisitor
    {
        private XElement root;

        public void Visit(Tag tag, IEnumerable<IElement> children)
        {
            if (root == null)
            {
                root = tag.ToXElement();
            }
            else
            {
                root.Add(tag.ToXElement());   
            }
        }

        public void Visit(string content)
        {
        }

        public void Visit(Namespaces namespaces)
        {
        }

        public XElement ToXElement()
        {
            return this.root;
        }
    }
}