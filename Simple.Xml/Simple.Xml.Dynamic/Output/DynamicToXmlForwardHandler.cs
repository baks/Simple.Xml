using System.Dynamic;
using System.Text;
using Simple.Xml.Structure.Output;

namespace Simple.Xml.Dynamic.Output
{
    public class DynamicToXmlForwardHandler : DynamicElementDecorator
    {
        public DynamicToXmlForwardHandler(BaseDynamicElement dynamicElement) : base(dynamicElement)
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
            var stringBuilder = new StringBuilder();
            var creator =
                new DynamicForwardXmlCreator(
                    new ForwardXmlStringProducer(new PrettyPrintStringXmlBuilder(new StringXmlBuilder(stringBuilder),
                        stringBuilder)));
            Accept(creator);

            return creator.ToString();
        }
    }
}