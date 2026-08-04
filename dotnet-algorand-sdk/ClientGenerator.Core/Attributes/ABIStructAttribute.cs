using System;


namespace AVM.ClientGenerator.Core.Attributes
{
    [AttributeUsage( AttributeTargets.Struct, AllowMultiple = false)]
    public class ABIStructAttribute : Attribute
    {
        public ABIStructAttribute()
        {
           
        }
    }
}
