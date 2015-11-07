using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;

namespace Simple.Xml
{
    public class VerboseElement : BaseElement
    {
        private readonly BaseElement baseElement;
        private readonly IOutput output;

        public VerboseElement(BaseElement baseElement, IOutput output)
        {
            if (baseElement == null)
            {
                throw new ArgumentNullException(nameof(baseElement));
            }
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            this.baseElement = baseElement;
            this.output = output;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            output.Write("GetDynamicMemberNames");
            return base.GetDynamicMemberNames();
        }

        public override DynamicMetaObject GetMetaObject(Expression parameter)
        {
            output.Write(string.Format("GetMetaObject for {0}", parameter.Type.Name));
            return base.GetMetaObject(parameter);
        }

        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            output.Write("TryBinaryOperation");
            return base.TryBinaryOperation(binder, arg, out result);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            output.Write("TryConvert");
            return base.TryConvert(binder, out result);
        }

        public override bool TryCreateInstance(CreateInstanceBinder binder, object[] args, out object result)
        {
            output.Write("TryCreateInstance");
            return base.TryCreateInstance(binder, args, out result);
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            output.Write("TryGetIndex");
            return base.TryGetIndex(binder, indexes, out result);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            output.Write("TrySetIndex");
            return base.TrySetIndex(binder, indexes, value);
        }

        public override bool TryUnaryOperation(UnaryOperationBinder binder, out object result)
        {
            output.Write("TryUnaryOperation");
            return base.TryUnaryOperation(binder, out result);
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            output.Write(string.Format("Try invoke with {0} arguments {1} and return type is {2}",
                binder.CallInfo.ArgumentCount, string.Join(", ", binder.CallInfo.ArgumentNames), binder.ReturnType.FullName));
            var res =  base.TryInvoke(binder, args, out result);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            output.Write(string.Format("TryInvokeMember for {0}", binder.Name));
            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            output.Write(string.Format("TrySetMember for {0}", binder.Name));
            return base.TrySetMember(binder, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            output.Write(string.Format("TryGetMember for {0}", binder.Name));
            return baseElement.TryGetMember(binder, out result);
        }

        public override void Accept(IElementVisitor visitor)
        {
            baseElement.Accept(visitor);
        }

        public override string ToXml()
        {
            return baseElement.ToXml();
        }
    }
}