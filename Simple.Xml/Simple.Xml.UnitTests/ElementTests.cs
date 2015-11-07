using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class ElementTests
    {
        private static readonly string ANY_NAME = "any";
        private static readonly IElement UNUSED_PARENT = new NullObjectElement();

        private readonly Element sut = new Element(ANY_NAME, UNUSED_PARENT);

        [Fact]
        public void VisitsVisitor()
        {
            var visitor = Substitute.For<IElementVisitor>();
            sut.Accept(visitor);

            visitor.Received(1).Visit(Arg.Any<string>(), Arg.Any<IElement>(), Arg.Any<IEnumerable<IElement>>());
        }
    }
}
