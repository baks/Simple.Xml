using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicToXmlForwardHandler : BaseDynamicElement
    {
        private readonly BaseDynamicElement dynamicElement;

        public DynamicToXmlForwardHandler(BaseDynamicElement dynamicElement)
        {
            this.dynamicElement = dynamicElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicElement.TryGetMember(binder, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var toXmlMethod = string.Equals("ToXml", binder.Name);
            if (toXmlMethod)
            {
                result = ToXml();
                return true;
            }

            return dynamicElement.TryInvokeMember(binder, args, out result);
        }

        public override void Accept(IDynamicElementVisitor visitor) => dynamicElement.Accept(visitor);

        public string ToXml()
        {
            var creator = new DynamicForwardXmlStringCreator();
            this.dynamicElement.Accept(creator);

            return creator.ToString();
        }
    }
}