using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ElementTests
    {
        private static readonly IElement UNUSED_PARENT = new NullObjectElement();
        private static readonly IElementCollector UNUSED_COLLECTOR = Substitute.For<IElementCollector>();
        private static string ANY_NAME => Arg.Any<string>();

        private static ElementName ANY_ELEMENT_NAME => Arg.Any<ElementName>();
        private static IElement ANY_PARENT => Arg.Any<IElement>();
        private static Tag ANY_TAG => Arg.Any<Tag>();
        private static IEnumerable<IElement> EMPTY_CHILDREN => Arg.Any<IEnumerable<IElement>>();

        private readonly IElementCollector collector;
        private readonly IUpwardElementVisitor upwardVisitor;
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly Element sut;

        public ElementTests()
        {
            collector = Substitute.For<IElementCollector>();
            upwardVisitor = Substitute.For<IUpwardElementVisitor>();
            downwardVisitor = Substitute.For<IDownwardElementVisitor>();
            sut = new Element(anElementName, UNUSED_PARENT, collector);
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

            upwardVisitor.Received(1).Visit(ANY_NAME, ANY_PARENT, EMPTY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToUpwardVisitor(string name)
        {
            ElementWithName(name).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(name, ANY_PARENT, EMPTY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesParentToUpwardVisitor(IElement parent)
        {
            ElementWithParent(parent).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(ANY_NAME, parent, EMPTY_CHILDREN);
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

            downwardVisitor.Received(1).Visit(ANY_TAG, EMPTY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToDownwardVisitor(string name)
        {
            ElementWithName(name).Accept(downwardVisitor);

            AssertDownwardVisitorIsVisitedWith(TagContentWithName(name), EMPTY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesChildrenToDownwardVisitor(IEnumerable<string> childrenNames)
        {
            sut.Accept(downwardVisitor);

            collector.Received(1).ChildrenFor(sut);
        }

        private void AssertUpwardVisitorIsVisitedWith(string name, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(name, parent, children);

        private void AssertDownwardVisitorIsVisitedWith(Tag tag, IEnumerable<IElement> children)
            => downwardVisitor.Received().Visit(tag, children);

        private static IElement ElementWithName(string name) => new Element(new ElementName(name), UNUSED_PARENT, UNUSED_COLLECTOR);

        private static IElement ElementWithParent(IElement parent) => new Element(anElementName, parent, UNUSED_COLLECTOR);

        private static Tag TagContentWithName(string name)
            => Arg.Is<Tag>(tagContent => tagContent.name.Equals(name));

        private static ElementName anElementName => new ElementName("any");
    }
}
