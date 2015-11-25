using Simple.Xml.Dynamic;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Xunit;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public class NamespaceTranslationToXmlFacts : BaseTestFixtureWithOutput
    {
        public NamespaceTranslationToXmlFacts(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void ShouldAddNamespacePrefixToElement()
        {
            var doc = DynamicXmlBuilder.NewDocument;

            var bodyWithNamespacePrefix = doc.Head.c_Body;

            var xml = doc.ToXml();

            Assert.Equal(@"<Head>
    <c:Body>
    </c:Body>
</Head>", xml);
        }

        [Fact]
        public void ShouldAddNamespaceDeclaration()
        { 
            DynamicXmlBuilder.NamespaceDeclarations(new Namespaces { {"c", "http://www.w3.org/1999/xhtml" } });
            var doc = DynamicXmlBuilder.NewDocument;
            var bodyWithNamespacePrefix = doc.Head.c_Body;

            var xml = doc.ToXml();

            Assert.Equal(@"<Head xmlns:c=""http://www.w3.org/1999/xhtml"">
    <c:Body>
    </c:Body>
</Head>", xml);
        }
    }
}
