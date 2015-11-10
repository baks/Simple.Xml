using System;
using System.Dynamic;

namespace Simple.Xml.Dynamic
{
    public class DynamicElementDecorator : BaseDynamicElement
    {
        private readonly BaseDynamicElement dynamicElement;

        public DynamicElementDecorator(BaseDynamicElement dynamicElement)
        {
            if (dynamicElement == null)
            {
                throw new ArgumentNullException(nameof(dynamicElement));
            }
            this.dynamicElement = dynamicElement;
        }

        public override void Accept(IDynamicElementVisitor visitor)
        {
            this.dynamicElement.Accept(visitor);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return this.dynamicElement.TryGetMember(binder, out result);
        }
    }
}