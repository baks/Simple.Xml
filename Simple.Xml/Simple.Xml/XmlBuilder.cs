using System;
using System.Dynamic;

namespace Simple.Xml
{
    public static class XmlBuilder
    {
        public static Func<DynamicObject, DynamicObject> DecorateElement = element => element;

        public static dynamic NewDocument
            => DecorateElement(new DynamicForwardXmlStringProducer(new DynamicElement(new RootElement())));
    }
}
