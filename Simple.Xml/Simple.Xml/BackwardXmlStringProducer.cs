using System.Collections.Generic;
using System.Text;

namespace Simple.Xml.Structure
{
    public class BackwardXmlStringProducer : IUpwardElementVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Visit(string name, IElement parent, IEnumerable<IElement> children)
        {
            StartTag(name);
            EndTag(name);
            parent.Accept(this);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void StartTag(string tag)
        {
            stringBuilder.Insert(0, "<" + tag + ">");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append("</" + tag + ">");
        }
    }
}