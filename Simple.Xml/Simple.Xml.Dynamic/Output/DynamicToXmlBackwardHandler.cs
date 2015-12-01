using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using Simple.Xml.Structure.Output;

namespace Simple.Xml.Dynamic.Output
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
            var creator = new DynamicBackwardXmlCreator(new BackwardXmlStringProducer(new StringXmlBuilder(new StringBuilder())));
            Accept(creator);

            return creator.ToString();
        }
    }
}