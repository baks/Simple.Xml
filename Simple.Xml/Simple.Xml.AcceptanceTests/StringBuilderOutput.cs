using System.Text;

namespace Simple.Xml.AcceptanceTests
{
    public class StringBuilderOutput : IOutput
    {
        private readonly StringBuilder builder = new StringBuilder();

        public void Write(string msg)
        {
            builder.AppendLine(msg);
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}