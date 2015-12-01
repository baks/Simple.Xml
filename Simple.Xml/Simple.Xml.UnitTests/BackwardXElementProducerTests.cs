using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class BackwardXElementProducerTests
    {
        private static IElement ANY_PARENT => Arg.Any<IElement>();
        private static IEnumerable<IElement> NO_CHILDREN => Enumerable.Empty<IElement>(); 

        private readonly BackwardXElementProducer sut = new BackwardXElementProducer();

        [Fact]
        public void IsUpwardVisitor()
        {
            Assert.IsAssignableFrom<IUpwardElementVisitor>(sut);
        }

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithPassedTag(Tag tag)
        {
            sut.Visit(tag, ANY_PARENT, NO_CHILDREN);

            var result = sut.ToXElement();

            Assert.NotNull(result);
            Assert.Equal(tag.tagName.name, result.Name.LocalName);
            Assert.Equal(tag.tagName.namespacePrefix.prefix, result.Name.NamespaceName);
            Assert.True(tag.attributes.Select(attr => attr.ToXAttribute())
                .SequenceEqual(result.Attributes(), new XAttributeComparer()));
        }

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithContent(Tag aTag, string content)
        {
            sut.Visit(aTag, ANY_PARENT, NO_CHILDREN);
            sut.Visit(content);

            var result = sut.ToXElement();

            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }
    }
}
