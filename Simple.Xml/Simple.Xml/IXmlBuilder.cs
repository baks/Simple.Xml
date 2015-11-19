namespace Simple.Xml.Structure
{
    public interface IXmlBuilder
    {
        void WriteStartTagFor(string name);
        void WriteEndTag();
        void WriteContent(string content);
    }
}