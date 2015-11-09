using System.Collections.Generic;
using System.Text;

namespace Simple.Xml
{
    public class ForwardXmlStringProducer : IDownwardElementVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Visit(string name, IEnumerable<IElement> children)
        {
            StartTag(name);
            ChildrenTags(children);
            EndTag(name);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void ChildrenTags(IEnumerable<IElement> children)
        {
            foreach (var child in children)
            {
                child.Accept(this);
            }
        }

        private void StartTag(string tag)
        {
            stringBuilder.Append($"<{tag}>");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append($"</{tag}>");
        }
    }
}