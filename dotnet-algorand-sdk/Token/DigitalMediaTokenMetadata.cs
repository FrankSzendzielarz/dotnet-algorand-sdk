using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorand.Token
{
    public class DigitalMediaTokenMetadata
    {
        /// <summary>
        /// (Required) Describes the standard used.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Standard { get; set; } = "arc69";

        /// <summary>
        /// Describes the asset to which this token represents.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A URI pointing to an external website. Borrowed from Open Sea's metadata format (https://docs.opensea.io/docs/metadata-standards).
        /// </summary>
        public Uri External_url { get; set; }

        /// <summary>
        /// A URI pointing to a high resolution version of the asset's media.
        /// </summary>
        public Uri Media_url { get; set; }

        /// <summary>
        /// Properties following the EIP-1155 'simple properties' format. (https://github.com/ethereum/EIPs/blob/master/EIPS/eip-1155.md#erc-1155-metadata-uri-json-schema)
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }


        /// <summary>
        /// Describes the MIME type of the ASA's URL (`au` field).
        /// </summary>
        public string Mime_type { get; set; }


    }
}
