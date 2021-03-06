﻿using System;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicElementTests
    {
        private readonly IElementContainer element;
        private readonly dynamic sut;
        private readonly IDynamicElementVisitor visitor;
        private readonly IElementFactory factory;
        private readonly Func<BaseDynamicElement, BaseDynamicElement> graphDecorator; 

        public DynamicElementTests()
        {
            visitor = Substitute.For<IDynamicElementVisitor>();
            element = Substitute.For<IElementContainer>();
            factory = Substitute.For<IElementFactory>();
            graphDecorator = Substitute.For<Func<BaseDynamicElement, BaseDynamicElement>>();
            sut = new DynamicElement(element, factory, graphDecorator);
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
