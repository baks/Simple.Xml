using System.Dynamic;
using NSubstitute;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Simple.Xml.Dynamic.UnitTests
{
    public class DynamicElementDecoratorTests
    {
        private readonly BaseDynamicElement decoratee;
        private readonly DynamicElementDecorator sut;

        public DynamicElementDecoratorTests()
        {
            decoratee = Substitute.For<BaseDynamicElement>();
            sut = new DynamicElementDecorator(decoratee);
        }

        [Theory, AutoSubstituteData]
        public void GuardsDecoratee(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(DynamicElementDecorator).GetConstructors());
        }

        [Theory, AutoSubstituteData]
        public void DelegatesAcceptToDecoratee(IDynamicElementVisitor aVisitor)
        {
            sut.Accept(aVisitor);

            decoratee.Received(1).Accept(aVisitor);
        }

        [Fact]
        public void DelegatesTryGetMemberToDecoratee()
        {
            object result;
            sut.TryGetMember(null, out result);

            decoratee.Received(1).TryGetMember(Arg.Any<GetMemberBinder>(), out result);
        }

        [Theory, AutoSubstituteData]
        public void DelegatesTrySetMemberToDecoratee(object anObject)
        {
            sut.TrySetMember(null, anObject);

            decoratee.Received(1).TrySetMember(Arg.Any<SetMemberBinder>(), Arg.Any<object>());
        }
    }
}
