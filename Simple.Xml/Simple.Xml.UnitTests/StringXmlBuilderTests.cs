using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class StringXmlBuilderTests
    {
        private readonly IEnumerable<Attribute> EMPTY_ATTRIBUTES = Enumerable.Empty<Attribute>(); 

        private readonly StringXmlBuilder sut = new StringXmlBuilder(new StringBuilder());

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.WriteStartTagFor(name, EMPTY_ATTRIBUTES);
            sut.WriteEndTag();

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void AddsContentToOutput(string name, string content)
        {
            sut.WriteStartTagFor(name, EMPTY_ATTRIBUTES);
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
            sut.WriteStartTagFor(aTag, EMPTY_ATTRIBUTES);
            sut.WriteEndTag();

            var exception = Record.Exception(()=> sut.WriteContent(aContent));

            Assert.IsAssignableFrom<InvalidOperationException>(exception);
        }
    }
}
