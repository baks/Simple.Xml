namespace Simple.Xml.Dynamic
{
    public class DynamicForwardXmlStringCreator : IDynamicElementVisitor
    {
        private ForwardXmlStringProducer producer;

        public void Visit(IElement element)
        {
            producer = new ForwardXmlStringProducer();
            element.Accept(producer);
        }

        public override string ToString()
        {
            return producer.ToString();
        }
    }
}