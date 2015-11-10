using NSubstitute;
using Simple.Xml.Structure;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicBackwardXmlCreatorTests
    {
        private readonly IElement element = Substitute.For<IElement>();
        private readonly DynamicBackwardXmlCreator sut = new DynamicBackwardXmlCreator();

        [Fact]
        public void PassesUpwardVisitorToElement()
        {
            sut.Visit(element);

            element.Received(1).Accept(Arg.Any<IUpwardElementVisitor>());
        }
    }
}
