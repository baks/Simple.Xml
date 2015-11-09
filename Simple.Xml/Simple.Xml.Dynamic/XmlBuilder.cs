using System;

namespace Simple.Xml.Dynamic
{
    public static class XmlBuilder
    {
        public static Func<BaseDynamicElement, BaseDynamicElement> DecorateElement = element => element;

        public static dynamic NewDocument
            => DecorateElement(new DynamicToXmlForwardHandler(new DynamicElement(new RootElement())));
    }
}
