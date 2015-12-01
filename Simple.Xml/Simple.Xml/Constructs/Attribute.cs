using System;
using System.Xml.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class Attribute
    {
        public readonly ElementName Name;
        public readonly string Value;

        public Attribute(ElementName elementName, string value)
        {
            if (elementName == null)
            {
                throw new ArgumentNullException(nameof(elementName));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Name = elementName;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}=\"{Value}\"";
        }

        public XAttribute ToXAttribute()
        {
            return new XAttribute(Name.ToXName(), Value);
        }
    }
}