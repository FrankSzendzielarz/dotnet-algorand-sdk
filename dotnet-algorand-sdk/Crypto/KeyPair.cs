using Algorand.Utils;
using Algorand.Utils.Crypto;
using NSec.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorand.Crypto
{
    public class KeyPair
    {
        internal const int SK_SIZE = 32;
        internal const int SK_SIZE_BITS = SK_SIZE * 8;
        private const int PK_SIZE = 32;
        public byte[] ClearTextPrivateKey { get; private set; }
        public byte[] ClearTextPublicKey { get; private set; }
      
        public Key Pair { get; private set; }


        public KeyPair(byte[] privateKey)
        {
            if (privateKey.Length != SK_SIZE)
                throw new ArgumentException("Incorrect private key length");


            Pair = Key.Import(SignatureAlgorithm.Ed25519,privateKey,KeyBlobFormat.RawPrivateKey);
            ClearTextPrivateKey = privateKey;
            ClearTextPublicKey = Pair.PublicKey.Export(KeyBlobFormat.PkixPublicKey); // X.509 ASN.1 prefix 

        }


        public KeyPair(SecureRandom random)
        {
            
            var algorithm = SignatureAlgorithm.Ed25519;

            // create a new key pair
            Pair = random.GenerateKey(algorithm);

            // get the private and public keys
            ClearTextPrivateKey= Pair.Export(KeyBlobFormat.RawPrivateKey);
            ClearTextPublicKey = Pair.PublicKey.Export(KeyBlobFormat.PkixPublicKey); // X.509 ASN.1 prefix 



        }





        public PublicKey PublicKey { 
            get
            {

                return Pair.PublicKey;
            } 
        }

        

        public string ToMnemonic()
        {
            
            return Mnemonic.FromKey(ClearTextPrivateKey);
        }
    }
    
}
