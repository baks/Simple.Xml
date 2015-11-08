using System.Dynamic;

namespace Simple.Xml
{
    public class DynamicToXmlBackwardHandler : BaseDynamicElement
    {
        private readonly BaseDynamicElement dynamicElement;

        public DynamicToXmlBackwardHandler(BaseDynamicElement dynamicElement)
        {
            this.dynamicElement = dynamicElement;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dynamicElement.TryGetMember(binder, out result);
        }

        public override void Accept(IDynamicElementVisitor visitor) => dynamicElement.Accept(visitor);

        public string ToXml()
        {
            var creator = new DynamicBackwardXmlCreator();
            this.dynamicElement.Accept(creator);

            return creator.ToString();
        }
    }
}