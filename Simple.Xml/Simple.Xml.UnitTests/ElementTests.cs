using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ElementTests
    {
        private static readonly string ANY_NAME = "any";
        private static readonly IElement UNUSED_PARENT = new NullObjectElement();
        private static readonly IElementCollector UNUSED_COLLECTOR = Substitute.For<IElementCollector>();

        private readonly IElementCollector collector;
        private readonly IUpwardElementVisitor upwardVisitor;
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly Element sut;

        public ElementTests()
        {
            collector = Substitute.For<IElementCollector>();
            upwardVisitor = Substitute.For<IUpwardElementVisitor>();
            downwardVisitor = Substitute.For<IDownwardElementVisitor>();
            sut = new Element(ANY_NAME, UNUSED_PARENT, collector);
    }

        [Theory, AutoSubstituteData]
        public void GuardsCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (Element).GetConstructors());
        }

        [Theory, AutoSubstituteData]
        public void AddsChildToCollector(IElement aChild)
        {
            sut.AddChild(aChild);

            collector.Received(1).AddElement(aChild);
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
            sut.Accept(upwardVisitor);

            collector.Received(1).ChildrenFor(sut);
        }

        [Fact]
        public void VisitsDownwardVisitor()
        {
            sut.Accept(downwardVisitor);

            downwardVisitor.Received(1).Visit(AName, AnElementsEnumerable, AnAttributesEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToDownwardVisitor(string name)
        {
            ElementWithName(name).Accept(downwardVisitor);

            AssertDownwardVisitorIsVisitedWith(name, AnElementsEnumerable, AnAttributesEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesChildrenToDownwardVisitor(IEnumerable<string> childrenNames)
        {
            sut.Accept(downwardVisitor);

            collector.Received(1).ChildrenFor(sut);
        }

        private void AssertUpwardVisitorIsVisitedWith(string name, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(name, parent, children);

        private void AssertDownwardVisitorIsVisitedWith(string name, IEnumerable<IElement> children, IEnumerable<Attribute> attributes)
            => downwardVisitor.Received().Visit(name, children, attributes);

        private static IElement ElementWithName(string name) => new Element(name, UNUSED_PARENT, UNUSED_COLLECTOR);

        private static IElement ElementWithParent(IElement parent) => new Element(ANY_NAME, parent, UNUSED_COLLECTOR);

        private static string AName => Arg.Any<string>();

        private static IElement AParent => Arg.Any<IElement>();

        private static IEnumerable<IElement> AnElementsEnumerable => Arg.Any<IEnumerable<IElement>>();

        private static IEnumerable<Attribute> AnAttributesEnumerable => Arg.Any<IEnumerable<Attribute>>();
    }
}
