using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ForwardXmlStringProducerTests
    {
        private static readonly IEnumerable<IElement> someChildren = Substitute.For<IEnumerable<IElement>>();
        private static readonly IEnumerable<Attribute> someAttributes = Substitute.For<IEnumerable<Attribute>>();
        private readonly ForwardXmlStringProducer sut = new ForwardXmlStringProducer();

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.Visit(name, someChildren, someAttributes);

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void VisitsPassedChildren(string aName, IElement child)
        {
            var theChildren = new[] {child, child, child};
            sut.Visit(aName, theChildren, someAttributes);

            child.Received(3).Accept(sut);
        }

        [Theory, AutoSubstituteData]
        public void GuardsVisitPeers(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(ForwardXmlStringProducer).GetMethods().Where(mi => string.Equals("Visit", mi.Name)));
        }

        [Fact]
        public void GuardAgainstEmptyName()
        {
            var emptyName = string.Empty;

            Assert.Throws<ArgumentNullException>(() => sut.Visit(emptyName, someChildren, someAttributes));
        }
    }
}
