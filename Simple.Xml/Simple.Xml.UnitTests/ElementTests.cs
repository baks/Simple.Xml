using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Structure.Constructs;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ElementTests
    {
        private static readonly Namespaces ANY_NAMESPACES = new Namespaces();
        private static readonly IElementCollector UNUSED_COLLECTOR = new ElementCollector();
        private static readonly IElement ANY_PARENT = new NullObjectElement();

        private static Tag ANY_TAG => Arg.Any<Tag>();

        private static IEnumerable<IElement> ANY_CHILDREN => Arg.Any<IEnumerable<IElement>>();

        private readonly IElementCollector collector;
        private readonly IUpwardElementVisitor upwardVisitor;
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly Element sut;

        public ElementTests()
        {
            collector = Substitute.For<IElementCollector>();
            upwardVisitor = Substitute.For<IUpwardElementVisitor>();
            downwardVisitor = Substitute.For<IDownwardElementVisitor>();
            sut = new Element(AnElementName, ANY_PARENT, collector);
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

            upwardVisitor.Received(1).Visit(ANY_TAG, ANY_PARENT, ANY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToUpwardVisitor(string name)
        {
            ElementWithName(name).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(TagContentWithName(name), ANY_PARENT, ANY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesParentToUpwardVisitor(IElement parent)
        {
            ElementWithParent(parent).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(ANY_TAG, parent, ANY_CHILDREN);
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

            downwardVisitor.Received(1).Visit(ANY_TAG, ANY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToDownwardVisitor(string name)
        {
            ElementWithName(name).Accept(downwardVisitor);

            AssertDownwardVisitorIsVisitedWith(TagContentWithName(name), ANY_CHILDREN);
        }

        [Theory, AutoSubstituteData]
        public void PassesChildrenToDownwardVisitor(IEnumerable<string> childrenNames)
        {
            sut.Accept(downwardVisitor);

            collector.Received(1).ChildrenFor(sut);
        }

        private void AssertUpwardVisitorIsVisitedWith(Tag tag, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(tag, parent, children);

        private void AssertDownwardVisitorIsVisitedWith(Tag tag, IEnumerable<IElement> children)
            => downwardVisitor.Received().Visit(tag, children);

        private static IElement ElementWithName(string name) => new Element(new ElementName(name, ANY_NAMESPACES), ANY_PARENT, UNUSED_COLLECTOR);

        private static IElement ElementWithParent(IElement parent) => new Element(AnElementName, parent, UNUSED_COLLECTOR);

        private static Tag TagContentWithName(string name)
            => Arg.Is<Tag>(tagContent => tagContent.tagName.name.Equals(name));

        private static ElementName AnElementName => new ElementName("any", ANY_NAMESPACES);
    }
}
