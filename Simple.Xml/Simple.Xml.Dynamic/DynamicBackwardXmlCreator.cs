using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public class DynamicBackwardXmlCreator : IDynamicElementVisitor
    {
        private readonly BackwardXmlStringProducer producer = new BackwardXmlStringProducer();

        public void Visit(IElement element) => element.Accept(producer);

        public override string ToString() => producer.ToString();
    }
}