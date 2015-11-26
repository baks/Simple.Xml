using System.Dynamic;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Dynamic.Output;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicToXElementBackwardHandlerTests
    {
        private readonly BaseDynamicElement dynamicElement;
        private readonly DynamicToXElementBackwardHandler sut;

        public DynamicToXElementBackwardHandlerTests()
        {
            dynamicElement = Substitute.For<BaseDynamicElement>();
            sut = new DynamicToXElementBackwardHandler(dynamicElement);
        }

        [Theory, AutoSubstituteData]
        public void GuardsCollaboratorsInConstructor(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(DynamicToXElementBackwardHandler).GetConstructors());
        }

        [Fact]
        public void IsBaseDynamicElementDecorator()
        {
            Assert.IsAssignableFrom<DynamicElementDecorator>(sut);
        }

        [Fact]
        public void HandlesToXNameMethodInvocation()
        {
            Record.Exception(() => ((dynamic)sut).ToXElement());

            var invokeMember = Arg.Any<InvokeMemberBinder>();
            var args = Arg.Any<object[]>();
            var obj = Arg.Any<object>();
            dynamicElement.DidNotReceive().TryInvokeMember(invokeMember, args, out obj);
        }
    }
}
