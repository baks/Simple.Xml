using System.Dynamic;

namespace Simple.Xml
{
    public abstract class BaseDynamicElement : DynamicObject
    {
        public abstract void Accept(IDynamicElementVisitor visitor);
    }
}