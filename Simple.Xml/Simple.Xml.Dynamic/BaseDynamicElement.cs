using System.Dynamic;

namespace Simple.Xml.Dynamic
{
    public abstract class BaseDynamicElement : DynamicObject
    {
        public abstract void Accept(IDynamicElementVisitor visitor);
    }
}