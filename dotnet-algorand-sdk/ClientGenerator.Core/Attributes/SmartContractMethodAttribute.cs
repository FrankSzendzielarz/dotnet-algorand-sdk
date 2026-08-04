using System;

namespace AVM.ClientGenerator.Core.Attributes
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class SmartContractMethodAttribute : System.Attribute
    {
        internal OnCompleteType callType;
        internal string identifier;

        public SmartContractMethodAttribute(OnCompleteType callType, string identifier="")
        {
            this.identifier = identifier;
            this.callType = callType;
        }
    }
}
