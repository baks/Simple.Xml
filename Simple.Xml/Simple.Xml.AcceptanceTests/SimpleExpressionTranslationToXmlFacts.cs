using Xunit;

namespace Simple.Xml.AcceptanceTests
{
    public class SimpleExpressionTranslationToXmlFacts
    {
        [Fact]
        public void ShouldTranslateRootElementToXml()
        {
            var xmlBuilder = XmlBuilder.Start();

            var xml = xmlBuilder.Root.ToXml();

            Assert.Equal("<root></root>", xml);
        }
    }
}
