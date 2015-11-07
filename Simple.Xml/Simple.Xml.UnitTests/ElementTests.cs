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

        private readonly Element sut = new Element(ANY_NAME, UNUSED_PARENT);

        [Theory, AutoSubstituteData]
        public void GuardCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (Element).GetConstructors());
        }

        [Fact]
        public void VisitsVisitor()
        {
            var visitor = Substitute.For<IUpwardElementVisitor>();
            sut.Accept(visitor);

            visitor.Received(1).Visit(Arg.Any<string>(), Arg.Any<IElement>(), Arg.Any<IEnumerable<IElement>>());
        }
    }
}
