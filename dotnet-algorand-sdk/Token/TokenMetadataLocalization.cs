using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorand.Token
{
    internal class TokenMetadataLocalization
    {
        /// <summary>
        /// The URI pattern to fetch localized data from. This URI should contain the substring `{locale}` which will be replaced with the appropriate locale value before sending the request.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Uri { get; set; }

        /// <summary>
        /// The locale of the default data within the base JSON
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Default { get; set; }

        /// <summary>
        /// The list of locales for which data is available. 
        /// These locales should conform to those defined in the Unicode Common Locale Data Repository (http://cldr.unicode.org/).
        /// </summary>
        
        //TODO Look into verifying locale names
        [JsonProperty(Required = Required.Always)]
        public string[] Locales { get; set; }

        /// <summary>
        /// The SHA-256 digests of the localized JSON files (except the default one). The field name is the locale. 
        /// The field value is a single SHA-256 integrity metadata as defined in the W3C subresource integrity specification (https://w3c.github.io/webappsec-subresource-integrity).
        /// </summary>
        public Dictionary<string,string> Integrity { get; set; }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
