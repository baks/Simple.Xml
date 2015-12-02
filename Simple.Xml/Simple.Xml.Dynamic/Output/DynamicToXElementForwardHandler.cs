using System;
using System.Dynamic;
using System.Xml.Linq;
using Simple.Xml.Structure.Output;

namespace Simple.Xml.Dynamic.Output
{
    public class DynamicToXElementForwardHandler : DynamicElementDecorator
    {
        public DynamicToXElementForwardHandler(BaseDynamicElement dynamicElement) : base(dynamicElement)
        {
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var toXElementMethod = string.Equals("ToXElement", binder.Name, StringComparison.Ordinal);
            if (toXElementMethod)
            {
                result = ToXElement();
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        private XElement ToXElement()
        {
            var producer = new ForwardXElementProducer();
            var creator = new DynamicForwardXmlCreator(producer);
            Accept(creator);

            return producer.ToXElement();
        }
    }
}