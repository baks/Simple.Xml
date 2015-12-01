using System.Collections.Generic;
using NSubstitute;
using Ploeh.AutoFixture;
using Simple.Xml.Structure.Constructs;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class BackwardXmlStringProducerTests
    {
        private static readonly Tag ANY_TAG = new Fixture().Create<Tag>();
        private static readonly IElement ANY_PARENT = Substitute.For<IElement>();
        private static readonly IEnumerable<IElement> someChildren = Substitute.For<IEnumerable<IElement>>();

        private readonly IXmlBuilder xmlBuilder;
        private readonly BackwardXmlStringProducer sut;

        public BackwardXmlStringProducerTests()
        {
            xmlBuilder = Substitute.For<IXmlBuilder>();
            sut = new BackwardXmlStringProducer(xmlBuilder);
        }


        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedName(Tag tag)
        {
            xmlBuilder.WhenForAnyArgs(x => x.WriteStartTagFor(ANY_TAG))
                .Then("TagStarted");
            xmlBuilder.When(x => x.WriteEndTag()).Expect("TagStarted");

            sut.Visit(tag, ANY_PARENT, someChildren);

            xmlBuilder.Received(1).WriteStartTagFor(tag);
            xmlBuilder.Received(1).WriteEndTag();
        }

        [Theory, AutoSubstituteData]
        public void PassesItselfToParent(IElement parent)
        {
            sut.Visit(ANY_TAG, parent, someChildren);

            parent.Received(1).Accept(sut);
        }
    }
}
