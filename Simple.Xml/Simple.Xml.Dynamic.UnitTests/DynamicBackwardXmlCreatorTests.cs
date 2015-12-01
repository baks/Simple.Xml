using NSubstitute;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicBackwardXmlCreatorTests
    {
        private readonly IElement element = Substitute.For<IElement>();
        private readonly IUpwardElementVisitor upwardVisitor;
        private readonly DynamicBackwardXmlCreator sut;

        public DynamicBackwardXmlCreatorTests()
        {
            upwardVisitor = Substitute.For<IUpwardElementVisitor>();
            sut = new DynamicBackwardXmlCreator(upwardVisitor);
        }

        [Fact]
        public void PassesUpwardVisitorToElement()
        {
            sut.Visit(element);

            element.Received(1).Accept(Arg.Any<IUpwardElementVisitor>());
        }
    }
}
