
using Newtonsoft.Json;
using System;

namespace AVM.ClientGenerator.ABI.ARC4
{
    public class ArgumentDescription
    {
        [JsonRequired]
        public string Type { get; set; }
        public string TypeDetail { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }


        public string Summary {
            get { if (String.IsNullOrWhiteSpace(TypeDetail)) return $"ABI Type is {Type}"; else return $"Type is {TypeDetail}"; } 
        }

        internal bool IsAccountRef()
        {
            // The ARC4 foreign-reference type is literally "account" (a 1-byte index into Txn.Accounts[]),
            // distinct from the plain "address" value type (32 raw bytes) - conflating the two here misencodes
            // any method that takes a plain address value (e.g. BiatecIdentityProvider.bootstrap's
            // governor/verificationSetter/engagementSetter address args), since it would silently drop the
            // 32-byte value from the ABI args and instead register it as a foreign-account reference that
            // nothing in the method signature actually asked for.
            return Type == "account";
        }

        internal bool IsApplicationRef()
        {
            return Type == "application";
        }

        internal bool IsAssetRef()
        {
            return Type == "asset";
        }

        internal bool IsTransaction()
        {
            string tx;
            if (string.IsNullOrWhiteSpace(TypeDetail)) tx = Type; else tx = TypeDetail;

            return !string.IsNullOrWhiteSpace(TypeHelpers.TransactionReferenceToInnerTransaction(tx));
        }

      
    }
}
