using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Simple.Xml.Structure.Constructs;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class RootElementTests
    {
        private static readonly IEnumerable<IElement> ANY_CHILDREN = null; 
        private static readonly IElement ANY_PARENT = new NullObjectElement();

        private readonly IElementCollector collector;
        private readonly RootElement sut;
        private readonly IDownwardElementVisitor downwardVisitor;
        private readonly IUpwardElementVisitor upwardVisitor;
        private readonly Namespaces aNamespaces;

        public RootElementTests()
        {
            aNamespaces = new Namespaces();
            collector = Substitute.For<IElementCollector>();
            sut = new RootElement(aNamespaces, collector);
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
            sut.Accept(downwardVisitor);
            collector.Received(1).ChildrenFor(sut);
        }

        [Fact]
        public void DoesNotVisitUpwardVisitor()
        {
            sut.Accept(upwardVisitor);
            
            upwardVisitor.DidNotReceive().Visit(Arg.Any<Tag>(), ANY_PARENT, ANY_CHILDREN);   
        }

        [Fact]
        public void VisitsNamespaces()
        {
            sut.Accept(downwardVisitor);

            downwardVisitor.Received(1).Visit(aNamespaces);
        }
    }
}
