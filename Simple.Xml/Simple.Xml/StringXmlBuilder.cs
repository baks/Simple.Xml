using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Xml.Structure
{
    public class StringXmlBuilder : IXmlBuilder
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        private Stack<string> tagsStack = new Stack<string>(); 

        public void WriteStartTagFor(string name)
        {
            StartTag(name, Enumerable.Empty<Attribute>());
            tagsStack.Push(name);
        }

        public void WriteEndTag()
        {
            EndTag(tagsStack.Pop());
        }

        public void WriteContent(string content)
        {
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