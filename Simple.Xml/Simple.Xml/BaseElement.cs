using System.Dynamic;

namespace Simple.Xml
{
    public abstract class BaseElement : DynamicObject, IElement
    {
        public abstract void Accept(IElementVisitor visitor);

        public abstract string ToXml();
    }
}