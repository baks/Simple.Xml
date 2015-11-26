using NSubstitute;
using Simple.Xml.Dynamic.Output;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicToXmlBackwardHandlerTests
    {
        private readonly BaseDynamicElement dynamicElement;
        private readonly DynamicToXmlBackwardHandler sut;

        public DynamicToXmlBackwardHandlerTests()
        {
            dynamicElement = Substitute.For<BaseDynamicElement>();
            sut = new DynamicToXmlBackwardHandler(dynamicElement);
        }

        [Fact]
        public void CreatesXmlByPassingDynamicBackwardXmlCreatorToDynamicElement()
        {
            sut.ToXml();

            dynamicElement.Received(1).Accept(Arg.Any<DynamicBackwardXmlCreator>());
        }

        [Fact]
        public void DynamicInvocationCreatesXmlByPassingBackwardXmlCreatorToDynamicElement()
        {
            ((dynamic) sut).ToXml();

            dynamicElement.Received(1).Accept(Arg.Any<DynamicBackwardXmlCreator>());
        }
    }
}
