/* 
 * for KMD HTTP API
 *
 * API for KMD (Key Management Daemon)
 *
 * OpenAPI spec version: 0.0.1
 * Contact: contact@algorand.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Algorand.Client.SwaggerDateConverter;

namespace Algorand.Kmd.Model
{
    /// <summary>
    /// APIV1Wallet is the API&#x27;s representation of a wallet
    /// </summary>
    [DataContract]
        public partial class APIV1Wallet :  IEquatable<APIV1Wallet>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIV1Wallet" /> class.
        /// </summary>
        /// <param name="driverName">driverName.</param>
        /// <param name="driverVersion">driverVersion.</param>
        /// <param name="id">id.</param>
        /// <param name="mnemonicUx">mnemonicUx.</param>
        /// <param name="name">name.</param>
        /// <param name="supportedTxs">supportedTxs.</param>
        public APIV1Wallet(string driverName = default(string), uint? driverVersion = default(uint?), string id = default(string), bool? mnemonicUx = default(bool?), string name = default(string), List<string> supportedTxs = default(List<string>))
        {
            this.DriverName = driverName;
            this.DriverVersion = driverVersion;
            this.Id = id;
            this.MnemonicUx = mnemonicUx;
            this.Name = name;
            this.SupportedTxs = supportedTxs;
        }
        
        /// <summary>
        /// Gets or Sets DriverName
        /// </summary>
        [DataMember(Name="driver_name", EmitDefaultValue=false)]
        public string DriverName { get; set; }

        /// <summary>
        /// Gets or Sets DriverVersion
        /// </summary>
        [DataMember(Name="driver_version", EmitDefaultValue=false)]
        public uint? DriverVersion { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets MnemonicUx
        /// </summary>
        [DataMember(Name="mnemonic_ux", EmitDefaultValue=false)]
        public bool? MnemonicUx { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets SupportedTxs
        /// </summary>
        [DataMember(Name="supported_txs", EmitDefaultValue=false)]
        public List<string> SupportedTxs { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class APIV1Wallet {\n");
            sb.Append("  DriverName: ").Append(DriverName).Append("\n");
            sb.Append("  DriverVersion: ").Append(DriverVersion).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  MnemonicUx: ").Append(MnemonicUx).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  SupportedTxs: ").Append(SupportedTxs).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as APIV1Wallet);
        }

        /// <summary>
        /// Returns true if APIV1Wallet instances are equal
        /// </summary>
        /// <param name="input">Instance of APIV1Wallet to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(APIV1Wallet input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DriverName == input.DriverName ||
                    (this.DriverName != null &&
                    this.DriverName.Equals(input.DriverName))
                ) && 
                (
                    this.DriverVersion == input.DriverVersion ||
                    (this.DriverVersion != null &&
                    this.DriverVersion.Equals(input.DriverVersion))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.MnemonicUx == input.MnemonicUx ||
                    (this.MnemonicUx != null &&
                    this.MnemonicUx.Equals(input.MnemonicUx))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.SupportedTxs == input.SupportedTxs ||
                    this.SupportedTxs != null &&
                    input.SupportedTxs != null &&
                    this.SupportedTxs.SequenceEqual(input.SupportedTxs)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.DriverName != null)
                    hashCode = hashCode * 59 + this.DriverName.GetHashCode();
                if (this.DriverVersion != null)
                    hashCode = hashCode * 59 + this.DriverVersion.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.MnemonicUx != null)
                    hashCode = hashCode * 59 + this.MnemonicUx.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.SupportedTxs != null)
                    hashCode = hashCode * 59 + this.SupportedTxs.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
