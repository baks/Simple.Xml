using System;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Structure;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicElementTests
    {
        private readonly IElement element;
        private readonly dynamic sut;
        private readonly IDynamicElementVisitor visitor;
        private readonly IElementFactory factory;

        public DynamicElementTests()
        {
            visitor = Substitute.For<IDynamicElementVisitor>();
            element = Substitute.For<IElement>();
            factory = Substitute.For<IElementFactory>();
            sut = new DynamicElement(element, factory);
        }

        [Theory, AutoSubstituteData]
        public void GuardsCollaborators(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(DynamicElement).GetConstructors());
        }

        [Fact]
        public void MemberInvocationAddsNewChildToElementWithMemberName()
        {
            var p = sut.Head;

            factory.Received(1).CreateElementWithNameForParent("Head", element);
        }

        [Theory, AutoSubstituteData]
        public void MemberAssignmentAddsContentToElementWithMemberName(string aContent)
        {
            sut.Head = aContent;

            factory.Received(1).CreateElementWithNameForParent("Head", element);
            factory.Received(1).CreateElementWithContentForParent(aContent, Arg.Any<IElement>());
        }

        [Fact]
        public void AcceptsDynamicElementVisitor()
        {
            sut.Accept(visitor);

            visitor.Received(1).Visit(element);
        }

        [Fact]
        public void PassesElementToVisitor()
        {
            sut.Accept(visitor);

            visitor.Received().Visit(element);
        }

        private IElement AContentElementWith(string aContent)
        {
            return Arg.Is<IElement>(el => el.Equals(new ContentElement(aContent)));
        }
    }
}
