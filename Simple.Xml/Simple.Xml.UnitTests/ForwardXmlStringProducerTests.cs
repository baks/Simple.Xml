using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class ForwardXmlStringProducerTests
    {
        private static readonly IEnumerable<IElement> someChildren = Substitute.For<IEnumerable<IElement>>();
        private readonly ForwardXmlStringProducer sut = new ForwardXmlStringProducer();

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.Visit(name, someChildren);

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void VisitsPassedChildren(string aName, IElement child)
        {
            var theChildren = new[] {child, child, child};
            sut.Visit(aName, theChildren);

            child.Received(3).Accept(sut);
        }

        [Theory, AutoSubstituteData]
        public void GuardsVisitPeers(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(ForwardXmlStringProducer).GetMethod("Visit"));
        }

        [Fact]
        public void GuardAgainstEmptyName()
        {
            var emptyName = string.Empty;

            Assert.Throws<ArgumentNullException>(() => sut.Visit(emptyName, someChildren));
        }
    }
}
