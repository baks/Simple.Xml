using System;
using Attribute = Simple.Xml.Structure.Constructs.Attribute;

namespace Simple.Xml.Structure
{
    public class ContentElement : IElement, IEquatable<IElement>, IEquatable<ContentElement>
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
        }

        public void AddAttribute(Attribute attr)
        {
        }

        public void Accept(IUpwardElementVisitor visitor)
        {
            visitor.Visit(content);
        }

        public void Accept(IDownwardElementVisitor visitor)
        {
            visitor.Visit(content);
        }

        public bool Equals(ContentElement other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.content, other.content, StringComparison.Ordinal);
        }

        public bool Equals(IElement other)
        {
            return CheckWhetherContentElementAndCheckEquableness(other);
        }

        public override bool Equals(object obj)
        {
            return CheckWhetherContentElementAndCheckEquableness(obj);
        }

        public override int GetHashCode()
        {
            return content.GetHashCode();
        }

        private bool CheckWhetherContentElementAndCheckEquableness(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(ContentElement))
            {
                return false;
            }

            return this.Equals((ContentElement)obj);
        }

        public static bool operator ==(ContentElement contentElementA, ContentElement contentElementB)
        {
            return Equals(contentElementA, contentElementB);
        }

        public static bool operator !=(ContentElement contentElementA, ContentElement contentElementB)
        {
            return !Equals(contentElementA, contentElementB);
        }
    }
}