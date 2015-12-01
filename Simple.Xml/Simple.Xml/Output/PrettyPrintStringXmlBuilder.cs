using System;
using System.Text;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class PrettyPrintStringXmlBuilder : IXmlBuilder
    {
        private const char IndentationCharacter = ' ';

        private readonly IXmlBuilder xmlBuilder;
        private readonly StringBuilder stringBuilder;

        private int indentationLevel;

        public PrettyPrintStringXmlBuilder(IXmlBuilder xmlBuilder, StringBuilder stringBuilder)
        {
            if (xmlBuilder == null)
            {
                throw new ArgumentNullException(nameof(xmlBuilder));
            }
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }
            this.indentationLevel = 0;
            this.xmlBuilder = xmlBuilder;
            this.stringBuilder = stringBuilder;
        }

        public void WriteStartTagFor(Tag tag)
        {
            if (stringBuilder.Length != 0)
            {
                stringBuilder.AppendLine();
            }
            AppendIndent();
            xmlBuilder.WriteStartTagFor(tag);
            IncreaseIndent();
        }

        public void WriteEndTag()
        {
            AppendLine();
            DecreaseIndent();
            AppendIndent();
            xmlBuilder.WriteEndTag();
        }

        public void WriteContent(string content)
        {
            AppendLine();
            AppendIndent();
            xmlBuilder.WriteContent(content);
        }

        public void UseNamespaces(Namespaces namespaces)
        {
           xmlBuilder.UseNamespaces(namespaces);
        }

        public override string ToString()
        {
            return xmlBuilder.ToString();
        }

        private void AppendIndent()
        {
            stringBuilder.Append(IndentationCharacter, indentationLevel * 4);
        }

        private void AppendLine()
        {
            if (indentationLevel > 0)
            {
                stringBuilder.AppendLine();
            }
        }

        private void IncreaseIndent()
        {
            indentationLevel++;
        }

        private void DecreaseIndent()
        {
            indentationLevel--;
        }
    }
}