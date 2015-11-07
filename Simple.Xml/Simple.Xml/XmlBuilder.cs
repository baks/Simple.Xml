using System;

namespace Simple.Xml
{
    public static class XmlBuilder
    {
        public static Func<BaseElement, BaseElement> DecorateElement = element => element;

        public static dynamic NewDocument => DecorateElement(new Document());
    }
}
