using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public class SimpleExpressionTranslationToXmlFacts : BaseTestFixtureWithOutput
    {

        public SimpleExpressionTranslationToXmlFacts(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void ShouldTranslatePropertyChain()
        {
            var xml = sut.NewDocument.Head.Body.Div.ToXml();

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
            var doc = sut.NewDocument;
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
            var doc = sut.NewDocument;

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
        public void ShouldTranslateMethodInvocations()
        {
            var doc = sut.NewDocument;

            doc.Head1();
            doc.Head2();
            doc.Head3();

            var xml = doc.ToXml();

            Assert.Equal(@"<Head1>
</Head1>
<Head2>
</Head2>
<Head3>
</Head3>", xml);
        }

        [Fact]
        public void ShouldTranslateMethodInvocationsWhenUsedInDeeperLevels()
        {
            var doc = sut.NewDocument;

            var body = doc.Head1.Body;
            body.Element();
            body.Element();

            var xml = doc.ToXml();

            Assert.Equal(@"<Head1>
    <Body>
        <Element>
        </Element>
        <Element>
        </Element>
    </Body>
</Head1>", xml);
        }

        private static string RemoveWhiteSpaces(string input)
            => new string(input.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }
}
