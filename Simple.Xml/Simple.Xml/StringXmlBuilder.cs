using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Xml.Structure
{
    public class StringXmlBuilder : IXmlBuilder
    {
        private readonly StringBuilder stringBuilder;
        private readonly Stack<string> tagsStack;

        public StringXmlBuilder(StringBuilder stringBuilder)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }
            this.stringBuilder = stringBuilder;
            this.tagsStack = new Stack<string>();
        }

        public void WriteStartTagFor(string name)
        {
            StartTag(name, Enumerable.Empty<Attribute>());
            tagsStack.Push(name);
        }

        public void WriteEndTag()
        {
            if (tagsStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot write end tag without start tag");
            }
            EndTag(tagsStack.Pop());
        }

        public void WriteContent(string content)
        {
            if (tagsStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot write content when no element");
            }
            stringBuilder.Append(content);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void StartTag(string tag, IEnumerable<Attribute> attributes)
        {
            var attributesString = attributes.Any() ? " " + string.Join(" ", attributes) : string.Empty;
            stringBuilder.Append($"<{tag}{attributesString}>");
        }

        private void EndTag(string tag)
        {
            stringBuilder.Append($"</{tag}>");
        }
    }
}