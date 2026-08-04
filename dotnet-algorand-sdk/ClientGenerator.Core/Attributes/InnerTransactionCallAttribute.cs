using System;

namespace AVM.ClientGenerator.Core.Attributes
{
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class InnerTransactionCallAttribute : System.Attribute
    {
      

        public InnerTransactionCallAttribute()
        {
       
        }
    }
}
