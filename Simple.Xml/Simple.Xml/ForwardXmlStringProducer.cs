using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Xml.Structure
{
    public class ForwardXmlStringProducer : IDownwardElementVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Visit(string name, IEnumerable<IElement> children, IEnumerable<Attribute> attributes)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            StartTag(name, attributes);
            ChildrenTags(children);
            EndTag(name);
        }

        public void Visit(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            stringBuilder.Append(content);
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

        private void StartTag(string tag, IEnumerable<Attribute> attributes)
        {
            var attributesString = attributes.Any() ? " " + string.Join(" ", attributes): string.Empty;
            stringBuilder.Append($"<{tag}{attributesString}>");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append($"</{tag}>");
        }
    }
}