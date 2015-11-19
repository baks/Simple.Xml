using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public class DynamicForwardXmlStringCreator : IDynamicElementVisitor
    {
        private readonly ForwardXmlStringProducer producer = new ForwardXmlStringProducer(new StringXmlBuilder());

        public void Visit(IElement element) => element.Accept(producer);

        public override string ToString() => producer.ToString();
    }
}