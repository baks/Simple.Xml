using System;
using NSubstitute;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class StringXmlBuilderTests
    {
        private readonly StringXmlBuilder sut = new StringXmlBuilder();

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.WriteStartTagFor(name);
            sut.WriteEndTag();

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void AddsContentToOutput(string name, string content)
        {
            sut.WriteStartTagFor(name);
            sut.WriteContent(content);
            sut.WriteEndTag();

            Assert.Equal($"<{name}>{content}</{name}>", sut.ToString());
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
        public void DoesNotAllowToWriteContentAfterTag(string aTag, string aContent)
        {
            sut.WriteStartTagFor(aTag);
            sut.WriteEndTag();

            var exception = Record.Exception(()=> sut.WriteContent(aContent));

            Assert.IsAssignableFrom<InvalidOperationException>(exception);
        }
    }
}
