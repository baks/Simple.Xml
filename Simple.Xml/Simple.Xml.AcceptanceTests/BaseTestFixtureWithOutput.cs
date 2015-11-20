using System;
using Simple.Xml.Dynamic;
using Xunit.Abstractions;

namespace Simple.Xml.AcceptanceTests
{
    public abstract class BaseTestFixtureWithOutput : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly IOutput output = new StringBuilderOutput();

        protected BaseTestFixtureWithOutput(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            XmlBuilder.DecorateElement = element => new VerboseElement(element, output);
        }

        public void Dispose()
        {
            testOutputHelper.WriteLine(output.ToString());
        }
    }
}