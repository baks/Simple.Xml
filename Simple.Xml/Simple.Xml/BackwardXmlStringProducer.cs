using System.Collections.Generic;
using System.Text;

namespace Simple.Xml.Structure
{
    public class BackwardXmlStringProducer : IUpwardElementVisitor
    {
        private const int StartPosition = 0;
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Visit(string name, IElement parent, IEnumerable<IElement> children)
        {
            StartTag(name);
            EndTag(name);
            parent.Accept(this);
        }

        public void Visit(string content)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void StartTag(string tag)
        {
            stringBuilder.Insert(StartPosition, $"<{tag}>");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append($"</{tag}>");
        }
    }
}