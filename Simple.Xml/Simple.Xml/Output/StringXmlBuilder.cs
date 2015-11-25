using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Structure.Output
{
    public class StringXmlBuilder : IXmlBuilder
    {
        private readonly StringBuilder stringBuilder;
        private readonly Stack<Tag> tagsStack;
        private readonly Stack<Namespaces> namespacesStack; 

        public StringXmlBuilder(StringBuilder stringBuilder)
        {
            if (stringBuilder == null)
            {
                throw new ArgumentNullException(nameof(stringBuilder));
            }
            this.stringBuilder = stringBuilder;
            this.tagsStack = new Stack<Tag>();
            this.namespacesStack = new Stack<Namespaces>();
        }

        public void WriteStartTagFor(Tag tag)
        {
            if (namespacesStack.Count == 0)
            {
                stringBuilder.Append($"<{tag}>");
            }
            else
            {
                stringBuilder.Append($"<{tag} {namespacesStack.Pop()}>");
            }
            tagsStack.Push(tag);
        }

        public void WriteEndTag()
        {
            if (tagsStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot write end tag without start tag");
            }
            EndTag(tagsStack.Pop());
        }

        public void WriteContent(string content)
        {
            if (tagsStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot write content when no element");
            }
            stringBuilder.Append(content);
        }

        public void UseNamespaces(Namespaces namespaces)
        {
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            if(namespaces != Namespaces.EmptyNamespaces)
            namespacesStack.Push(namespaces);
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        private void EndTag(Tag tag)
        {
            stringBuilder.Append($"</{tag.tagName}>");
        }
    }
}