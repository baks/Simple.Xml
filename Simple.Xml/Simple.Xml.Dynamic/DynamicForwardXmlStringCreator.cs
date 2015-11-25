using System.Text;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Output;

namespace Simple.Xml.Dynamic
{
    public class DynamicForwardXmlStringCreator : IDynamicElementVisitor
    {
        private readonly ForwardXmlStringProducer producer;

        public DynamicForwardXmlStringCreator()
        {
            var stringBuilder = new StringBuilder();
            producer = new ForwardXmlStringProducer(new PrettyPrintStringXmlBuilder(new StringXmlBuilder(stringBuilder), stringBuilder));
        }

        public void Visit(IElement element) => element.Accept(producer);

        public override string ToString() => producer.ToString();
    }
}