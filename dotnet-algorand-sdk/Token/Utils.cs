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

            byte[] metadataHash;
            if (tokenMetadata.Extra_metadata != null)
            {
                var prefix = Encoding.UTF8.GetBytes($"arc0003/am");
                var interimHash = Digester.Digest(Encoding.UTF8.GetBytes($"arc0003/amj{tokenMetadata}");
                var extraMeta = Encoding.UTF8.GetBytes(tokenMetadata.Extra_metadata);

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

    

        public static AssetParams GenerateFractionalNonFungibleTokenParameters(TokenMetadata tokenMetadata, Uri metadataURI, Account creator, string unitName = null, string assetName = null)
        {
            if (creator == null) throw new ArgumentNullException();
            if (tokenMetadata == null) throw new ArgumentNullException();
            if (tokenMetadata.IsValid()) throw new ArgumentException("Token metadata invalid.");
            if (metadataURI.Scheme.ToLower() == "http") throw new ArgumentException("Http is not permitted.");
            if (!metadataURI.IsAbsoluteUri) throw new ArgumentException("Relative metadataURI not permitted.");

            return new AssetParams()
            {
                Creator = creator.Address.ToString(),
                Name = assetName ?? tokenMetadata.Name,
                UnitName = unitName,
                Total = 1,
                Decimals = 0,
                Url = metadataURI.ToString(),
                MetadataHash = Encoding.ASCII.GetBytes("16efaa3924a6fd9d3a4880099a4ac65d")

            };

        }
    }
}
