using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class RootElementTests
    {
        private readonly IElementCollector collector;
        private readonly RootElement sut;
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly IUpwardElementVisitor upwardVisitor;

        public RootElementTests()
        {
            collector = Substitute.For<IElementCollector>();
            sut = new RootElement(collector);
            downwardVisitor = Substitute.For<IDownwardElementVisitor>();
            upwardVisitor = Substitute.For<IUpwardElementVisitor>();
        }

        [Theory, AutoSubstituteData]
        public void AddsChildrenToCollector(IElement aChild)
        {
            sut.AddChild(aChild);

            collector.Received(1).AddElement(aChild);
        }

        [Theory, AutoSubstituteData]
        public void PassesVisitorIntoAllChildren(IEnumerable<string> childrenNames)
        {
            childrenNames.Select(childName => sut.NewChild(childName)).ToList();
            sut.Accept(downwardVisitor);

            AssertPassesDownwardVisitorToAllChildren(childrenNames);
        }

        [Fact]
        public void DoesNotVisitUpwardVisitor()
        {
            sut.Accept(upwardVisitor);
            
            upwardVisitor.DidNotReceive().Visit(AName, AParent, AnElementsEnumerable);   
        }

        private void AssertPassesDownwardVisitorToAllChildren(IEnumerable<string> childrenNames)
        {
            foreach (var name in childrenNames)
            {
                downwardVisitor.Received(1).Visit(name, AnElementsEnumerable);
            }
        }

        private static string AName => Arg.Any<string>();

        private static IEnumerable<IElement> AnElementsEnumerable => Arg.Any<IEnumerable<IElement>>();

        private static IElement AParent => Arg.Any<IElement>();
    }
}
