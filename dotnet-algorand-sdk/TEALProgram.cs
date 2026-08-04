using Algorand.Algod.Model;
using Algorand.Algod.Model.Converters.MsgPack;
using Algorand.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;

namespace Algorand
{
    /// <summary>
    /// TEALProgram
    /// </summary>
    [JsonConverter(typeof(BytesConverter))]

    [MessagePackObject]
    [MessagePackFormatter(typeof(TEALProgramFormatterMsgPack))]
    public class TEALProgram
    {
        private byte[] program = null;
        [IgnoreMember]
        public byte[] Bytes
        {
            get
            {
                if (program == null) return null;
                return JavaHelper<byte>.ArrayCopyOf(program, program.Length);
            }
        }

        /// <summary>
        /// default values for serializer to ignore
        /// </summary>
        public TEALProgram() { }

        /// <summary>
        /// Initialize a TEALProgram based on the byte array.An ArgumentException is thrown if the program is invalid.
        /// </summary>
        /// <param name="program">program</param>
        [JsonConstructor]
        public TEALProgram(byte[] program)
        {
            if (program == null) return;
            
            this.program = JavaHelper<byte>.ArrayCopyOf(program, program.Length);
        }

        /// <summary>
        /// Validate the TEAL Program against known langspec
        /// </summary>
        /// <param name="errorMessage">Error message if error</param>
        /// <returns>false</returns>
        public bool Validate(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                Logic.ReadProgram(program, null);
                return true;
            }
            catch (Exception e)
            {
                errorMessage=e.Message;
            }
            return false;
        }

        /// <summary>
        /// Initialize a TEALProgram based on the base64 encoding.
        /// A runtime exception is thrown if the program is invalid.
        /// </summary>
        /// <param name="base64String">base64String</param>
        public TEALProgram(string base64String) : this(Convert.FromBase64String(base64String)) { }

        /// <summary>
        /// Creates Signature compatible with ed25519verify TEAL opcode from data and program bytes
        /// </summary>
        /// <param name="data">data byte[]</param>
        /// <param name="program">program byte[]</param>
        /// <returns>Signature</returns>
        public Signature Sign(byte[] data,Account signingAccount)
        {
            LogicsigSignature lsig = new LogicsigSignature(program);
            return signingAccount.TealSign(data, lsig.Address);
        }
    }
}