using System.Collections.Generic;
using System.Text;

namespace Simple.Xml
{
    public class ForwardXmlStringProducer : IElementVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Visit(string name, IElement parent, IEnumerable<IElement> children)
        {
            StartTag(name);
            foreach (var child in children)
            {
                child.Accept(this);
            }
            EndTag(name);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void StartTag(string tag)
        {
            stringBuilder.Append("<" + tag + ">");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append("</" + tag + ">");
        }
    }
}