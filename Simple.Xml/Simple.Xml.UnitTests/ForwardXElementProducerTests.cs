using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
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

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithChildren(
            Tag tag, 
            [Frozen]ElementName elementName,
            IEnumerable<Element> children)
        {
            sut.Visit(tag, children);

            var result = sut.ToXElement();

            Assert.Equal(children.Count(), result.Elements().Count());
            Assert.Equal(elementName.ToXName(), result.Elements().First().Name);
            Assert.Equal(elementName.ToXName(), result.Elements().ElementAt(1).Name);
            Assert.Equal(elementName.ToXName(), result.Elements().ElementAt(2).Name);
        }

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithContent(Tag aTag, string content)
        {
            sut.Visit(aTag, ANY_CHILDREN);
            sut.Visit(content);

            var result = sut.ToXElement();

            Assert.NotNull(result);
            Assert.Equal(content, result.Value);
        }
    }
}
