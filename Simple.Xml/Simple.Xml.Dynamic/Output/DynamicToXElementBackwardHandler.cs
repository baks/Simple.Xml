using System;
using System.Dynamic;
using System.Xml.Linq;
using Simple.Xml.Structure.Output;

namespace Simple.Xml.Dynamic.Output
{
    public class DynamicToXElementBackwardHandler : DynamicElementDecorator
    {
        private readonly BaseDynamicElement baseDynamicElement;

        public DynamicToXElementBackwardHandler(BaseDynamicElement baseDynamicElement) : base(baseDynamicElement)
        {
            if (baseDynamicElement == null)
            {
                throw new ArgumentNullException(nameof(baseDynamicElement));
            }
            this.baseDynamicElement = baseDynamicElement;
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
            var producer = new BackwardXElementProducer();
            var creator = new DynamicBackwardXmlCreator(producer);
            Accept(creator);

            return producer.ToXElement();
        }
    }
}
