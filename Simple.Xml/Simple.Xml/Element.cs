using System.Dynamic;

namespace Simple.Xml
{
    public class Element : DynamicObject
    {
        public string ToXml()
        {
            return "<root></root>";
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new Element();
            return true;
        }
    }
}
