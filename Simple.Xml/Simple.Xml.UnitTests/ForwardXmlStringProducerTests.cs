using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;
using NSubstitute.Exceptions;
using NSubstitute.Routing;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ForwardXmlStringProducerTests
    {
        private static readonly IEnumerable<IElement> EMPTY_CHILDREN = Enumerable.Empty<IElement>();
        private static readonly IEnumerable<Attribute> EMPTY_ATTRIBUTES = Enumerable.Empty<Attribute>();

        private readonly ForwardXmlStringProducer sut;
        private readonly IXmlBuilder xmlBuilder;

        public ForwardXmlStringProducerTests()
        {
            xmlBuilder = Substitute.For<IXmlBuilder>();
            sut = new ForwardXmlStringProducer(xmlBuilder);
        }

        [Theory, AutoSubstituteData]
        public void WritesStartTagAndEndTag(string name)
        {
            xmlBuilder.When(x => x.WriteStartTagFor(name)).Then("TagStarted");
            xmlBuilder.When(x => x.WriteEndTag()).Expect("TagStarted");

            sut.Visit(name, EMPTY_CHILDREN, EMPTY_ATTRIBUTES);

            xmlBuilder.Received(1).WriteStartTagFor(name);
            xmlBuilder.Received(1).WriteEndTag();
        }

        [Theory, AutoSubstituteData]
        public void WritesContent(string content)
        {
            sut.Visit(content);

            xmlBuilder.Received(1).WriteContent(content);
        }

        /*[Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.Visit(name, EMPTY_CHILDREN, EMPTY_ATTRIBUTES);

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }*/

        [Theory, AutoSubstituteData]
        public void VisitsPassedChildren(string aName, IElement child)
        {
            var theChildren = new[] {child, child, child};
            sut.Visit(aName, theChildren, EMPTY_ATTRIBUTES);

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

            Assert.Throws<ArgumentNullException>(() => sut.Visit(emptyName, EMPTY_CHILDREN, EMPTY_ATTRIBUTES));
        }
    }

    public static class WhenCalledExtensions
    {
        private static string savedState = "undefined";

        public static void Then<T>(this WhenCalled<T> whenCalled, string newState)
        {
            whenCalled.Do(info => savedState = newState);
        }

        public static void Expect<T>(this WhenCalled<T> whenCalled, string expectedState)
        {
            whenCalled.Do(info =>
            {
                if (!string.Equals(savedState, expectedState, StringComparison.Ordinal))
                {
                    throw new Exception(string.Format(" Expected {0}, but was {1}", expectedState, savedState));
                }
            });
        }
    }
}
