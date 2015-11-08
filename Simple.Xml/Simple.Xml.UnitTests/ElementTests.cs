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
        public void GuardsCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (Element).GetConstructors());
        }

        [Fact]
        public void VisitsUpwardVisitor()
        {
            sut.Accept(upwardVisitor);

            upwardVisitor.Received(1).Visit(AString, AnElement, AElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesNameToUpwardVisitor(string name)
        {
            ElementWithName(name).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(name, AnElement, AElementsEnumerable);
        }

        [Theory, AutoSubstituteData]
        public void PassesParentToUpwardVisitor(IElement parent)
        {
            ElementWithParent(parent).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(AString, parent, AElementsEnumerable);
        }

        /*[Theory, AutoSubstituteData]
        public void PassesChildrenToUpwardVisitor(string childName)
        {
            ElementWithChild(childName).Accept(upwardVisitor);

            AssertUpwardVisitorIsVisitedWith(AString, AnElement, EnumerableWithElements(childName));
        }*/

        private void AssertUpwardVisitorIsVisitedWith(string name, IElement parent, IEnumerable<IElement> children)
            => upwardVisitor.Received().Visit(name, parent, children);

        private static IElement ElementWithName(string name) => new Element(name, UNUSED_PARENT);

        private static IElement ElementWithParent(IElement parent) => new Element(ANY_NAME, parent);

        private static IElement ElementWithChild(IElement child)
        {
            var element = new Element(ANY_NAME, UNUSED_PARENT);
            element.AddChild(child);

            return element;
        }

        private static string AString => Arg.Any<string>();

        private static IElement AnElement => Arg.Any<IElement>();

        private static IEnumerable<IElement> AElementsEnumerable => Arg.Any<IEnumerable<IElement>>();

        /*private static IEnumerable<IElement> EnumerableWithElements(string childName)
            => Arg.Is<IEnumerable<IElement>>(c => c.SequenceEqual(new [] {child}));*/
    }
}
