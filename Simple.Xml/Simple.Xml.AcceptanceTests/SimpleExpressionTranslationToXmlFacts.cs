using System;
using System.Collections.Generic;
using System.Linq;
using Simple.Xml.Dynamic;
using Simple.Xml.Structure;
using Xunit;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public class SimpleExpressionTranslationToXmlFacts : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly IOutput output = new StringBuilderOutput();

        public SimpleExpressionTranslationToXmlFacts(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            XmlBuilder.DecorateElement = element => new VerboseElement(element, output);
        }

        [Fact]
        public void ShouldTranslatePropertyChain()
        {
            var xml = XmlBuilder.NewDocument.Head.Body.Div.ToXml();

            Assert.Equal(RemoveWhiteSpaces(@"<Head>
    <Body>
        <Div>
        </Div>
    </Body>
</Head>"), xml);
        }

        [Fact]
        public void ShouldTranslateMoreThanOneElementForTheSameNode()
        {
            var doc = XmlBuilder.NewDocument;
            var body = doc.Head.Body;
            var firstDiv = body.Div.P1;
            var secondDiv = body.Div.P2;

            var xml = doc.ToXml();

            Assert.Equal(@"<Head>
    <Body>
        <Div>
            <P1>
            </P1>
        </Div>
        <Div>
            <P2>
            </P2>
        </Div>
    </Body>
</Head>", xml);
        }

        [Fact]
        public void ShouldAddContentToElement()
        {
            var doc = XmlBuilder.NewDocument;

            var body = doc.Head.Body;
            body.Div.P1 = "content";

            var xml = doc.ToXml();

            Assert.Equal(@"<Head>
    <Body>
        <Div>
            <P1>
                content
            </P1>
        </Div>
    </Body>
</Head>", xml);
        }

        [Fact]
        public void ShouldAddAttributesToElement()
        {
            var doc = XmlBuilder.NewDocument;

            doc.Head.Body = new Attributes {{"name", "body"}, {"style", "top:456px"}};

            var xml = doc.ToXml();

            Assert.Equal(@"
<Head>
    <Body name=""body"" style=""top:456px"">
    </Body>
</Head>", xml);
        }

        public void Dispose()
        {
            testOutputHelper.WriteLine(output.ToString());
        }

        private static string RemoveWhiteSpaces(string input)
            => new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }
}
