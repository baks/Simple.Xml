using NSubstitute;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicToXmlForwardHandlerTests
    {
        private readonly BaseDynamicElement dynamicElement;
        private readonly DynamicToXmlForwardHandler sut;

        public DynamicToXmlForwardHandlerTests()
        {
            dynamicElement = Substitute.For<BaseDynamicElement>();
            sut = new DynamicToXmlForwardHandler(dynamicElement);
        }

        [Fact]
        public void CreatesXmlByPassingForwardXmlCreatorToDynamicElement()
        {
            sut.ToXml();

            dynamicElement.Received(1).Accept(Arg.Any<DynamicForwardXmlStringCreator>());
        }

        [Fact]
        public void DynamicInvocationCreatesXmlByPassingForwardXmlCreatorToDynamicElement()
        {
            ((dynamic) sut).ToXml();

            dynamicElement.Received(1).Accept(Arg.Any<DynamicForwardXmlStringCreator>());
        }
    }
}
