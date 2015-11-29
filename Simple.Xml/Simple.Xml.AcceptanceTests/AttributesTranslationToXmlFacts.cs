using Simple.Xml.Dynamic;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Xunit;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public class AttributesTranslationToXmlFacts : BaseTestFixtureWithOutput
    {
        public AttributesTranslationToXmlFacts(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void ShouldAddAttributesToElement()
        {
            var doc = sut.NewDocument;

            doc.Head.Body(new Attributes { { "name", "body" }, { "style", "top:456px" } });

            var xml = doc.ToXml();

            Assert.Equal(@"<Head>
    <Body name=""body"" style=""top:456px"">
    </Body>
</Head>", xml);
        }

        [Fact]
        public void ShouldAddNamespaceToAttributeToElement()
        {
            var sut = new DynamicXmlBuilder(new Namespaces {{"c", "http://www.w3.org"}});
            var doc = sut.NewDocument;

            doc.Head.Body(new Attributes {{"c_name", "body"}, {"c_style", "top:456px"}});

            var xml = doc.ToXml();

            Assert.Equal(@"<Head xmlns:c=""http://www.w3.org"">
    <Body c:name=""body"" c:style=""top:456px"">
    </Body>
</Head>", xml);
        }

    }
}
