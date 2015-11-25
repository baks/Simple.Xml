using Simple.Xml.Structure;
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

            doc.Head.Body = new Attributes { { "name", "body" }, { "style", "top:456px" } };

            var xml = doc.ToXml();

            Assert.Equal(@"<Head>
    <Body name=""body"" style=""top:456px"">
    </Body>
</Head>", xml);
        }

    }
}
