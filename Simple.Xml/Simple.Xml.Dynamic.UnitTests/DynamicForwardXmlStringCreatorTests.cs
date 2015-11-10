using NSubstitute;
using Simple.Xml.Structure;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicForwardXmlStringCreatorTests
    {
        private readonly IElement element = Substitute.For<IElement>();
        private readonly DynamicForwardXmlStringCreator sut = new DynamicForwardXmlStringCreator();

        [Fact]
        public void PassesDownwardVisitorToElement()
        {
            sut.Visit(element);

            element.Received(1).Accept(Arg.Any<IDownwardElementVisitor>());
        }
    }
}
