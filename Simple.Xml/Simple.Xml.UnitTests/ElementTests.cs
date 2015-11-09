using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.UnitTests;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ElementTests
    {
        private static readonly string ANY_NAME = "any";
        private static readonly IElement UNUSED_PARENT = new NullObjectElement();

        private readonly IUpwardElementVisitor upwardVisitor = Substitute.For<IUpwardElementVisitor>();
        private readonly IDownwardElementVisitor downwardVisitor = Substitute.For<IDownwardElementVisitor>();
        private readonly Element sut = new Element(ANY_NAME, UNUSED_PARENT);

        [Theory, AutoSubstituteData]
        public void GuardsCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (Element).GetConstructors());
        }

        [Fact]
        public void VisitsUpwardVisitor()
        {
            sut.Accept(upwardVisitor);

            upwardVisitor.Received(1).Visit(AName, AParent, AnElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToUpwardVisitor(string name)
        {
            ElementWithName(name).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(name, AParent, AnElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesParentToUpwardVisitor(IElement parent)
        {
            ElementWithParent(parent).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(AName, parent, AnElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesChildrenToUpwardVisitor(IEnumerable<string> childrenNames)
        {
            var children = childrenNames.Select(childName => sut.NewChild(childName)).ToList();
            sut.Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(AName, AParent, EnumerableWithElements(children));
        }

        [Fact]
        public void VisitsDownwardVisitor()
        {
            sut.Accept(downwardVisitor);

            downwardVisitor.Received(1).Visit(AName, AnElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToDownwardVisitor(string name)
        {
            ElementWithName(name).Accept(downwardVisitor);

            AssertDownwardVisitorIsVisitedWith(name, AnElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesChildrenToDownwardVisitor(IEnumerable<string> childrenNames)
        {
            var children = childrenNames.Select(childName => sut.NewChild(childName)).ToList();
            sut.Accept(downwardVisitor);

            AssertDownwardVisitorIsVisitedWith(AName, EnumerableWithElements(children));
        }

        private void AssertUpwardVisitorIsVisitedWith(string name, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(name, parent, children);

        private void AssertDownwardVisitorIsVisitedWith(string name, IEnumerable<IElement> children)
            => downwardVisitor.Received().Visit(name, children);

        private static IElement ElementWithName(string name) => new Element(name, UNUSED_PARENT);

        private static IElement ElementWithParent(IElement parent) => new Element(ANY_NAME, parent);

        private static string AName => Arg.Any<string>();

        private static IElement AParent => Arg.Any<IElement>();

        private static IEnumerable<IElement> AnElementsEnumerable => Arg.Any<IEnumerable<IElement>>();

        private static IEnumerable<IElement> EnumerableWithElements(IEnumerable<IElement> children)
            => Arg.Is<IEnumerable<IElement>>(c => c.SequenceEqual(children));
    }
}
