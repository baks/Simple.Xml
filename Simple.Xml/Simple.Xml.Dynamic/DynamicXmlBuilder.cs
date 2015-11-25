using System;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public static class DynamicXmlBuilder
    {
        public static Func<BaseDynamicElement, BaseDynamicElement> DecorateElement = element => element;
        public static Namespaces namespaces = Namespaces.EmptyNamespaces;

        public static dynamic NewDocument
            =>
                DecorateElement(
                    new DynamicToXmlForwardHandler(
                        new DynamicElement(new RootElement(namespaces, new ElementCollector()), new ElementFactory())));

        public static void NamespaceDeclarations(Namespaces namespaces)
        {
            DynamicXmlBuilder.namespaces = namespaces;
        }
    }
}
