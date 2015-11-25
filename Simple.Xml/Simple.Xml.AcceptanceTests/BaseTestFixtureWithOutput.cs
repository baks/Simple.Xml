using System;
using Simple.Xml.Dynamic;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public abstract class BaseTestFixtureWithOutput : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;
        protected readonly IOutput output;

        protected DynamicXmlBuilder sut;

        protected BaseTestFixtureWithOutput(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            this.output = new StringBuilderOutput();
            this.sut = new DynamicXmlBuilder(element => new VerboseElement(element, output));
        }

        public void Dispose()
        {
            testOutputHelper.WriteLine(output.ToString());
        }
    }
}