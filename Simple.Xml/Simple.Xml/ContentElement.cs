using System;

namespace Simple.Xml.Structure
{
    public class ContentElement : IElement
    {
        private readonly string content;

        public ContentElement(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            this.content = content;
        }

        public void AddChild(IElement child)
        {
            throw new System.NotImplementedException();
        }

        public IElement NewChild(string childName)
        {
            throw new System.NotImplementedException();
        }

        public void Accept(IUpwardElementVisitor visitor)
        {
            visitor.Visit(content);
        }

        public void Accept(IDownwardElementVisitor visitor)
        {
            visitor.Visit(content);
        }
    }
}