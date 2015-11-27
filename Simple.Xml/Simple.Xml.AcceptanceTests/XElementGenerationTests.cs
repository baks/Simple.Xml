using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Simple.Xml.Dynamic;
using Simple.Xml.Structure.Constructs;
using Xunit;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public class XElementGenerationTests : BaseTestFixtureWithOutput
    {
        public XElementGenerationTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void ShouldTranslateToXElementSimplePropertyChain()
        {
            var xElement = sut.NewDocument.Head.ToXElement() as XElement;

            Assert.NotNull(xElement);
            Assert.Equal(XName.Get("Head"), xElement.Name);
        }

        [Fact]
        public void ShouldTranslateToXElementWithNamespaces()
        {
            var xElement = sut.NewDocument.c_Head.ToXElement() as XElement;

            Assert.NotNull(xElement);
            Assert.Equal(XName.Get("Head", "c"), xElement.Name);
        }

        [Fact]
        public void ShouldTranslateToXElementMoreAdvancedDocument()
        {
            var doc = sut.NewDocument;
            doc.c_Head.c_Body.c_Div();
            var xElement = doc.ToXElement() as XElement;

            Assert.NotNull(xElement);
            Assert.Equal(XName.Get("Head", "c"), xElement.Name);

            Assert.Equal(1, xElement.Elements().Count());
            var cBodyChild = xElement.Elements().First();
            Assert.Equal(XName.Get("Body","c"), cBodyChild.Name);

            Assert.Equal(1, cBodyChild.Elements().Count());
            var cDivChild = cBodyChild.Elements().First();
            Assert.Equal(XName.Get("Div", "c"), cDivChild.Name);
        }

        [Fact]
        public void ShouldUseNamespaceNameFromConfiguration()
        {
            sut = new DynamicXmlBuilder(new Namespaces{ {"c", "http://www.w3.org" } });
            var xElement = sut.NewDocument.c_Head.ToXElement() as XElement;

            Assert.NotNull(xElement);
            Assert.Equal(XName.Get("Head", "http://www.w3.org"), xElement.Name);
        }

        [Fact]
        public void ShouldAddAttributesToXElement()
        {
            var doc = sut.NewDocument;
            var attrName = "c";
            var attrValue = "0";
            var attributes = new Attributes {{attrName, attrValue}};
            doc.Head = attributes;

            var xElement = doc.ToXElement() as XElement;

            Assert.NotNull(xElement);
            Assert.Equal(1, xElement.Attributes().Count());

            attributes.Iterator()
                .Select(attr => attr.ToXAttribute())
                .SequenceEqual(xElement.Attributes(), new XAttributeComparer());
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
}
