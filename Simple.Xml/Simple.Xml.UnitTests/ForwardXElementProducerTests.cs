using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ForwardXElementProducerTests
    {
        private static IEnumerable<IElement> ANY_CHILDREN => Substitute.For<IEnumerable<IElement>>();
        private readonly ForwardXElementProducer sut = new ForwardXElementProducer();
        
        [Fact]
        public void IsDownwardVisitor()
        {
            Assert.IsAssignableFrom<IDownwardElementVisitor>(sut);
        }

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithPassedTag(Tag tag)
        {
            sut.Visit(tag, ANY_CHILDREN);

            var result = sut.ToXElement();

            Assert.Equal(tag.tagName.name, result.Name.LocalName);
            Assert.Equal(tag.tagName.namespacePrefix.prefix, result.Name.NamespaceName);

            tag.attributes.Select(attr => attr.ToXAttribute())
                .SequenceEqual(result.Attributes(), new XAttributeComparer());
        }
    }
}
