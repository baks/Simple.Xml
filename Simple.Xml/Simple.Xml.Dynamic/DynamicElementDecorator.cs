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

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return this.dynamicElement.TrySetMember(binder, value);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return this.dynamicElement.TryInvokeMember(binder, args, out result);
        }
    }
}