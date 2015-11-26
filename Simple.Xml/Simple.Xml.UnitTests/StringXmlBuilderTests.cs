using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;
using Attribute = Simple.Xml.Structure.Constructs.Attribute;

namespace Simple.Xml.Structure.UnitTests
{
    public class StringXmlBuilderTests
    {
        private static Tag ANY_TAG => Arg.Any<Tag>();

        private static Tag TagFor(string name) => new Tag(new TagName(name, NamespacePrefix.EmptyNamespacePrefix), Enumerable.Empty<Attribute>());

        private readonly IEnumerable<Attribute> EMPTY_ATTRIBUTES = Enumerable.Empty<Attribute>();

        private readonly StringXmlBuilder sut = new StringXmlBuilder(new StringBuilder());

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.WriteStartTagFor(TagFor(name));
            sut.WriteEndTag();

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void AddsContentToOutput(string name, string content)
        {
            sut.WriteStartTagFor(TagFor(name));
            sut.WriteContent(content);
            sut.WriteEndTag();

            Assert.Equal($"<{name}>{content}</{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void AddsNamespaceToOutput(Namespaces namespaces, Tag tag)
        {
            sut.UseNamespaces(namespaces);
            sut.WriteStartTagFor(tag);

            Assert.Equal($"<{tag} {namespaces}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void DoesNotAllowToWriteAContentWithoutStartingATag(string aContent)
        {
            var exception = Record.Exception(() => sut.WriteContent(aContent));

            Assert.IsAssignableFrom<InvalidOperationException>(exception);
        }

        [Theory, AutoSubstituteData]
        public void DoesNotAllowToEndTagWithoutStartingIt()
        {
            var exception = Record.Exception(() => sut.WriteEndTag());

            Assert.IsAssignableFrom<InvalidOperationException>(exception);
        }

        [Theory, AutoSubstituteData]
        public void DoesNotAllowToWriteContentAfterTag(Tag aTag, string aContent)
        {
            sut.WriteStartTagFor(aTag);
            sut.WriteEndTag();

            var exception = Record.Exception(()=> sut.WriteContent(aContent));

            Assert.IsAssignableFrom<InvalidOperationException>(exception);
        }
    }
}
