using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Structure;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicElementTests
    {
        private readonly IElement element;
        private readonly dynamic sut;
        private readonly IDynamicElementVisitor visitor;

        public DynamicElementTests()
        {
            visitor = Substitute.For<IDynamicElementVisitor>();
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

        [Fact]
        public void AcceptsDynamicElementVisitor()
        {
            sut.Accept(visitor);

            visitor.Received(1).Visit(Arg.Any<IElement>());
        }

        [Fact]
        public void PassesElementToVisitor()
        {
            sut.Accept(visitor);

            visitor.Received().Visit(element);
        }
    }
}
