using System.Collections.Generic;

namespace Simple.Xml.Structure
{
    public interface IXmlBuilder
    {
        void WriteStartTagFor(string name, IEnumerable<Attribute> attributes);
        void WriteEndTag();
        void WriteContent(string content);
    }
}