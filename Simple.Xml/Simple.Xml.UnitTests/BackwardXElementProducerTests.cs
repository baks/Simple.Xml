using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NSubstitute;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class BackwardXElementProducerTests
    {
        private static IElement ANY_PARENT => Arg.Any<IElement>();
        private static IEnumerable<IElement> SOME_CHILDREN => Arg.Any<IEnumerable<IElement>>(); 
        private readonly BackwardXElementProducer sut = new BackwardXElementProducer();

        [Fact]
        public void IsUpwardVisitor()
        {
            Assert.IsAssignableFrom<IUpwardElementVisitor>(sut);
        }

        [Theory, AutoSubstituteData]
        public void CreatesXElementWithPassedTag(Tag tag)
        {
            sut.Visit(tag, ANY_PARENT, SOME_CHILDREN);

            var result = sut.ToXElement();

            Assert.NotNull(result);
            Assert.Equal(tag.tagName.name, result.Name.LocalName);
            Assert.Equal(tag.tagName.namespacePrefix.prefix, result.Name.NamespaceName);
            Assert.True(tag.attributes.Select(attr => attr.ToXAttribute())
                .SequenceEqual(result.Attributes(), new XAttributeComparer()));
        }
    }

    public class XAttributeComparer : IEqualityComparer<XAttribute>
    {
        public bool Equals(XAttribute x, XAttribute y)
        {
            return x.Name == y.Name && x.Value == y.Value;
        }

        public int GetHashCode(XAttribute obj)
        {
            return obj.Name.GetHashCode() ^ obj.Value.GetHashCode();
        }
    }
}
