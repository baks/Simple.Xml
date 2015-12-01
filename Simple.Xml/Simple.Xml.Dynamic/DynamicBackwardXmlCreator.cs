using System;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class DynamicBackwardXmlCreator : IDynamicElementVisitor
    {
        private readonly IUpwardElementVisitor upwardVisitor;

        public DynamicBackwardXmlCreator(IUpwardElementVisitor upwardVisitor)
        {
            if (upwardVisitor == null)
            {
                throw new ArgumentNullException(nameof(upwardVisitor));
            }
            this.upwardVisitor = upwardVisitor;
        }

        public void Visit(IElement element) => element.Accept(upwardVisitor);

        public override string ToString() => upwardVisitor.ToString();
    }
}