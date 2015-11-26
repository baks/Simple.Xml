using System;
using System.Xml.Linq;

namespace Simple.Xml.Structure.Constructs
{
    public class Attribute
    {
        public readonly string Name;
        public readonly string Value;

        public Attribute(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}=\"{Value}\"";
        }

        public XAttribute ToXAttribute()
        {
            return new XAttribute(XName.Get(Name), Value);
        }
    }
}