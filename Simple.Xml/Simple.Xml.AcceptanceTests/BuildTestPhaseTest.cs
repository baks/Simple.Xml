using Xunit;

namespace Simple.Xml.AcceptanceTests
{
    public class BuildTestPhaseTest
    {
        [Fact]
        public void ShouldTestFailedShowUpOnAppveyor()
        {
            Assert.False(true);
        }
    }
}
