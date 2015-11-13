using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Simple.Xml.Structure.UnitTests
{
    public class ContentElementTests
    {
        private static readonly string aContent = "aContent";

        private readonly ContentElement sut = new ContentElement(aContent);
        private readonly IUpwardElementVisitor upwardVisitor = Substitute.For<IUpwardElementVisitor>();
        private readonly IDownwardElementVisitor downwardVisitor = Substitute.For<IDownwardElementVisitor>();

        [Fact]
        public void VisitsContentInUpwardVisitor()
        {
            sut.Accept(upwardVisitor);

            upwardVisitor.Received(1).Visit(aContent);
        }

        [Fact]
        public void VisitsContentInDownwardVisitor()
        {
            sut.Accept(downwardVisitor);

            downwardVisitor.Received(1).Visit(aContent);
        }
    }
}
