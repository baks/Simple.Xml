using System.Collections.Generic;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class ElementTests
    {
        private static readonly string ANY_NAME = "any";
        private static readonly IElement UNUSED_PARENT = new NullObjectElement();

        private readonly IUpwardElementVisitor upwardVisitor = Substitute.For<IUpwardElementVisitor>();
        private readonly IDownwardElementVisitor downwardVisitor = Substitute.For<IDownwardElementVisitor>();
        private readonly Element sut = new Element(ANY_NAME, UNUSED_PARENT);

        [Theory, AutoSubstituteData]
        public void GuardCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (Element).GetConstructors());
        }

        [Fact]
        public void VisitsUpwardVisitor()
        {
            sut.Accept(upwardVisitor);

            upwardVisitor.Received(1).Visit(Arg.Any<string>(), Arg.Any<IElement>(), Arg.Any<IEnumerable<IElement>>());
        }

        [Theory, AutoSubstituteData]
        public void PassNameToUpwardVisitor(string name)
        {
            ElementWithName(name).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(name, Arg.Any<IElement>(), Arg.Any<IEnumerable<IElement>>());
        }

        [Theory, AutoSubstituteData]
        public void PassParentToUpwardVisitor(IElement parent)
        {
            ElementWithParent(parent).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(Arg.Any<string>(), parent, Arg.Any<IEnumerable<IElement>>());
        }

        private void AssertUpwardVisitorIsVisitedWith(string name, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(name, parent, children);

        private static Element ElementWithName(string name) => new Element(name, UNUSED_PARENT);

        private static Element ElementWithParent(IElement parent) => new Element(ANY_NAME, parent);
    }
}
