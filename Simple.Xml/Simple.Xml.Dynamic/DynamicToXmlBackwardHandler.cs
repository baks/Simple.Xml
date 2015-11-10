using System.Dynamic;

namespace Simple.Xml.Dynamic
{
    public class DynamicToXmlBackwardHandler : DynamicElementDecorator
    {
        public DynamicToXmlBackwardHandler(BaseDynamicElement dynamicElement) : base(dynamicElement)
        {
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var toXmlMethod = string.Equals("ToXml", binder.Name);
            if (toXmlMethod)
            {
                result = ToXml();
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public string ToXml()
        {
            var creator = new DynamicBackwardXmlCreator();
            Accept(creator);

            return creator.ToString();
        }
    }
}