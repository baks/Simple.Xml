using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Xunit;

namespace Simple.Xml.UnitTests
{
    public class ForwardXmlStringProducerTests
    {
        private readonly ForwardXmlStringProducer sut = new ForwardXmlStringProducer();

        [Theory, AutoSubstituteData]
        public void AddsTagWithVisitedNameToString(string name)
        {
            sut.Visit(name, Enumerable.Empty<IElement>());

            Assert.Equal($"<{name}></{name}>", sut.ToString());
        }

        [Theory, AutoSubstituteData]
        public void VisitsPassedChildren(string aName, IElement child)
        {
            var theChildren = new[] {child, child, child};
            sut.Visit(aName, theChildren);

            child.Received(3).Accept(sut);
        }
    }
}
