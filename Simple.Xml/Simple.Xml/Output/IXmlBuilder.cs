using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public interface IXmlBuilder
    {
        void WriteStartTagFor(Tag tag);
        void WriteEndTag();
        void WriteContent(string content);
        void UseNamespaces(Namespaces namespaces);
    }
}