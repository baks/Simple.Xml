using System.Dynamic;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Dynamic.Output;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicToXElementForwardHandlerTests
    {
        private readonly BaseDynamicElement dynamicElement;
        private readonly DynamicToXElementForwardHandler sut;

        public DynamicToXElementForwardHandlerTests()
        {
            dynamicElement = Substitute.For<BaseDynamicElement>();
            sut = new DynamicToXElementForwardHandler(dynamicElement);
        }

        [Fact]
        public void IsDynamicDecorator()
        {
            Assert.IsAssignableFrom<DynamicElementDecorator>(sut);
        }

        [Theory, AutoSubstituteData]
        public void GuardsCollaboratorsInConstructor(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(DynamicToXElementForwardHandler).GetConstructors());
        }

        [Fact]
        public void HandlesToXElementInvocation()
        {
            Record.Exception(() => ((dynamic)sut).ToXElement());

            var invokeMember = Arg.Any<InvokeMemberBinder>();
            var args = Arg.Any<object[]>();
            var obj = Arg.Any<object>();
            dynamicElement.DidNotReceive().TryInvokeMember(invokeMember, args, out obj);
        }
    }
}
