using System.Collections.Generic;

namespace Simple.Xml
{
    public class RootElement : IElement
    {
        private readonly List<IElement> children;

        public RootElement()
        {
            children = new List<IElement>();
        }

        public void AddChild(IElement child) => children.Add(child);

        public void Accept(IDownwardElementVisitor visitor)
        {
            foreach (var child in children)
            {
                child.Accept(visitor);
            }
        }

        public void Accept(IUpwardElementVisitor visitor) { }
    }
}