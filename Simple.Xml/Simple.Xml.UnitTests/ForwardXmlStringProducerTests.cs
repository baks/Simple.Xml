﻿using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ForwardXmlStringProducerTests
    {
        private static readonly IEnumerable<IElement> NO_CHILDREN = Enumerable.Empty<IElement>();
        private static readonly Tag ANY_TAG = null;

        private readonly ForwardXmlStringProducer sut;
        private readonly IXmlBuilder xmlBuilder;

        public ForwardXmlStringProducerTests()
        {
            xmlBuilder = Substitute.For<IXmlBuilder>();
            sut = new ForwardXmlStringProducer(xmlBuilder);
        }

        [Theory, AutoSubstituteData]
        public void WritesStartTagAndEndTag(Tag tag)
        {
            xmlBuilder.WhenForAnyArgs(x => x.WriteStartTagFor(ANY_TAG))
                .Then("TagStarted");
            xmlBuilder.When(x => x.WriteEndTag()).Expect("TagStarted");

            sut.Visit(tag, NO_CHILDREN);

            xmlBuilder.Received(1).WriteStartTagFor(tag);
            xmlBuilder.Received(1).WriteEndTag();
        }

        [Theory, AutoSubstituteData]
        public void WritesAttributes(Tag tag)
        {
            xmlBuilder.WhenForAnyArgs(x => x.WriteStartTagFor(ANY_TAG))
                .Then("TagStarted");
            xmlBuilder.When(x => x.WriteEndTag()).Expect("TagStarted");

            sut.Visit(tag, NO_CHILDREN);

            xmlBuilder.Received(1).WriteStartTagFor(tag);
            xmlBuilder.Received(1).WriteEndTag();
        }

        [Theory, AutoSubstituteData]
        public void WritesContent(string content)
        {
            sut.Visit(content);

            xmlBuilder.Received(1).WriteContent(Arg.Any<string>());
        }

        [Theory, AutoSubstituteData]
        public void InstructsBuilderToUseNamespaces(Namespaces namespaces)
        {
            sut.Visit(namespaces);

            xmlBuilder.Received(1).UseNamespaces(namespaces);
        }

        [Theory, AutoSubstituteData]
        public void VisitsPassedChildren(Tag aTag, IElement child)
        {
            var theChildren = new[] {child, child, child};
            sut.Visit(aTag, theChildren);

            child.Received(3).Accept(sut);
        }

        [Theory, AutoSubstituteData]
        public void GuardsVisitPeers(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(ForwardXmlStringProducer).GetMethods().Where(mi => string.Equals("Visit", mi.Name)));
        }
    }
}
