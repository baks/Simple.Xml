using System;

namespace Simple.Xml
{
    public static class XmlBuilder
    {
        public static Func<IElement, IElement> DecorateElement = element => element;

        public static dynamic NewDocument => DecorateElement(new Document());
    }
}
