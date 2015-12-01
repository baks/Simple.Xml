using NSubstitute;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicForwardXmlStringCreatorTests
    {
        private readonly IElement element = Substitute.For<IElement>();
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly DynamicForwardXmlCreator sut;

        public DynamicForwardXmlStringCreatorTests()
        {
            downwardVisitor = Substitute.For<IDownwardElementVisitor>();
            sut = new DynamicForwardXmlCreator(downwardVisitor);
        }

        [Fact]
        public void PassesDownwardVisitorToElement()
        {
            sut.Visit(element);

            element.Received(1).Accept(Arg.Any<IDownwardElementVisitor>());
        }
    }
}
