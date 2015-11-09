using Simple.Xml.Structure;

namespace Simple.Xml.Dynamic
{
    public class DynamicBackwardXmlCreator : IDynamicElementVisitor
    {
        private BackwardXmlStringProducer producer;
        public void Visit(IElement element)
        {
            producer = new BackwardXmlStringProducer();
            element.Accept(producer);
        }

        public override string ToString()
        {
            return producer.ToString();
        }
    }
}