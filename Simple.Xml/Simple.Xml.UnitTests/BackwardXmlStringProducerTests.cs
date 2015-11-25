using System.Collections.Generic;
using NSubstitute;
using Ploeh.AutoFixture;
using Simple.Xml.Structure.Output;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class BackwardXmlStringProducerTests
    {
        private static readonly string anyName = new Fixture().Create<string>();
        private static readonly IElement anyParent = Substitute.For<IElement>();
        private static readonly IEnumerable<IElement> someChildren = Substitute.For<IEnumerable<IElement>>(); 
        private readonly BackwardXmlStringProducer sut = new BackwardXmlStringProducer();


        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedName(string name)
        {
            sut.Visit(name, anyParent, someChildren);

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void PassesItselfToParent(IElement parent)
        {
            sut.Visit(anyName, parent, someChildren);

            parent.Received(1).Accept(sut);
        }
    }
}
