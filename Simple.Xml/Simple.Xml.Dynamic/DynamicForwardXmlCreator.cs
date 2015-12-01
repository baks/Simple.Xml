using System;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class DynamicForwardXmlCreator : IDynamicElementVisitor
    {
        private readonly IDownwardElementVisitor downwardVisitor;

        public DynamicForwardXmlCreator(IDownwardElementVisitor downwardVisitor)
        {
            if (downwardVisitor == null)
            {
                throw new ArgumentNullException(nameof(downwardVisitor));
            }
            this.downwardVisitor = downwardVisitor;
        }

        public void Visit(IElement element) => element.Accept(downwardVisitor);

        public override string ToString() => downwardVisitor.ToString();
    }
}