using System.Dynamic;
using System.Linq.Expressions;

namespace Simple.Xml
{
    public abstract class BaseDynamicElement : DynamicObject
    {
        public abstract void Accept(IDynamicElementVisitor visitor);

        public override DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return base.GetMetaObject(parameter);
        }
    }
}