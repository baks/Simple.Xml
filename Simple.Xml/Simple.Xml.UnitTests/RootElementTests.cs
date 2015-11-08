using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class RootElementTests
    {
        private readonly RootElement sut = new RootElement();
        private readonly IDownwardElementVisitor downwardVisitor = Substitute.For<IDownwardElementVisitor>();
        private readonly IUpwardElementVisitor upwardVisitor = Substitute.For<IUpwardElementVisitor>();

        [Theory, AutoSubstituteData]
        public void PassesVisitorIntoAllChildren(IEnumerable<IElement> children)
        {
            foreach (var child in children)
            {
                sut.AddChild(child);
            }
            sut.Accept(downwardVisitor);

            AssertPassesDownwardVisitorToAllChildren(children);
        }

        [Fact]
        public void DoesNotVisitUpwardVisitor()
        {
            sut.Accept(upwardVisitor);
            
            upwardVisitor.DidNotReceive().Visit(AName, AParent, AnElementsEnumerable);   
        }

        private void AssertPassesDownwardVisitorToAllChildren(IEnumerable<IElement> children)
        {
            foreach (var child in children)
            {
                child.Received(1).Accept(downwardVisitor);
            }
        }

        private static string AName => Arg.Any<string>();

        private static IEnumerable<IElement> AnElementsEnumerable => Arg.Any<IEnumerable<IElement>>();

        private static IElement AParent => Arg.Any<IElement>();
    }
}
