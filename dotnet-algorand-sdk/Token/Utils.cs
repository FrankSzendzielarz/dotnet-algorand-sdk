using Algorand.V2.Algod.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorand.Token
{
    public static class Utils
    {

        private static AssetParams GenerateTokenParameters(TokenMetadata tokenMetadata, Uri metadataURI, Account creator, string unitName, string assetName, ulong total, ulong decimals)
        {
            if (creator == null) throw new ArgumentNullException();
            if (tokenMetadata == null) throw new ArgumentNullException();
            if (tokenMetadata.IsValid()) throw new ArgumentException("Token metadata invalid.");
            if (metadataURI.Scheme.ToLower() == "http") throw new ArgumentException("Http is not permitted.");
            if (!metadataURI.IsAbsoluteUri) throw new ArgumentException("Relative metadataURI not permitted.");
            try
            {
                byte[] testBytes = Convert.FromBase64String(tokenMetadata.ExtraMetadata);
            }
            catch
            {
                throw new ArgumentException("extra_metadata must be a base64 string");
            }

            byte[] metadataHash;
            if (tokenMetadata.ExtraMetadata != null)
            {
                var prefix = Encoding.UTF8.GetBytes($"arc0003/am");
                var interimHash = Digester.Digest(Encoding.UTF8.GetBytes($"arc0003/amj{tokenMetadata}"));
                var extraMeta = Encoding.UTF8.GetBytes(tokenMetadata.ExtraMetadata);

                var concatenated = prefix.Concat(interimHash).Concat(extraMeta).ToArray();
                metadataHash = Digester.Digest(concatenated);
            }
            else
            {
                metadataHash = Digester.Digest(Encoding.UTF8.GetBytes(tokenMetadata.ToString()));
            }


            return new AssetParams()
            {
                Creator = creator.Address.ToString(),
                Name = assetName ?? tokenMetadata.Name,
                UnitName = unitName,
                Total = total,
                Decimals = decimals,
                Url = metadataURI.ToString(),
                MetadataHash = metadataHash
            };
        }

        public static AssetParams GeneratePureNonFungibleTokenParameters(TokenMetadata tokenMetadata, Uri metadataURI, Account creator, string unitName = null, string assetName= null )
        {
            ulong total = 1;
            ulong decimals = 0;

            return GenerateTokenParameters(tokenMetadata, metadataURI, creator, unitName, assetName, total, decimals);

        }

    
        
        /// <param name="fraction">Multiplied by 10 to get number of fractions</param>
        /// <param name="tokenMetadata">The Arc3 token metadata </param>
        /// <param name="metadataURI">URI where the metadata is stored</param>
        /// <param name="creator">Account to create the asset</param>
        /// <param name="unitName">Unit name</param>
        /// <param name="assetName">Asset name</param>
        /// <returns></returns>
        public static AssetParams GenerateFractionalNonFungibleTokenParameters(byte fractionMagnitude, TokenMetadata tokenMetadata, Uri metadataURI, Account creator, string unitName = null, string assetName = null)
        {
            if (fractionMagnitude == 0) throw new ArgumentException();
            ulong total = (ulong)Math.Pow(10, fractionMagnitude);
            ulong decimals = fractionMagnitude;

            return GenerateTokenParameters(tokenMetadata, metadataURI, creator, unitName, assetName, total, decimals);

        }
    }
}
