using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IXmlBuilder
    {
        void WriteStartTagFor(string name, IEnumerable<Attribute> attributes);
        void WriteStartTagFor(Tag tag);
        void WriteEndTag();

        void WriteEndTagFor(Tag tag);
        void WriteContent(string content);
    }
}