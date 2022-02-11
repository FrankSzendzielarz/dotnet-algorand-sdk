using Algorand.V2.Algod.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Algorand.Token
{
    /// <summary>
    /// Arc3 token metadata 
    /// https://github.com/algorandfoundation/ARCs/blob/main/ARCs/arc-0003.md
    /// </summary>
    public class TokenMetadata
    {
        #region Arc3 Properties

        /// <summary>
        /// Identifies the asset to which this token represents
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// The number of decimal places that the token amount should display - 
        /// e.g. 18, means to divide the token amount by 1000000000000000000 to get its user representation.
        /// </summary>
        
        public int? Decimals { get; set; }

        /// <summary>
        /// Describes the asset to which this token represents
        /// </summary>
        
        public string Description { get; set; }

        /// <summary>
        /// A URI pointing to a file with MIME type image/* representing the asset to which this token represents.
        /// Consider making any images at a width between 320 and 1080 pixels and aspect ratio between 1.91:1 and 4:5 inclusive.
        /// </summary>
        
        public string Image { get; set; }

        /// <summary>
        /// The SHA-256 digest of the file pointed by the URI image. 
        /// The field value is a single SHA-256 integrity metadata as defined in the W3C subresource integrity specification
        /// (https://w3c.github.io/webappsec-subresource-integrity).
        /// </summary>
        
        public string ImageIntegrity { get; set; }

        /// <summary>
        /// The MIME type of the file pointed by the URI image. MUST be of the form 'image/*'.
        /// </summary>
        
        public string ImageMimetype { get; set; }

        /// <summary>
        /// Background color do display the asset. MUST be a six-character hexadecimal without a pre-pended #.
        /// </summary>
        
        [JsonConverter(typeof(ColorHexConverter))]  
        public System.Drawing.Color BackgroundColor { get; set; }

        /// <summary>
        /// A URI pointing to an external website presenting the asset.
        /// </summary>
        
        public Uri ExternalUrl { get; set; }

        /// <summary>
        /// The SHA-256 digest of the file pointed by the URI external_url. 
        /// The field value is a single SHA-256 integrity metadata as defined in the W3C subresource integrity specification 
        /// (https://w3c.github.io/webappsec-subresource-integrity).
        /// </summary>
        
        public string ExternalUrlIntegrity { get; set; }

        /// <summary>
        /// The MIME type of the file pointed by the URI external_url. It is expected to be 'text/html' in almost all cases.
        /// </summary>
        
        public string ExternalUrlMimetype { get; set; }

        /// <summary>
        /// A URI pointing to a multi-media file representing the asset.
        /// </summary>
        [JsonProperty(PropertyName = "animation_url")]
        public Uri AnimationUrl { get; set; }

        /// <summary>
        /// The SHA-256 digest of the file pointed by the URI external_url.
        /// The field value is a single SHA-256 integrity metadata as defined in the W3C subresource integrity specification
        /// (https://w3c.github.io/webappsec-subresource-integrity).
        /// </summary>
        
        public string AnimationUrlIntegrity { get; set; }

        /// <summary>
        /// The MIME type of the file pointed by the URI animation_url. 
        /// If the MIME type is not specified, clients MAY guess the MIME type from the file extension or MAY decide not to display the asset at all.
        /// It is STRONGLY RECOMMENDED to include the MIME type.
        /// </summary>
        
        public string AnimationUrlMimetype { get; set; }

        /// <summary>
        /// Arbitrary properties (also called attributes). Values may be strings, numbers, object or arrays.
        /// </summary>
        
        public object Properties { get; set; }

        /// <summary>
        /// Extra metadata in base64. If the field is specified (even if it is an empty string) the asset metadata (am) of the ASA is computed 
        /// differently than if it is not specified.
        /// </summary>
        
        public string ExtraMetadata { get; set; } = default(string);

        #endregion

        #region Utility Methods


        

        public virtual bool IsValid()
        {
            try
            {
                byte[] testBytes = Convert.FromBase64String(ExtraMetadata);
            }
            catch
            {
                return false;
            }

            return true;
        }

       

        public static TokenMetadata FromJson(string json)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }, Formatting = Formatting.Indented };
            
            return JsonConvert.DeserializeObject<TokenMetadata>(json,settings);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() { NamingStrategy=new SnakeCaseNamingStrategy()}, Formatting = Formatting.Indented });
        }

      

        #endregion



    }

   


}
