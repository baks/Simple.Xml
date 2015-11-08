using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class DynamicElementTests
    {
        private readonly IElement element;
        private readonly dynamic sut;

        public DynamicElementTests()
        {
            element = Substitute.For<IElement>();
            sut = new DynamicElement(element);
        }

        [Theory, AutoSubstituteData]
        public void GuardsCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(DynamicElement).GetConstructors());
        }

        [Fact]
        public void MemberInvocationAddsNewChildToElementWithMemberName()
        {
            var p = sut.Head;

            element.Received(1).NewChild("Head");
        }
    }
}
