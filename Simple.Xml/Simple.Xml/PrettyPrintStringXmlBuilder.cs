using System;
using System.Text;

namespace Simple.Xml.Structure
{
    public class PrettyPrintStringXmlBuilder : IXmlBuilder
    {
        private const char INDENTATION_CHAR = ' ';

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

        public void WriteStartTagFor(string name)
        {
            AppendLine();
            AppendIndent();
            xmlBuilder.WriteStartTagFor(name);
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

        public override string ToString()
        {
            return xmlBuilder.ToString();
        }

        private void AppendIndent()
        {
            stringBuilder.Append(INDENTATION_CHAR, indentationLevel * 4);
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