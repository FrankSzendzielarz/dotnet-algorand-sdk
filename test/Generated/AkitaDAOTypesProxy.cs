using System;
using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using AVM.ClientGenerator;
using AVM.ClientGenerator.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVM.ClientGenerator.ABI.ARC56;
using Algorand.AVM.ClientGenerator.ABI.ARC56;

namespace AkitaDAOTypesRegression
{


    public class AkitaDAOTypesProxy : ProxyBase
    {
        public override AppDescriptionArc56 App { get; set; }

        public AkitaDAOTypesProxy(DefaultApi defaultApi, ulong appId) : base(defaultApi, appId)
        {
            App = Newtonsoft.Json.JsonConvert.DeserializeObject<AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56>(Encoding.UTF8.GetString(Convert.FromBase64String(_ARC56DATA))) ?? throw new Exception("Error reading ARC56 data");

        }

        public class Structs
        {
            public class ProposalAddAllowances : AVMObjectType
            {
                public string Escrow { get; set; }

                public Structs.ProposalAddAllowancesAllowances[] Allowances { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Value = (Allowances ?? Array.Empty<Structs.ProposalAddAllowancesAllowances>()).ToList();
                    stringRef[ret.Count] = arrAllowances.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalAddAllowances Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalAddAllowances();
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    var indexAllowances = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Decode(bytes.Skip(indexAllowances + prefixOffset).ToArray());
                    ret.Allowances = arrAllowances.Value.ToArray();
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalAddAllowances);
                }
                public bool Equals(ProposalAddAllowances? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalAddAllowances left, ProposalAddAllowances right)
                {
                    return EqualityComparer<ProposalAddAllowances>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalAddAllowances left, ProposalAddAllowances right)
                {
                    return !(left == right);
                }

            }

            public class ProposalAddNamedPlugin : AVMObjectType
            {
                public string Name { get; set; }

                public ulong Plugin { get; set; }

                public Algorand.Address Caller { get; set; }

                public string Escrow { get; set; }

                public byte DelegationType { get; set; }

                public ulong LastValid { get; set; }

                public ulong Cooldown { get; set; }

                public Structs.ProposalAddNamedPluginMethods[] Methods { get; set; }

                public bool UseRounds { get; set; }

                public bool UseExecutionKey { get; set; }

                public bool CoverFees { get; set; }

                public bool DefaultToEscrow { get; set; }

                public ulong Fee { get; set; }

                public ulong Power { get; set; }

                public ulong Duration { get; set; }

                public ulong Participation { get; set; }

                public ulong Approval { get; set; }

                public string SourceLink { get; set; }

                public Structs.ProposalAddAllowancesAllowances[] Allowances { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.From(Name);
                    stringRef[ret.Count] = vName.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPlugin.From(Plugin);
                    ret.AddRange(vPlugin.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vCaller.From(Caller);
                    ret.AddRange(vCaller.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDelegationType = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vDelegationType.From(DelegationType);
                    ret.AddRange(vDelegationType.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vLastValid.From(LastValid);
                    ret.AddRange(vLastValid.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCooldown = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vCooldown.From(Cooldown);
                    ret.AddRange(vCooldown.Encode());
                    var arrMethods = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddNamedPluginMethods>(x => Structs.ProposalAddNamedPluginMethods.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrMethods.Value = (Methods ?? Array.Empty<Structs.ProposalAddNamedPluginMethods>()).ToList();
                    stringRef[ret.Count] = arrMethods.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseRounds = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vUseRounds.From(UseRounds);
                    ret.AddRange(vUseRounds.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vUseExecutionKey.From(UseExecutionKey);
                    ret.AddRange(vUseExecutionKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCoverFees = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vCoverFees.From(CoverFees);
                    ret.AddRange(vCoverFees.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDefaultToEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vDefaultToEscrow.From(DefaultToEscrow);
                    ret.AddRange(vDefaultToEscrow.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFee = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vFee.From(Fee);
                    ret.AddRange(vFee.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPower = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPower.From(Power);
                    ret.AddRange(vPower.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDuration = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vDuration.From(Duration);
                    ret.AddRange(vDuration.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vParticipation = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vParticipation.From(Participation);
                    ret.AddRange(vParticipation.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApproval = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vApproval.From(Approval);
                    ret.AddRange(vApproval.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSourceLink = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vSourceLink.From(SourceLink);
                    stringRef[ret.Count] = vSourceLink.Encode();
                    ret.AddRange(new byte[2]);
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Value = (Allowances ?? Array.Empty<Structs.ProposalAddAllowancesAllowances>()).ToList();
                    stringRef[ret.Count] = arrAllowances.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalAddNamedPlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalAddNamedPlugin();
                    uint count = 0;
                    var indexName = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.Decode(bytes.Skip(indexName + prefixOffset).ToArray());
                    var valueName = vName.ToValue();
                    if (valueName is string vNameValue) { ret.Name = vNameValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPlugin.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePlugin = vPlugin.ToValue();
                    if (valuePlugin is ulong vPluginValue) { ret.Plugin = vPluginValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vCaller.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCaller = vCaller.ToValue();
                    if (valueCaller is Algorand.Address vCallerValue) { ret.Caller = vCallerValue; }
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDelegationType = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vDelegationType.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDelegationType = vDelegationType.ToValue();
                    if (valueDelegationType is byte vDelegationTypeValue) { ret.DelegationType = vDelegationTypeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vLastValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueLastValid = vLastValid.ToValue();
                    if (valueLastValid is ulong vLastValidValue) { ret.LastValid = vLastValidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCooldown = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vCooldown.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCooldown = vCooldown.ToValue();
                    if (valueCooldown is ulong vCooldownValue) { ret.Cooldown = vCooldownValue; }
                    var indexMethods = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrMethods = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddNamedPluginMethods>(x => Structs.ProposalAddNamedPluginMethods.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrMethods.Decode(bytes.Skip(indexMethods + prefixOffset).ToArray());
                    ret.Methods = arrMethods.Value.ToArray();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseRounds = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vUseRounds.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueUseRounds = vUseRounds.ToValue();
                    if (valueUseRounds is bool vUseRoundsValue) { ret.UseRounds = vUseRoundsValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vUseExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueUseExecutionKey = vUseExecutionKey.ToValue();
                    if (valueUseExecutionKey is bool vUseExecutionKeyValue) { ret.UseExecutionKey = vUseExecutionKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCoverFees = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vCoverFees.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCoverFees = vCoverFees.ToValue();
                    if (valueCoverFees is bool vCoverFeesValue) { ret.CoverFees = vCoverFeesValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDefaultToEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vDefaultToEscrow.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDefaultToEscrow = vDefaultToEscrow.ToValue();
                    if (valueDefaultToEscrow is bool vDefaultToEscrowValue) { ret.DefaultToEscrow = vDefaultToEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFee = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vFee.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFee = vFee.ToValue();
                    if (valueFee is ulong vFeeValue) { ret.Fee = vFeeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPower = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPower.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePower = vPower.ToValue();
                    if (valuePower is ulong vPowerValue) { ret.Power = vPowerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDuration = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vDuration.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDuration = vDuration.ToValue();
                    if (valueDuration is ulong vDurationValue) { ret.Duration = vDurationValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vParticipation = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vParticipation.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueParticipation = vParticipation.ToValue();
                    if (valueParticipation is ulong vParticipationValue) { ret.Participation = vParticipationValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApproval = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vApproval.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueApproval = vApproval.ToValue();
                    if (valueApproval is ulong vApprovalValue) { ret.Approval = vApprovalValue; }
                    var indexSourceLink = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSourceLink = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vSourceLink.Decode(bytes.Skip(indexSourceLink + prefixOffset).ToArray());
                    var valueSourceLink = vSourceLink.ToValue();
                    if (valueSourceLink is string vSourceLinkValue) { ret.SourceLink = vSourceLinkValue; }
                    var indexAllowances = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Decode(bytes.Skip(indexAllowances + prefixOffset).ToArray());
                    ret.Allowances = arrAllowances.Value.ToArray();
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalAddNamedPlugin);
                }
                public bool Equals(ProposalAddNamedPlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalAddNamedPlugin left, ProposalAddNamedPlugin right)
                {
                    return EqualityComparer<ProposalAddNamedPlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalAddNamedPlugin left, ProposalAddNamedPlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalAddPlugin : AVMObjectType
            {
                public ulong Plugin { get; set; }

                public Algorand.Address Caller { get; set; }

                public string Escrow { get; set; }

                public byte DelegationType { get; set; }

                public ulong LastValid { get; set; }

                public ulong Cooldown { get; set; }

                public Structs.ProposalAddNamedPluginMethods[] Methods { get; set; }

                public bool UseRounds { get; set; }

                public bool UseExecutionKey { get; set; }

                public bool CoverFees { get; set; }

                public bool DefaultToEscrow { get; set; }

                public ulong Fee { get; set; }

                public ulong Power { get; set; }

                public ulong Duration { get; set; }

                public ulong Participation { get; set; }

                public ulong Approval { get; set; }

                public string SourceLink { get; set; }

                public Structs.ProposalAddAllowancesAllowances[] Allowances { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPlugin.From(Plugin);
                    ret.AddRange(vPlugin.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vCaller.From(Caller);
                    ret.AddRange(vCaller.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDelegationType = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vDelegationType.From(DelegationType);
                    ret.AddRange(vDelegationType.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vLastValid.From(LastValid);
                    ret.AddRange(vLastValid.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCooldown = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vCooldown.From(Cooldown);
                    ret.AddRange(vCooldown.Encode());
                    var arrMethods = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddNamedPluginMethods>(x => Structs.ProposalAddNamedPluginMethods.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrMethods.Value = (Methods ?? Array.Empty<Structs.ProposalAddNamedPluginMethods>()).ToList();
                    stringRef[ret.Count] = arrMethods.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseRounds = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vUseRounds.From(UseRounds);
                    ret.AddRange(vUseRounds.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vUseExecutionKey.From(UseExecutionKey);
                    ret.AddRange(vUseExecutionKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCoverFees = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vCoverFees.From(CoverFees);
                    ret.AddRange(vCoverFees.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDefaultToEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vDefaultToEscrow.From(DefaultToEscrow);
                    ret.AddRange(vDefaultToEscrow.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFee = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vFee.From(Fee);
                    ret.AddRange(vFee.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPower = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPower.From(Power);
                    ret.AddRange(vPower.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDuration = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vDuration.From(Duration);
                    ret.AddRange(vDuration.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vParticipation = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vParticipation.From(Participation);
                    ret.AddRange(vParticipation.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApproval = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vApproval.From(Approval);
                    ret.AddRange(vApproval.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSourceLink = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vSourceLink.From(SourceLink);
                    stringRef[ret.Count] = vSourceLink.Encode();
                    ret.AddRange(new byte[2]);
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Value = (Allowances ?? Array.Empty<Structs.ProposalAddAllowancesAllowances>()).ToList();
                    stringRef[ret.Count] = arrAllowances.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalAddPlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalAddPlugin();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPlugin.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePlugin = vPlugin.ToValue();
                    if (valuePlugin is ulong vPluginValue) { ret.Plugin = vPluginValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vCaller.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCaller = vCaller.ToValue();
                    if (valueCaller is Algorand.Address vCallerValue) { ret.Caller = vCallerValue; }
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDelegationType = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vDelegationType.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDelegationType = vDelegationType.ToValue();
                    if (valueDelegationType is byte vDelegationTypeValue) { ret.DelegationType = vDelegationTypeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vLastValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueLastValid = vLastValid.ToValue();
                    if (valueLastValid is ulong vLastValidValue) { ret.LastValid = vLastValidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCooldown = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vCooldown.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCooldown = vCooldown.ToValue();
                    if (valueCooldown is ulong vCooldownValue) { ret.Cooldown = vCooldownValue; }
                    var indexMethods = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrMethods = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddNamedPluginMethods>(x => Structs.ProposalAddNamedPluginMethods.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrMethods.Decode(bytes.Skip(indexMethods + prefixOffset).ToArray());
                    ret.Methods = arrMethods.Value.ToArray();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseRounds = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vUseRounds.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueUseRounds = vUseRounds.ToValue();
                    if (valueUseRounds is bool vUseRoundsValue) { ret.UseRounds = vUseRoundsValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vUseExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vUseExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueUseExecutionKey = vUseExecutionKey.ToValue();
                    if (valueUseExecutionKey is bool vUseExecutionKeyValue) { ret.UseExecutionKey = vUseExecutionKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCoverFees = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vCoverFees.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCoverFees = vCoverFees.ToValue();
                    if (valueCoverFees is bool vCoverFeesValue) { ret.CoverFees = vCoverFeesValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDefaultToEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vDefaultToEscrow.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDefaultToEscrow = vDefaultToEscrow.ToValue();
                    if (valueDefaultToEscrow is bool vDefaultToEscrowValue) { ret.DefaultToEscrow = vDefaultToEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFee = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vFee.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFee = vFee.ToValue();
                    if (valueFee is ulong vFeeValue) { ret.Fee = vFeeValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPower = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPower.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePower = vPower.ToValue();
                    if (valuePower is ulong vPowerValue) { ret.Power = vPowerValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vDuration = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vDuration.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueDuration = vDuration.ToValue();
                    if (valueDuration is ulong vDurationValue) { ret.Duration = vDurationValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vParticipation = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vParticipation.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueParticipation = vParticipation.ToValue();
                    if (valueParticipation is ulong vParticipationValue) { ret.Participation = vParticipationValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApproval = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vApproval.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueApproval = vApproval.ToValue();
                    if (valueApproval is ulong vApprovalValue) { ret.Approval = vApprovalValue; }
                    var indexSourceLink = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vSourceLink = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vSourceLink.Decode(bytes.Skip(indexSourceLink + prefixOffset).ToArray());
                    var valueSourceLink = vSourceLink.ToValue();
                    if (valueSourceLink is string vSourceLinkValue) { ret.SourceLink = vSourceLinkValue; }
                    var indexAllowances = queue.Dequeue() * 256 + queue.Dequeue();
                    var arrAllowances = new AVM.ClientGenerator.ABI.ARC4.Types.StructArray<Structs.ProposalAddAllowancesAllowances>(x => Structs.ProposalAddAllowancesAllowances.Parse(x)) { IsFixedLength = false, FixedLength = 0 };
                    arrAllowances.Decode(bytes.Skip(indexAllowances + prefixOffset).ToArray());
                    ret.Allowances = arrAllowances.Value.ToArray();
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalAddPlugin);
                }
                public bool Equals(ProposalAddPlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalAddPlugin left, ProposalAddPlugin right)
                {
                    return EqualityComparer<ProposalAddPlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalAddPlugin left, ProposalAddPlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalExecuteNamedPlugin : AVMObjectType
            {
                public string Name { get; set; }

                public byte[] ExecutionKey { get; set; }

                public byte[][] Groups { get; set; }

                public ulong FirstValid { get; set; }

                public ulong LastValid { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.From(Name);
                    stringRef[ret.Count] = vName.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    vExecutionKey.From(ExecutionKey);
                    ret.AddRange(vExecutionKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    vGroups.From(Groups);
                    ret.AddRange(vGroups.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vFirstValid.From(FirstValid);
                    ret.AddRange(vFirstValid.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vLastValid.From(LastValid);
                    ret.AddRange(vLastValid.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalExecuteNamedPlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalExecuteNamedPlugin();
                    uint count = 0;
                    var indexName = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.Decode(bytes.Skip(indexName + prefixOffset).ToArray());
                    var valueName = vName.ToValue();
                    if (valueName is string vNameValue) { ret.Name = vNameValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueExecutionKey = vExecutionKey.ToValue();
                    if (valueExecutionKey is byte[] vExecutionKeyValue) { ret.ExecutionKey = vExecutionKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    count = vGroups.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueGroups = vGroups.ToValue();
                    if (valueGroups is byte[][] vGroupsValue) { ret.Groups = vGroupsValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vFirstValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFirstValid = vFirstValid.ToValue();
                    if (valueFirstValid is ulong vFirstValidValue) { ret.FirstValid = vFirstValidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vLastValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueLastValid = vLastValid.ToValue();
                    if (valueLastValid is ulong vLastValidValue) { ret.LastValid = vLastValidValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalExecuteNamedPlugin);
                }
                public bool Equals(ProposalExecuteNamedPlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalExecuteNamedPlugin left, ProposalExecuteNamedPlugin right)
                {
                    return EqualityComparer<ProposalExecuteNamedPlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalExecuteNamedPlugin left, ProposalExecuteNamedPlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalExecutePlugin : AVMObjectType
            {
                public ulong Plugin { get; set; }

                public string Escrow { get; set; }

                public byte[] ExecutionKey { get; set; }

                public byte[][] Groups { get; set; }

                public ulong FirstValid { get; set; }

                public ulong LastValid { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPlugin.From(Plugin);
                    ret.AddRange(vPlugin.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    vExecutionKey.From(ExecutionKey);
                    ret.AddRange(vExecutionKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    vGroups.From(Groups);
                    ret.AddRange(vGroups.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vFirstValid.From(FirstValid);
                    ret.AddRange(vFirstValid.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vLastValid.From(LastValid);
                    ret.AddRange(vLastValid.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalExecutePlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalExecutePlugin();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPlugin.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePlugin = vPlugin.ToValue();
                    if (valuePlugin is ulong vPluginValue) { ret.Plugin = vPluginValue; }
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueExecutionKey = vExecutionKey.ToValue();
                    if (valueExecutionKey is byte[] vExecutionKeyValue) { ret.ExecutionKey = vExecutionKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    count = vGroups.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueGroups = vGroups.ToValue();
                    if (valueGroups is byte[][] vGroupsValue) { ret.Groups = vGroupsValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vFirstValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFirstValid = vFirstValid.ToValue();
                    if (valueFirstValid is ulong vFirstValidValue) { ret.FirstValid = vFirstValidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vLastValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueLastValid = vLastValid.ToValue();
                    if (valueLastValid is ulong vLastValidValue) { ret.LastValid = vLastValidValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalExecutePlugin);
                }
                public bool Equals(ProposalExecutePlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalExecutePlugin left, ProposalExecutePlugin right)
                {
                    return EqualityComparer<ProposalExecutePlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalExecutePlugin left, ProposalExecutePlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalNewEscrow : AVMObjectType
            {
                public string Escrow { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalNewEscrow Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalNewEscrow();
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalNewEscrow);
                }
                public bool Equals(ProposalNewEscrow? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalNewEscrow left, ProposalNewEscrow right)
                {
                    return EqualityComparer<ProposalNewEscrow>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalNewEscrow left, ProposalNewEscrow right)
                {
                    return !(left == right);
                }

            }

            public class ProposalRemoveAllowances : AVMObjectType
            {
                public string Escrow { get; set; }

                public ulong[] Assets { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAssets = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64[]");
                    vAssets.From(Assets);
                    ret.AddRange(vAssets.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalRemoveAllowances Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalRemoveAllowances();
                    uint count = 0;
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vAssets = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64[]");
                    count = vAssets.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueAssets = vAssets.ToValue();
                    if (valueAssets is ulong[] vAssetsValue) { ret.Assets = vAssetsValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalRemoveAllowances);
                }
                public bool Equals(ProposalRemoveAllowances? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalRemoveAllowances left, ProposalRemoveAllowances right)
                {
                    return EqualityComparer<ProposalRemoveAllowances>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalRemoveAllowances left, ProposalRemoveAllowances right)
                {
                    return !(left == right);
                }

            }

            public class ProposalRemoveExecutePlugin : AVMObjectType
            {
                public byte[] ExecutionKey { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    vExecutionKey.From(ExecutionKey);
                    ret.AddRange(vExecutionKey.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalRemoveExecutePlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ProposalRemoveExecutePlugin();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueExecutionKey = vExecutionKey.ToValue();
                    if (valueExecutionKey is byte[] vExecutionKeyValue) { ret.ExecutionKey = vExecutionKeyValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalRemoveExecutePlugin);
                }
                public bool Equals(ProposalRemoveExecutePlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalRemoveExecutePlugin left, ProposalRemoveExecutePlugin right)
                {
                    return EqualityComparer<ProposalRemoveExecutePlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalRemoveExecutePlugin left, ProposalRemoveExecutePlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalRemoveNamedPlugin : AVMObjectType
            {
                public string Name { get; set; }

                public ulong Plugin { get; set; }

                public Algorand.Address Caller { get; set; }

                public string Escrow { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.From(Name);
                    stringRef[ret.Count] = vName.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPlugin.From(Plugin);
                    ret.AddRange(vPlugin.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vCaller.From(Caller);
                    ret.AddRange(vCaller.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalRemoveNamedPlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalRemoveNamedPlugin();
                    uint count = 0;
                    var indexName = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vName = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vName.Decode(bytes.Skip(indexName + prefixOffset).ToArray());
                    var valueName = vName.ToValue();
                    if (valueName is string vNameValue) { ret.Name = vNameValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPlugin.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePlugin = vPlugin.ToValue();
                    if (valuePlugin is ulong vPluginValue) { ret.Plugin = vPluginValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vCaller.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCaller = vCaller.ToValue();
                    if (valueCaller is Algorand.Address vCallerValue) { ret.Caller = vCallerValue; }
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalRemoveNamedPlugin);
                }
                public bool Equals(ProposalRemoveNamedPlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalRemoveNamedPlugin left, ProposalRemoveNamedPlugin right)
                {
                    return EqualityComparer<ProposalRemoveNamedPlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalRemoveNamedPlugin left, ProposalRemoveNamedPlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalRemovePlugin : AVMObjectType
            {
                public ulong Plugin { get; set; }

                public Algorand.Address Caller { get; set; }

                public string Escrow { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vPlugin.From(Plugin);
                    ret.AddRange(vPlugin.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    vCaller.From(Caller);
                    ret.AddRange(vCaller.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalRemovePlugin Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalRemovePlugin();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vPlugin = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vPlugin.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valuePlugin = vPlugin.ToValue();
                    if (valuePlugin is ulong vPluginValue) { ret.Plugin = vPluginValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vCaller = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("address");
                    count = vCaller.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueCaller = vCaller.ToValue();
                    if (valueCaller is Algorand.Address vCallerValue) { ret.Caller = vCallerValue; }
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalRemovePlugin);
                }
                public bool Equals(ProposalRemovePlugin? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalRemovePlugin left, ProposalRemovePlugin right)
                {
                    return EqualityComparer<ProposalRemovePlugin>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalRemovePlugin left, ProposalRemovePlugin right)
                {
                    return !(left == right);
                }

            }

            public class ProposalToggleEscrowLock : AVMObjectType
            {
                public string Escrow { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.From(Escrow);
                    stringRef[ret.Count] = vEscrow.Encode();
                    ret.AddRange(new byte[2]);
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalToggleEscrowLock Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalToggleEscrowLock();
                    var indexEscrow = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vEscrow = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vEscrow.Decode(bytes.Skip(indexEscrow + prefixOffset).ToArray());
                    var valueEscrow = vEscrow.ToValue();
                    if (valueEscrow is string vEscrowValue) { ret.Escrow = vEscrowValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalToggleEscrowLock);
                }
                public bool Equals(ProposalToggleEscrowLock? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalToggleEscrowLock left, ProposalToggleEscrowLock right)
                {
                    return EqualityComparer<ProposalToggleEscrowLock>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalToggleEscrowLock left, ProposalToggleEscrowLock right)
                {
                    return !(left == right);
                }

            }

            public class ProposalUpdateField : AVMObjectType
            {
                public string Field { get; set; }

                public byte[] Value { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField.From(Field);
                    stringRef[ret.Count] = vField.Encode();
                    ret.AddRange(new byte[2]);
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vValue = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    vValue.From(Value);
                    ret.AddRange(vValue.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalUpdateField Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var prefixOffset = 0;
                    var retPrefix = new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                    if (retPrefix.SequenceEqual(Constants.RetPrefix))
                    {
                        prefixOffset = 4;
                        for (int i = 0; i < 4 && queue.Count > 0; i++) { queue.Dequeue(); }
                    }
                    var ret = new ProposalUpdateField();
                    uint count = 0;
                    var indexField = queue.Dequeue() * 256 + queue.Dequeue();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("string");
                    vField.Decode(bytes.Skip(indexField + prefixOffset).ToArray());
                    var valueField = vField.ToValue();
                    if (valueField is string vFieldValue) { ret.Field = vFieldValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vValue = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[]");
                    count = vValue.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueValue = vValue.ToValue();
                    if (valueValue is byte[] vValueValue) { ret.Value = vValueValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalUpdateField);
                }
                public bool Equals(ProposalUpdateField? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalUpdateField left, ProposalUpdateField right)
                {
                    return EqualityComparer<ProposalUpdateField>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalUpdateField left, ProposalUpdateField right)
                {
                    return !(left == right);
                }

            }

            public class ProposalUpgradeApp : AVMObjectType
            {
                public ulong App { get; set; }

                public byte[] ExecutionKey { get; set; }

                public byte[][] Groups { get; set; }

                public ulong FirstValid { get; set; }

                public ulong LastValid { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApp = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vApp.From(App);
                    ret.AddRange(vApp.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    vExecutionKey.From(ExecutionKey);
                    ret.AddRange(vExecutionKey.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    vGroups.From(Groups);
                    ret.AddRange(vGroups.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vFirstValid.From(FirstValid);
                    ret.AddRange(vFirstValid.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vLastValid.From(LastValid);
                    ret.AddRange(vLastValid.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalUpgradeApp Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ProposalUpgradeApp();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vApp = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vApp.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueApp = vApp.ToValue();
                    if (valueApp is ulong vAppValue) { ret.App = vAppValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vExecutionKey = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32]");
                    count = vExecutionKey.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueExecutionKey = vExecutionKey.ToValue();
                    if (valueExecutionKey is byte[] vExecutionKeyValue) { ret.ExecutionKey = vExecutionKeyValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vGroups = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[32][]");
                    count = vGroups.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueGroups = vGroups.ToValue();
                    if (valueGroups is byte[][] vGroupsValue) { ret.Groups = vGroupsValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vFirstValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vFirstValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueFirstValid = vFirstValid.ToValue();
                    if (valueFirstValid is ulong vFirstValidValue) { ret.FirstValid = vFirstValidValue; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vLastValid = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vLastValid.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueLastValid = vLastValid.ToValue();
                    if (valueLastValid is ulong vLastValidValue) { ret.LastValid = vLastValidValue; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalUpgradeApp);
                }
                public bool Equals(ProposalUpgradeApp? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalUpgradeApp left, ProposalUpgradeApp right)
                {
                    return EqualityComparer<ProposalUpgradeApp>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalUpgradeApp left, ProposalUpgradeApp right)
                {
                    return !(left == right);
                }

            }

            public class ProposalAddAllowancesAllowances : AVMObjectType
            {
                public ulong Field0 { get; set; }

                public byte Field1 { get; set; }

                public ulong Field2 { get; set; }

                public ulong Field3 { get; set; }

                public ulong Field4 { get; set; }

                public bool Field5 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    vField1.From(Field1);
                    ret.AddRange(vField1.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField2.From(Field2);
                    ret.AddRange(vField2.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField3 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField3.From(Field3);
                    ret.AddRange(vField3.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField4 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField4.From(Field4);
                    ret.AddRange(vField4.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField5 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    vField5.From(Field5);
                    ret.AddRange(vField5.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalAddAllowancesAllowances Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ProposalAddAllowancesAllowances();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is ulong vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint8");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is byte vField1Value) { ret.Field1 = vField1Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField2 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField2.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField2 = vField2.ToValue();
                    if (valueField2 is ulong vField2Value) { ret.Field2 = vField2Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField3 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField3.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField3 = vField3.ToValue();
                    if (valueField3 is ulong vField3Value) { ret.Field3 = vField3Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField4 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField4.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField4 = vField4.ToValue();
                    if (valueField4 is ulong vField4Value) { ret.Field4 = vField4Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField5 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("bool");
                    count = vField5.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField5 = vField5.ToValue();
                    if (valueField5 is bool vField5Value) { ret.Field5 = vField5Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalAddAllowancesAllowances);
                }
                public bool Equals(ProposalAddAllowancesAllowances? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalAddAllowancesAllowances left, ProposalAddAllowancesAllowances right)
                {
                    return EqualityComparer<ProposalAddAllowancesAllowances>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalAddAllowancesAllowances left, ProposalAddAllowancesAllowances right)
                {
                    return !(left == right);
                }

            }

            public class ProposalAddNamedPluginMethods : AVMObjectType
            {
                public byte[] Field0 { get; set; }

                public ulong Field1 { get; set; }

                public byte[] ToByteArray()
                {
                    var ret = new List<byte>();
                    var stringRef = new Dictionary<int, byte[]>();
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[4]");
                    vField0.From(Field0);
                    ret.AddRange(vField0.Encode());
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    vField1.From(Field1);
                    ret.AddRange(vField1.Encode());
                    foreach (var item in stringRef)
                    {
                        var b1 = ret.Count;
                        ret[item.Key] = Convert.ToByte(b1 / 256);
                        ret[item.Key + 1] = Convert.ToByte(b1 % 256);
                        ret.AddRange(item.Value);
                    }
                    return ret.ToArray();

                }

                public static ProposalAddNamedPluginMethods Parse(byte[] bytes)
                {
                    var queue = new Queue<byte>(bytes);
                    var ret = new ProposalAddNamedPluginMethods();
                    uint count = 0;
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField0 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("byte[4]");
                    count = vField0.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField0 = vField0.ToValue();
                    if (valueField0 is byte[] vField0Value) { ret.Field0 = vField0Value; }
                    AVM.ClientGenerator.ABI.ARC4.Types.WireType vField1 = AVM.ClientGenerator.ABI.ARC4.Types.WireType.FromABIDescription("uint64");
                    count = vField1.Decode(queue.ToArray());
                    for (int i = 0; i < Convert.ToInt32(count); i++) { queue.Dequeue(); }
                    var valueField1 = vField1.ToValue();
                    if (valueField1 is ulong vField1Value) { ret.Field1 = vField1Value; }
                    return ret;

                }

                public override string ToString()
                {
                    return $"{this.GetType().ToString()} {BitConverter.ToString(ToByteArray()).Replace("-", "")}";
                }
                public override bool Equals(object? obj)
                {
                    return Equals(obj as ProposalAddNamedPluginMethods);
                }
                public bool Equals(ProposalAddNamedPluginMethods? other)
                {
                    return other is not null && ToByteArray().SequenceEqual(other.ToByteArray());
                }
                public override int GetHashCode()
                {
                    return ToByteArray().GetHashCode();
                }
                public static bool operator ==(ProposalAddNamedPluginMethods left, ProposalAddNamedPluginMethods right)
                {
                    return EqualityComparer<ProposalAddNamedPluginMethods>.Default.Equals(left, right);
                }
                public static bool operator !=(ProposalAddNamedPluginMethods left, ProposalAddNamedPluginMethods right)
                {
                    return !(left == right);
                }

            }

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalUpgradeApp</param>
        public async Task<Structs.ProposalUpgradeApp> ProposalUpgradeAppShape(Structs.ProposalUpgradeApp shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 146, 179, 88, 150 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalUpgradeApp.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalUpgradeAppShape_Transactions(Structs.ProposalUpgradeApp shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 146, 179, 88, 150 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalAddPlugin</param>
        public async Task<Structs.ProposalAddPlugin> ProposalAddPluginShape(Structs.ProposalAddPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 126, 228, 186, 183 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalAddPlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalAddPluginShape_Transactions(Structs.ProposalAddPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 126, 228, 186, 183 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalAddNamedPlugin</param>
        public async Task<Structs.ProposalAddNamedPlugin> ProposalAddNamedPluginShape(Structs.ProposalAddNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 213, 243, 255 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalAddNamedPlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalAddNamedPluginShape_Transactions(Structs.ProposalAddNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 130, 213, 243, 255 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalRemovePlugin</param>
        public async Task<Structs.ProposalRemovePlugin> ProposalRemovePluginShape(Structs.ProposalRemovePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 243, 148, 26, 44 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalRemovePlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalRemovePluginShape_Transactions(Structs.ProposalRemovePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 243, 148, 26, 44 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalRemoveNamedPlugin</param>
        public async Task<Structs.ProposalRemoveNamedPlugin> ProposalRemoveNamedPluginShape(Structs.ProposalRemoveNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 241, 207, 34, 204 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalRemoveNamedPlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalRemoveNamedPluginShape_Transactions(Structs.ProposalRemoveNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 241, 207, 34, 204 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalExecutePlugin</param>
        public async Task<Structs.ProposalExecutePlugin> ProposalExecutePluginShape(Structs.ProposalExecutePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 189, 78, 247, 48 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalExecutePlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalExecutePluginShape_Transactions(Structs.ProposalExecutePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 189, 78, 247, 48 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalExecuteNamedPlugin</param>
        public async Task<Structs.ProposalExecuteNamedPlugin> ProposalExecuteNamedPluginShape(Structs.ProposalExecuteNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 235, 171, 94, 20 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalExecuteNamedPlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalExecuteNamedPluginShape_Transactions(Structs.ProposalExecuteNamedPlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 235, 171, 94, 20 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalRemoveExecutePlugin</param>
        public async Task<Structs.ProposalRemoveExecutePlugin> ProposalRemoveExecutePluginShape(Structs.ProposalRemoveExecutePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 107, 138, 189, 47 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalRemoveExecutePlugin.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalRemoveExecutePluginShape_Transactions(Structs.ProposalRemoveExecutePlugin shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 107, 138, 189, 47 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalAddAllowances</param>
        public async Task<Structs.ProposalAddAllowances> ProposalAddAllowancesShape(Structs.ProposalAddAllowances shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 252, 175, 132, 32 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalAddAllowances.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalAddAllowancesShape_Transactions(Structs.ProposalAddAllowances shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 252, 175, 132, 32 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalRemoveAllowances</param>
        public async Task<Structs.ProposalRemoveAllowances> ProposalRemoveAllowancesShape(Structs.ProposalRemoveAllowances shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 85, 206, 92, 169 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalRemoveAllowances.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalRemoveAllowancesShape_Transactions(Structs.ProposalRemoveAllowances shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 85, 206, 92, 169 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalNewEscrow</param>
        public async Task<Structs.ProposalNewEscrow> ProposalNewEscrowShape(Structs.ProposalNewEscrow shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 166, 56, 190, 35 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalNewEscrow.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalNewEscrowShape_Transactions(Structs.ProposalNewEscrow shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 166, 56, 190, 35 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalToggleEscrowLock</param>
        public async Task<Structs.ProposalToggleEscrowLock> ProposalToggleEscrowLockShape(Structs.ProposalToggleEscrowLock shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 80, 170, 184, 29 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalToggleEscrowLock.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalToggleEscrowLockShape_Transactions(Structs.ProposalToggleEscrowLock shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 80, 170, 184, 29 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///
        ///</summary>
        /// <param name="shape"> ProposalUpdateField</param>
        public async Task<Structs.ProposalUpdateField> ProposalUpdateFieldShape(Structs.ProposalUpdateField shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 20, 157, 60, 203 };

            var result = await base.SimApp(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);
            var lastLogBytes = result.Last();
            if (lastLogBytes.Length < 4 || lastLogBytes[0] != 21 || lastLogBytes[1] != 31 || lastLogBytes[2] != 124 || lastLogBytes[3] != 117) throw new Exception("Invalid ABI handle");
            return Structs.ProposalUpdateField.Parse(lastLogBytes.Skip(4).ToArray());

        }

        public async Task<List<Transaction>> ProposalUpdateFieldShape_Transactions(Structs.ProposalUpdateField shape, Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 20, 157, 60, 203 };

            return await base.MakeTransactionList(new List<object> { abiHandle, shape }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        ///<summary>
        ///Constructor Bare Action
        ///</summary>
        public async Task CreateApplication(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.CreateApplication)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 0, 193, 250, 21 };

            var result = await base.CallApp(new List<object> { }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        public async Task<List<Transaction>> CreateApplication_Transactions(Account _tx_sender, ulong? _tx_fee, string _tx_note = "", ulong _tx_roundValidity = 1000, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.CreateApplication)
        {
            _tx_boxes ??= new List<BoxRef>();
            _tx_transactions ??= new List<Transaction>();
            _tx_assets ??= new List<ulong>();
            _tx_apps ??= new List<ulong>();
            _tx_accounts ??= new List<Address>();
            byte[] abiHandle = { 0, 193, 250, 21 };

            return await base.MakeTransactionList(new List<object> { }, _tx_fee: _tx_fee, _tx_callType: _tx_callType, _tx_roundValidity: _tx_roundValidity, _tx_note: _tx_note, _tx_sender: _tx_sender, _tx_transactions: _tx_transactions, _tx_apps: _tx_apps, _tx_assets: _tx_assets, _tx_accounts: _tx_accounts, _tx_boxes: _tx_boxes);

        }

        protected override ulong? ExtraProgramPages { get; set; } = 0;
        protected string _ARC56DATA = "eyJhcmNzIjpbMjIsMjhdLCJuYW1lIjoiQWtpdGFEQU9UeXBlcyIsImRlc2MiOm51bGwsIm5ldHdvcmtzIjp7fSwic3RydWN0cyI6eyJQcm9wb3NhbEFkZEFsbG93YW5jZXMiOlt7Im5hbWUiOiJlc2Nyb3ciLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoiYWxsb3dhbmNlcyIsInR5cGUiOiJQcm9wb3NhbEFkZEFsbG93YW5jZXNBbGxvd2FuY2VzW10ifV0sIlByb3Bvc2FsQWRkTmFtZWRQbHVnaW4iOlt7Im5hbWUiOiJuYW1lIiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6InBsdWdpbiIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJjYWxsZXIiLCJ0eXBlIjoiYWRkcmVzcyJ9LHsibmFtZSI6ImVzY3JvdyIsInR5cGUiOiJzdHJpbmcifSx7Im5hbWUiOiJkZWxlZ2F0aW9uVHlwZSIsInR5cGUiOiJ1aW50OCJ9LHsibmFtZSI6Imxhc3RWYWxpZCIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJjb29sZG93biIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJtZXRob2RzIiwidHlwZSI6IlByb3Bvc2FsQWRkTmFtZWRQbHVnaW5NZXRob2RzW10ifSx7Im5hbWUiOiJ1c2VSb3VuZHMiLCJ0eXBlIjoiYm9vbCJ9LHsibmFtZSI6InVzZUV4ZWN1dGlvbktleSIsInR5cGUiOiJib29sIn0seyJuYW1lIjoiY292ZXJGZWVzIiwidHlwZSI6ImJvb2wifSx7Im5hbWUiOiJkZWZhdWx0VG9Fc2Nyb3ciLCJ0eXBlIjoiYm9vbCJ9LHsibmFtZSI6ImZlZSIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJwb3dlciIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJkdXJhdGlvbiIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJwYXJ0aWNpcGF0aW9uIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImFwcHJvdmFsIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6InNvdXJjZUxpbmsiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoiYWxsb3dhbmNlcyIsInR5cGUiOiJQcm9wb3NhbEFkZEFsbG93YW5jZXNBbGxvd2FuY2VzW10ifV0sIlByb3Bvc2FsQWRkUGx1Z2luIjpbeyJuYW1lIjoicGx1Z2luIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImNhbGxlciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoiZXNjcm93IiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6ImRlbGVnYXRpb25UeXBlIiwidHlwZSI6InVpbnQ4In0seyJuYW1lIjoibGFzdFZhbGlkIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImNvb2xkb3duIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6Im1ldGhvZHMiLCJ0eXBlIjoiUHJvcG9zYWxBZGROYW1lZFBsdWdpbk1ldGhvZHNbXSJ9LHsibmFtZSI6InVzZVJvdW5kcyIsInR5cGUiOiJib29sIn0seyJuYW1lIjoidXNlRXhlY3V0aW9uS2V5IiwidHlwZSI6ImJvb2wifSx7Im5hbWUiOiJjb3ZlckZlZXMiLCJ0eXBlIjoiYm9vbCJ9LHsibmFtZSI6ImRlZmF1bHRUb0VzY3JvdyIsInR5cGUiOiJib29sIn0seyJuYW1lIjoiZmVlIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6InBvd2VyIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImR1cmF0aW9uIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6InBhcnRpY2lwYXRpb24iLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiYXBwcm92YWwiLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoic291cmNlTGluayIsInR5cGUiOiJzdHJpbmcifSx7Im5hbWUiOiJhbGxvd2FuY2VzIiwidHlwZSI6IlByb3Bvc2FsQWRkQWxsb3dhbmNlc0FsbG93YW5jZXNbXSJ9XSwiUHJvcG9zYWxFeGVjdXRlTmFtZWRQbHVnaW4iOlt7Im5hbWUiOiJuYW1lIiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6ImV4ZWN1dGlvbktleSIsInR5cGUiOiJieXRlWzMyXSJ9LHsibmFtZSI6Imdyb3VwcyIsInR5cGUiOiJieXRlWzMyXVtdIn0seyJuYW1lIjoiZmlyc3RWYWxpZCIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJsYXN0VmFsaWQiLCJ0eXBlIjoidWludDY0In1dLCJQcm9wb3NhbEV4ZWN1dGVQbHVnaW4iOlt7Im5hbWUiOiJwbHVnaW4iLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiZXNjcm93IiwidHlwZSI6InN0cmluZyJ9LHsibmFtZSI6ImV4ZWN1dGlvbktleSIsInR5cGUiOiJieXRlWzMyXSJ9LHsibmFtZSI6Imdyb3VwcyIsInR5cGUiOiJieXRlWzMyXVtdIn0seyJuYW1lIjoiZmlyc3RWYWxpZCIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJsYXN0VmFsaWQiLCJ0eXBlIjoidWludDY0In1dLCJQcm9wb3NhbE5ld0VzY3JvdyI6W3sibmFtZSI6ImVzY3JvdyIsInR5cGUiOiJzdHJpbmcifV0sIlByb3Bvc2FsUmVtb3ZlQWxsb3dhbmNlcyI6W3sibmFtZSI6ImVzY3JvdyIsInR5cGUiOiJzdHJpbmcifSx7Im5hbWUiOiJhc3NldHMiLCJ0eXBlIjoidWludDY0W10ifV0sIlByb3Bvc2FsUmVtb3ZlRXhlY3V0ZVBsdWdpbiI6W3sibmFtZSI6ImV4ZWN1dGlvbktleSIsInR5cGUiOiJieXRlWzMyXSJ9XSwiUHJvcG9zYWxSZW1vdmVOYW1lZFBsdWdpbiI6W3sibmFtZSI6Im5hbWUiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoicGx1Z2luIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImNhbGxlciIsInR5cGUiOiJhZGRyZXNzIn0seyJuYW1lIjoiZXNjcm93IiwidHlwZSI6InN0cmluZyJ9XSwiUHJvcG9zYWxSZW1vdmVQbHVnaW4iOlt7Im5hbWUiOiJwbHVnaW4iLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiY2FsbGVyIiwidHlwZSI6ImFkZHJlc3MifSx7Im5hbWUiOiJlc2Nyb3ciLCJ0eXBlIjoic3RyaW5nIn1dLCJQcm9wb3NhbFRvZ2dsZUVzY3Jvd0xvY2siOlt7Im5hbWUiOiJlc2Nyb3ciLCJ0eXBlIjoic3RyaW5nIn1dLCJQcm9wb3NhbFVwZGF0ZUZpZWxkIjpbeyJuYW1lIjoiZmllbGQiLCJ0eXBlIjoic3RyaW5nIn0seyJuYW1lIjoidmFsdWUiLCJ0eXBlIjoiYnl0ZVtdIn1dLCJQcm9wb3NhbFVwZ3JhZGVBcHAiOlt7Im5hbWUiOiJhcHAiLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiZXhlY3V0aW9uS2V5IiwidHlwZSI6ImJ5dGVbMzJdIn0seyJuYW1lIjoiZ3JvdXBzIiwidHlwZSI6ImJ5dGVbMzJdW10ifSx7Im5hbWUiOiJmaXJzdFZhbGlkIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6Imxhc3RWYWxpZCIsInR5cGUiOiJ1aW50NjQifV0sIlByb3Bvc2FsQWRkQWxsb3dhbmNlc0FsbG93YW5jZXMiOlt7Im5hbWUiOiJmaWVsZDAiLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiZmllbGQxIiwidHlwZSI6InVpbnQ4In0seyJuYW1lIjoiZmllbGQyIiwidHlwZSI6InVpbnQ2NCJ9LHsibmFtZSI6ImZpZWxkMyIsInR5cGUiOiJ1aW50NjQifSx7Im5hbWUiOiJmaWVsZDQiLCJ0eXBlIjoidWludDY0In0seyJuYW1lIjoiZmllbGQ1IiwidHlwZSI6ImJvb2wifV0sIlByb3Bvc2FsQWRkTmFtZWRQbHVnaW5NZXRob2RzIjpbeyJuYW1lIjoiZmllbGQwIiwidHlwZSI6ImJ5dGVbNF0ifSx7Im5hbWUiOiJmaWVsZDEiLCJ0eXBlIjoidWludDY0In1dfSwiTWV0aG9kcyI6W3sibmFtZSI6InByb3Bvc2FsVXBncmFkZUFwcFNoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsYnl0ZVszMl0sYnl0ZVszMl1bXSx1aW50NjQsdWludDY0KSIsInN0cnVjdCI6IlByb3Bvc2FsVXBncmFkZUFwcCIsIm5hbWUiOiJzaGFwZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIodWludDY0LGJ5dGVbMzJdLGJ5dGVbMzJdW10sdWludDY0LHVpbnQ2NCkiLCJzdHJ1Y3QiOiJQcm9wb3NhbFVwZ3JhZGVBcHAiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InByb3Bvc2FsQWRkUGx1Z2luU2hhcGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHVpbnQ2NCxhZGRyZXNzLHN0cmluZyx1aW50OCx1aW50NjQsdWludDY0LChieXRlWzRdLHVpbnQ2NClbXSxib29sLGJvb2wsYm9vbCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsc3RyaW5nLCh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbClbXSkiLCJzdHJ1Y3QiOiJQcm9wb3NhbEFkZFBsdWdpbiIsIm5hbWUiOiJzaGFwZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIodWludDY0LGFkZHJlc3Msc3RyaW5nLHVpbnQ4LHVpbnQ2NCx1aW50NjQsKGJ5dGVbNF0sdWludDY0KVtdLGJvb2wsYm9vbCxib29sLGJvb2wsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCxzdHJpbmcsKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKVtdKSIsInN0cnVjdCI6IlByb3Bvc2FsQWRkUGx1Z2luIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJwcm9wb3NhbEFkZE5hbWVkUGx1Z2luU2hhcGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHN0cmluZyx1aW50NjQsYWRkcmVzcyxzdHJpbmcsdWludDgsdWludDY0LHVpbnQ2NCwoYnl0ZVs0XSx1aW50NjQpW10sYm9vbCxib29sLGJvb2wsYm9vbCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHN0cmluZywodWludDY0LHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wpW10pIiwic3RydWN0IjoiUHJvcG9zYWxBZGROYW1lZFBsdWdpbiIsIm5hbWUiOiJzaGFwZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIoc3RyaW5nLHVpbnQ2NCxhZGRyZXNzLHN0cmluZyx1aW50OCx1aW50NjQsdWludDY0LChieXRlWzRdLHVpbnQ2NClbXSxib29sLGJvb2wsYm9vbCxib29sLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsc3RyaW5nLCh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbClbXSkiLCJzdHJ1Y3QiOiJQcm9wb3NhbEFkZE5hbWVkUGx1Z2luIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJwcm9wb3NhbFJlbW92ZVBsdWdpblNoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6Iih1aW50NjQsYWRkcmVzcyxzdHJpbmcpIiwic3RydWN0IjoiUHJvcG9zYWxSZW1vdmVQbHVnaW4iLCJuYW1lIjoic2hhcGUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHVpbnQ2NCxhZGRyZXNzLHN0cmluZykiLCJzdHJ1Y3QiOiJQcm9wb3NhbFJlbW92ZVBsdWdpbiIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoicHJvcG9zYWxSZW1vdmVOYW1lZFBsdWdpblNoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihzdHJpbmcsdWludDY0LGFkZHJlc3Msc3RyaW5nKSIsInN0cnVjdCI6IlByb3Bvc2FsUmVtb3ZlTmFtZWRQbHVnaW4iLCJuYW1lIjoic2hhcGUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHN0cmluZyx1aW50NjQsYWRkcmVzcyxzdHJpbmcpIiwic3RydWN0IjoiUHJvcG9zYWxSZW1vdmVOYW1lZFBsdWdpbiIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoicHJvcG9zYWxFeGVjdXRlUGx1Z2luU2hhcGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHVpbnQ2NCxzdHJpbmcsYnl0ZVszMl0sYnl0ZVszMl1bXSx1aW50NjQsdWludDY0KSIsInN0cnVjdCI6IlByb3Bvc2FsRXhlY3V0ZVBsdWdpbiIsIm5hbWUiOiJzaGFwZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIodWludDY0LHN0cmluZyxieXRlWzMyXSxieXRlWzMyXVtdLHVpbnQ2NCx1aW50NjQpIiwic3RydWN0IjoiUHJvcG9zYWxFeGVjdXRlUGx1Z2luIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJwcm9wb3NhbEV4ZWN1dGVOYW1lZFBsdWdpblNoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihzdHJpbmcsYnl0ZVszMl0sYnl0ZVszMl1bXSx1aW50NjQsdWludDY0KSIsInN0cnVjdCI6IlByb3Bvc2FsRXhlY3V0ZU5hbWVkUGx1Z2luIiwibmFtZSI6InNoYXBlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihzdHJpbmcsYnl0ZVszMl0sYnl0ZVszMl1bXSx1aW50NjQsdWludDY0KSIsInN0cnVjdCI6IlByb3Bvc2FsRXhlY3V0ZU5hbWVkUGx1Z2luIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJwcm9wb3NhbFJlbW92ZUV4ZWN1dGVQbHVnaW5TaGFwZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoYnl0ZVszMl0pIiwic3RydWN0IjoiUHJvcG9zYWxSZW1vdmVFeGVjdXRlUGx1Z2luIiwibmFtZSI6InNoYXBlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihieXRlWzMyXSkiLCJzdHJ1Y3QiOiJQcm9wb3NhbFJlbW92ZUV4ZWN1dGVQbHVnaW4iLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InByb3Bvc2FsQWRkQWxsb3dhbmNlc1NoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihzdHJpbmcsKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sKVtdKSIsInN0cnVjdCI6IlByb3Bvc2FsQWRkQWxsb3dhbmNlcyIsIm5hbWUiOiJzaGFwZSIsImRlc2MiOm51bGwsImRlZmF1bHRWYWx1ZSI6bnVsbH1dLCJyZXR1cm5zIjp7InR5cGUiOiIoc3RyaW5nLCh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbClbXSkiLCJzdHJ1Y3QiOiJQcm9wb3NhbEFkZEFsbG93YW5jZXMiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InByb3Bvc2FsUmVtb3ZlQWxsb3dhbmNlc1NoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihzdHJpbmcsdWludDY0W10pIiwic3RydWN0IjoiUHJvcG9zYWxSZW1vdmVBbGxvd2FuY2VzIiwibmFtZSI6InNoYXBlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihzdHJpbmcsdWludDY0W10pIiwic3RydWN0IjoiUHJvcG9zYWxSZW1vdmVBbGxvd2FuY2VzIiwiZGVzYyI6bnVsbH0sImFjdGlvbnMiOnsiY3JlYXRlIjpbXSwiY2FsbCI6WyJOb09wIl19LCJyZWFkb25seSI6dHJ1ZSwiZXZlbnRzIjpbXSwicmVjb21tZW5kYXRpb25zIjp7ImlubmVyVHJhbnNhY3Rpb25Db3VudCI6bnVsbCwiYm94ZXMiOm51bGwsImFjY291bnRzIjpudWxsLCJhcHBzIjpudWxsLCJhc3NldHMiOm51bGx9fSx7Im5hbWUiOiJwcm9wb3NhbE5ld0VzY3Jvd1NoYXBlIiwiZGVzYyI6bnVsbCwiYXJncyI6W3sidHlwZSI6IihzdHJpbmcpIiwic3RydWN0IjoiUHJvcG9zYWxOZXdFc2Nyb3ciLCJuYW1lIjoic2hhcGUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHN0cmluZykiLCJzdHJ1Y3QiOiJQcm9wb3NhbE5ld0VzY3JvdyIsImRlc2MiOm51bGx9LCJhY3Rpb25zIjp7ImNyZWF0ZSI6W10sImNhbGwiOlsiTm9PcCJdfSwicmVhZG9ubHkiOnRydWUsImV2ZW50cyI6W10sInJlY29tbWVuZGF0aW9ucyI6eyJpbm5lclRyYW5zYWN0aW9uQ291bnQiOm51bGwsImJveGVzIjpudWxsLCJhY2NvdW50cyI6bnVsbCwiYXBwcyI6bnVsbCwiYXNzZXRzIjpudWxsfX0seyJuYW1lIjoicHJvcG9zYWxUb2dnbGVFc2Nyb3dMb2NrU2hhcGUiLCJkZXNjIjpudWxsLCJhcmdzIjpbeyJ0eXBlIjoiKHN0cmluZykiLCJzdHJ1Y3QiOiJQcm9wb3NhbFRvZ2dsZUVzY3Jvd0xvY2siLCJuYW1lIjoic2hhcGUiLCJkZXNjIjpudWxsLCJkZWZhdWx0VmFsdWUiOm51bGx9XSwicmV0dXJucyI6eyJ0eXBlIjoiKHN0cmluZykiLCJzdHJ1Y3QiOiJQcm9wb3NhbFRvZ2dsZUVzY3Jvd0xvY2siLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19LHsibmFtZSI6InByb3Bvc2FsVXBkYXRlRmllbGRTaGFwZSIsImRlc2MiOm51bGwsImFyZ3MiOlt7InR5cGUiOiIoc3RyaW5nLGJ5dGVbXSkiLCJzdHJ1Y3QiOiJQcm9wb3NhbFVwZGF0ZUZpZWxkIiwibmFtZSI6InNoYXBlIiwiZGVzYyI6bnVsbCwiZGVmYXVsdFZhbHVlIjpudWxsfV0sInJldHVybnMiOnsidHlwZSI6IihzdHJpbmcsYnl0ZVtdKSIsInN0cnVjdCI6IlByb3Bvc2FsVXBkYXRlRmllbGQiLCJkZXNjIjpudWxsfSwiYWN0aW9ucyI6eyJjcmVhdGUiOltdLCJjYWxsIjpbIk5vT3AiXX0sInJlYWRvbmx5Ijp0cnVlLCJldmVudHMiOltdLCJyZWNvbW1lbmRhdGlvbnMiOnsiaW5uZXJUcmFuc2FjdGlvbkNvdW50IjpudWxsLCJib3hlcyI6bnVsbCwiYWNjb3VudHMiOm51bGwsImFwcHMiOm51bGwsImFzc2V0cyI6bnVsbH19XSwic3RhdGUiOnsic2NoZW1hIjp7Imdsb2JhbCI6eyJpbnRzIjowLCJieXRlcyI6MH0sImxvY2FsIjp7ImludHMiOjAsImJ5dGVzIjowfX0sImtleXMiOnsiZ2xvYmFsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJrZXkiOiIifSwibG9jYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsImtleSI6IiJ9fSwibWFwcyI6eyJnbG9iYWwiOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH0sImxvY2FsIjp7ImRlc2MiOm51bGwsImtleVR5cGUiOiIiLCJ2YWx1ZVR5cGUiOiIiLCJwcmVmaXgiOm51bGx9LCJib3giOnsiZGVzYyI6bnVsbCwia2V5VHlwZSI6IiIsInZhbHVlVHlwZSI6IiIsInByZWZpeCI6bnVsbH19fSwiYmFyZUFjdGlvbnMiOnsiY3JlYXRlIjpbIk5vT3AiXSwiY2FsbCI6W119LCJzb3VyY2VJbmZvIjp7ImFwcHJvdmFsIjp7InNvdXJjZUluZm8iOlt7InBjIjpbMTU1LDE5MiwyMTMsMjM3LDI1OCwyOTQsMzE1LDMzNiwzNjAsMzgxLDQxOCw0NTEsNDcyLDUwNiw1MjcsNTYzLDU4NCw2MzQsNjU0LDY4OSw3MDksNzQ0LDc3NSw4MDYsODI2XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBhcnJheSBsZW5ndGggaGVhZGVyIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjYyXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsQWRkQWxsb3dhbmNlcyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM4OV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBzbWFydF9jb250cmFjdHMvYXJjNTgvZGFvL3R5cGVzLnRzOjpQcm9wb3NhbEFkZE5hbWVkUGx1Z2luIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjY2XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsQWRkUGx1Z2luIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTkyXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsRXhlY3V0ZU5hbWVkUGx1Z2luIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTM1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsRXhlY3V0ZVBsdWdpbiIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6Wzc0OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBzbWFydF9jb250cmFjdHMvYXJjNTgvZGFvL3R5cGVzLnRzOjpQcm9wb3NhbE5ld0VzY3JvdyIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzcxN10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgbnVtYmVyIG9mIGJ5dGVzIGZvciBzbWFydF9jb250cmFjdHMvYXJjNTgvZGFvL3R5cGVzLnRzOjpQcm9wb3NhbFJlbW92ZUFsbG93YW5jZXMiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2MDddLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3Igc21hcnRfY29udHJhY3RzL2FyYzU4L2Rhby90eXBlcy50czo6UHJvcG9zYWxSZW1vdmVFeGVjdXRlUGx1Z2luIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDc3XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsUmVtb3ZlTmFtZWRQbHVnaW4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MjNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3Igc21hcnRfY29udHJhY3RzL2FyYzU4L2Rhby90eXBlcy50czo6UHJvcG9zYWxSZW1vdmVQbHVnaW4iLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls3NzldLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3Igc21hcnRfY29udHJhY3RzL2FyYzU4L2Rhby90eXBlcy50czo6UHJvcG9zYWxUb2dnbGVFc2Nyb3dMb2NrIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbODMxXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCBudW1iZXIgb2YgYnl0ZXMgZm9yIHNtYXJ0X2NvbnRyYWN0cy9hcmM1OC9kYW8vdHlwZXMudHM6OlByb3Bvc2FsVXBkYXRlRmllbGQiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxNjNdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIG51bWJlciBvZiBieXRlcyBmb3Igc21hcnRfY29udHJhY3RzL2FyYzU4L2Rhby90eXBlcy50czo6UHJvcG9zYWxVcGdyYWRlQXBwIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzM2LDc2N10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDAgb2YgKChsZW4rdXRmOFtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2MjZdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAwIG9mICgobGVuK3V0ZjhbXSksKGxlbisodWludDY0LHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wxKVtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls2ODFdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAwIG9mICgobGVuK3V0ZjhbXSksKGxlbit1aW50NjRbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzk4XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMCBvZiAoKGxlbit1dGY4W10pLChsZW4rdWludDhbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNDQzXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMCBvZiAoKGxlbit1dGY4W10pLHVpbnQ2NCx1aW50OFszMl0sKGxlbit1dGY4W10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzI4Nl0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDAgb2YgKChsZW4rdXRmOFtdKSx1aW50NjQsdWludDhbMzJdLChsZW4rdXRmOFtdKSx1aW50OCx1aW50NjQsdWludDY0LChsZW4rKHVpbnQ4WzRdLHVpbnQ2NClbXSksYm9vbDEsYm9vbDEsYm9vbDEsYm9vbDEsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCwobGVuK3V0ZjhbXSksKGxlbisodWludDY0LHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wxKVtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls1NTVdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAwIG9mICgobGVuK3V0ZjhbXSksdWludDhbMzJdLChsZW4rdWludDhbMzJdW10pLHVpbnQ2NCx1aW50NjQpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNjQ2XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMSBvZiAoKGxlbit1dGY4W10pLChsZW4rKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sMSlbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNzAxXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMSBvZiAoKGxlbit1dGY4W10pLChsZW4rdWludDY0W10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzgxOF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDEgb2YgKChsZW4rdXRmOFtdKSwobGVuK3VpbnQ4W10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzQ5OF0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDEgb2YgKHVpbnQ2NCwobGVuK3V0ZjhbXSksdWludDhbMzJdLChsZW4rdWludDhbMzJdW10pLHVpbnQ2NCx1aW50NjQpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjI5XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMTYgb2YgKHVpbnQ2NCx1aW50OFszMl0sKGxlbit1dGY4W10pLHVpbnQ4LHVpbnQ2NCx1aW50NjQsKGxlbisodWludDhbNF0sdWludDY0KVtdKSxib29sMSxib29sMSxib29sMSxib29sMSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LChsZW4rdXRmOFtdKSwobGVuKyh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbDEpW10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM1Ml0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDE3IG9mICgobGVuK3V0ZjhbXSksdWludDY0LHVpbnQ4WzMyXSwobGVuK3V0ZjhbXSksdWludDgsdWludDY0LHVpbnQ2NCwobGVuKyh1aW50OFs0XSx1aW50NjQpW10pLGJvb2wxLGJvb2wxLGJvb2wxLGJvb2wxLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsKGxlbit1dGY4W10pLChsZW4rKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sMSlbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjUwXSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMTcgb2YgKHVpbnQ2NCx1aW50OFszMl0sKGxlbit1dGY4W10pLHVpbnQ4LHVpbnQ2NCx1aW50NjQsKGxlbisodWludDhbNF0sdWludDY0KVtdKSxib29sMSxib29sMSxib29sMSxib29sMSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LChsZW4rdXRmOFtdKSwobGVuKyh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbDEpW10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzM3M10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDE4IG9mICgobGVuK3V0ZjhbXSksdWludDY0LHVpbnQ4WzMyXSwobGVuK3V0ZjhbXSksdWludDgsdWludDY0LHVpbnQ2NCwobGVuKyh1aW50OFs0XSx1aW50NjQpW10pLGJvb2wxLGJvb2wxLGJvb2wxLGJvb2wxLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsKGxlbit1dGY4W10pLChsZW4rKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sMSlbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbNTc2XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMiBvZiAoKGxlbit1dGY4W10pLHVpbnQ4WzMyXSwobGVuK3VpbnQ4WzMyXVtdKSx1aW50NjQsdWludDY0KSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE0N10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDIgb2YgKHVpbnQ2NCx1aW50OFszMl0sKGxlbit1aW50OFszMl1bXSksdWludDY0LHVpbnQ2NCkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0MTBdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAyIG9mICh1aW50NjQsdWludDhbMzJdLChsZW4rdXRmOFtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOlsxODRdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAyIG9mICh1aW50NjQsdWludDhbMzJdLChsZW4rdXRmOFtdKSx1aW50OCx1aW50NjQsdWludDY0LChsZW4rKHVpbnQ4WzRdLHVpbnQ2NClbXSksYm9vbDEsYm9vbDEsYm9vbDEsYm9vbDEsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCwobGVuK3V0ZjhbXSksKGxlbisodWludDY0LHVpbnQ4LHVpbnQ2NCx1aW50NjQsdWludDY0LGJvb2wxKVtdKSkiLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9LHsicGMiOls0NjRdLCJlcnJvck1lc3NhZ2UiOiJpbnZhbGlkIHRhaWwgcG9pbnRlciBhdCBpbmRleCAzIG9mICgobGVuK3V0ZjhbXSksdWludDY0LHVpbnQ4WzMyXSwobGVuK3V0ZjhbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzA3XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggMyBvZiAoKGxlbit1dGY4W10pLHVpbnQ2NCx1aW50OFszMl0sKGxlbit1dGY4W10pLHVpbnQ4LHVpbnQ2NCx1aW50NjQsKGxlbisodWludDhbNF0sdWludDY0KVtdKSxib29sMSxib29sMSxib29sMSxib29sMSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LChsZW4rdXRmOFtdKSwobGVuKyh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbDEpW10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzUxOV0sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdGFpbCBwb2ludGVyIGF0IGluZGV4IDMgb2YgKHVpbnQ2NCwobGVuK3V0ZjhbXSksdWludDhbMzJdLChsZW4rdWludDhbMzJdW10pLHVpbnQ2NCx1aW50NjQpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMjA1XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggNiBvZiAodWludDY0LHVpbnQ4WzMyXSwobGVuK3V0ZjhbXSksdWludDgsdWludDY0LHVpbnQ2NCwobGVuKyh1aW50OFs0XSx1aW50NjQpW10pLGJvb2wxLGJvb2wxLGJvb2wxLGJvb2wxLHVpbnQ2NCx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsKGxlbit1dGY4W10pLChsZW4rKHVpbnQ2NCx1aW50OCx1aW50NjQsdWludDY0LHVpbnQ2NCxib29sMSlbXSkpIiwidGVhbCI6bnVsbCwic291cmNlIjpudWxsfSx7InBjIjpbMzI4XSwiZXJyb3JNZXNzYWdlIjoiaW52YWxpZCB0YWlsIHBvaW50ZXIgYXQgaW5kZXggNyBvZiAoKGxlbit1dGY4W10pLHVpbnQ2NCx1aW50OFszMl0sKGxlbit1dGY4W10pLHVpbnQ4LHVpbnQ2NCx1aW50NjQsKGxlbisodWludDhbNF0sdWludDY0KVtdKSxib29sMSxib29sMSxib29sMSxib29sMSx1aW50NjQsdWludDY0LHVpbnQ2NCx1aW50NjQsdWludDY0LChsZW4rdXRmOFtdKSwobGVuKyh1aW50NjQsdWludDgsdWludDY0LHVpbnQ2NCx1aW50NjQsYm9vbDEpW10pKSIsInRlYWwiOm51bGwsInNvdXJjZSI6bnVsbH0seyJwYyI6WzE0MiwxNzksMjAwLDIyNCwyNDUsMjgxLDMwMiwzMjMsMzQ3LDM2OCw0MDUsNDM4LDQ1OSw0OTMsNTE0LDU1MCw1NzEsNjIyLDY0MSw2NzcsNjk2LDczMiw3NjMsNzk0LDgxM10sImVycm9yTWVzc2FnZSI6ImludmFsaWQgdHVwbGUgZW5jb2RpbmciLCJ0ZWFsIjpudWxsLCJzb3VyY2UiOm51bGx9XSwicGNPZmZzZXRNZXRob2QiOiJub25lIn0sImNsZWFyIjp7InNvdXJjZUluZm8iOltdLCJwY09mZnNldE1ldGhvZCI6Im5vbmUifX0sInNvdXJjZSI6eyJhcHByb3ZhbCI6IkkzQnlZV2R0WVNCMlpYSnphVzl1SURFeENpTndjbUZuYldFZ2RIbHdaWFJ5WVdOcklHWmhiSE5sQ2dvdkx5QkFZV3huYjNKaGJtUm1iM1Z1WkdGMGFXOXVMMkZzWjI5eVlXNWtMWFI1Y0dWelkzSnBjSFF2WVhKak5DOXBibVJsZUM1a0xuUnpPanBEYjI1MGNtRmpkQzVoY0hCeWIzWmhiRkJ5YjJkeVlXMG9LU0F0UGlCMWFXNTBOalE2Q20xaGFXNDZDaUFnSUNCcGJuUmpZbXh2WTJzZ01DQXlJREVnTkFvZ0lDQWdZbmwwWldOaWJHOWpheUF3ZURFMU1XWTNZemMxQ2lBZ0lDQXZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWEpqTlRndlpHRnZMM1I1Y0dWekxtRnNaMjh1ZEhNNk5Bb2dJQ0FnTHk4Z1pYaHdiM0owSUdOc1lYTnpJRUZyYVhSaFJFRlBWSGx3WlhNZ1pYaDBaVzVrY3lCRGIyNTBjbUZqZENCN0NpQWdJQ0IwZUc0Z1RuVnRRWEJ3UVhKbmN3b2dJQ0FnWW5vZ2JXRnBibDlmWDJGc1oyOTBjMTlmTG1SbFptRjFiSFJEY21WaGRHVkFNakFLSUNBZ0lIUjRiaUJQYmtOdmJYQnNaWFJwYjI0S0lDQWdJQ0VLSUNBZ0lHRnpjMlZ5ZEFvZ0lDQWdkSGh1SUVGd2NHeHBZMkYwYVc5dVNVUUtJQ0FnSUdGemMyVnlkQW9nSUNBZ2NIVnphR0o1ZEdWemN5QXdlRGt5WWpNMU9EazJJREI0TjJWbE5HSmhZamNnTUhnNE1tUTFaak5tWmlBd2VHWXpPVFF4WVRKaklEQjRaakZqWmpJeVkyTWdNSGhpWkRSbFpqY3pNQ0F3ZUdWaVlXSTFaVEUwSURCNE5tSTRZV0prTW1ZZ01IaG1ZMkZtT0RReU1DQXdlRFUxWTJVMVkyRTVJREI0WVRZek9HSmxNak1nTUhnMU1HRmhZamd4WkNBd2VERTBPV1F6WTJOaUlDOHZJRzFsZEdodlpDQWljSEp2Y0c5ellXeFZjR2R5WVdSbFFYQndVMmhoY0dVb0tIVnBiblEyTkN4aWVYUmxXek15WFN4aWVYUmxXek15WFZ0ZExIVnBiblEyTkN4MWFXNTBOalFwS1NoMWFXNTBOalFzWW5sMFpWc3pNbDBzWW5sMFpWc3pNbDFiWFN4MWFXNTBOalFzZFdsdWREWTBLU0lzSUcxbGRHaHZaQ0FpY0hKdmNHOXpZV3hCWkdSUWJIVm5hVzVUYUdGd1pTZ29kV2x1ZERZMExHRmtaSEpsYzNNc2MzUnlhVzVuTEhWcGJuUTRMSFZwYm5RMk5DeDFhVzUwTmpRc0tHSjVkR1ZiTkYwc2RXbHVkRFkwS1Z0ZExHSnZiMndzWW05dmJDeGliMjlzTEdKdmIyd3NkV2x1ZERZMExIVnBiblEyTkN4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeHpkSEpwYm1jc0tIVnBiblEyTkN4MWFXNTBPQ3gxYVc1ME5qUXNkV2x1ZERZMExIVnBiblEyTkN4aWIyOXNLVnRkS1Nrb2RXbHVkRFkwTEdGa1pISmxjM01zYzNSeWFXNW5MSFZwYm5RNExIVnBiblEyTkN4MWFXNTBOalFzS0dKNWRHVmJORjBzZFdsdWREWTBLVnRkTEdKdmIyd3NZbTl2YkN4aWIyOXNMR0p2YjJ3c2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNkV2x1ZERZMExIVnBiblEyTkN4emRISnBibWNzS0hWcGJuUTJOQ3gxYVc1ME9DeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3hpYjI5c0tWdGRLU0lzSUcxbGRHaHZaQ0FpY0hKdmNHOXpZV3hCWkdST1lXMWxaRkJzZFdkcGJsTm9ZWEJsS0NoemRISnBibWNzZFdsdWREWTBMR0ZrWkhKbGMzTXNjM1J5YVc1bkxIVnBiblE0TEhWcGJuUTJOQ3gxYVc1ME5qUXNLR0o1ZEdWYk5GMHNkV2x1ZERZMEtWdGRMR0p2YjJ3c1ltOXZiQ3hpYjI5c0xHSnZiMndzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3h6ZEhKcGJtY3NLSFZwYm5RMk5DeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeGliMjlzS1Z0ZEtTa29jM1J5YVc1bkxIVnBiblEyTkN4aFpHUnlaWE56TEhOMGNtbHVaeXgxYVc1ME9DeDFhVzUwTmpRc2RXbHVkRFkwTENoaWVYUmxXelJkTEhWcGJuUTJOQ2xiWFN4aWIyOXNMR0p2YjJ3c1ltOXZiQ3hpYjI5c0xIVnBiblEyTkN4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc2MzUnlhVzVuTENoMWFXNTBOalFzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN4MWFXNTBOalFzWW05dmJDbGJYU2tpTENCdFpYUm9iMlFnSW5CeWIzQnZjMkZzVW1WdGIzWmxVR3gxWjJsdVUyaGhjR1VvS0hWcGJuUTJOQ3hoWkdSeVpYTnpMSE4wY21sdVp5a3BLSFZwYm5RMk5DeGhaR1J5WlhOekxITjBjbWx1WnlraUxDQnRaWFJvYjJRZ0luQnliM0J2YzJGc1VtVnRiM1psVG1GdFpXUlFiSFZuYVc1VGFHRndaU2dvYzNSeWFXNW5MSFZwYm5RMk5DeGhaR1J5WlhOekxITjBjbWx1WnlrcEtITjBjbWx1Wnl4MWFXNTBOalFzWVdSa2NtVnpjeXh6ZEhKcGJtY3BJaXdnYldWMGFHOWtJQ0p3Y205d2IzTmhiRVY0WldOMWRHVlFiSFZuYVc1VGFHRndaU2dvZFdsdWREWTBMSE4wY21sdVp5eGllWFJsV3pNeVhTeGllWFJsV3pNeVhWdGRMSFZwYm5RMk5DeDFhVzUwTmpRcEtTaDFhVzUwTmpRc2MzUnlhVzVuTEdKNWRHVmJNekpkTEdKNWRHVmJNekpkVzEwc2RXbHVkRFkwTEhWcGJuUTJOQ2tpTENCdFpYUm9iMlFnSW5CeWIzQnZjMkZzUlhobFkzVjBaVTVoYldWa1VHeDFaMmx1VTJoaGNHVW9LSE4wY21sdVp5eGllWFJsV3pNeVhTeGllWFJsV3pNeVhWdGRMSFZwYm5RMk5DeDFhVzUwTmpRcEtTaHpkSEpwYm1jc1lubDBaVnN6TWwwc1lubDBaVnN6TWwxYlhTeDFhVzUwTmpRc2RXbHVkRFkwS1NJc0lHMWxkR2h2WkNBaWNISnZjRzl6WVd4U1pXMXZkbVZGZUdWamRYUmxVR3gxWjJsdVUyaGhjR1VvS0dKNWRHVmJNekpkS1Nrb1lubDBaVnN6TWwwcElpd2diV1YwYUc5a0lDSndjbTl3YjNOaGJFRmtaRUZzYkc5M1lXNWpaWE5UYUdGd1pTZ29jM1J5YVc1bkxDaDFhVzUwTmpRc2RXbHVkRGdzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc1ltOXZiQ2xiWFNrcEtITjBjbWx1Wnl3b2RXbHVkRFkwTEhWcGJuUTRMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEdKdmIyd3BXMTBwSWl3Z2JXVjBhRzlrSUNKd2NtOXdiM05oYkZKbGJXOTJaVUZzYkc5M1lXNWpaWE5UYUdGd1pTZ29jM1J5YVc1bkxIVnBiblEyTkZ0ZEtTa29jM1J5YVc1bkxIVnBiblEyTkZ0ZEtTSXNJRzFsZEdodlpDQWljSEp2Y0c5ellXeE9aWGRGYzJOeWIzZFRhR0Z3WlNnb2MzUnlhVzVuS1Nrb2MzUnlhVzVuS1NJc0lHMWxkR2h2WkNBaWNISnZjRzl6WVd4VWIyZG5iR1ZGYzJOeWIzZE1iMk5yVTJoaGNHVW9LSE4wY21sdVp5a3BLSE4wY21sdVp5a2lMQ0J0WlhSb2IyUWdJbkJ5YjNCdmMyRnNWWEJrWVhSbFJtbGxiR1JUYUdGd1pTZ29jM1J5YVc1bkxHSjVkR1ZiWFNrcEtITjBjbWx1Wnl4aWVYUmxXMTBwSWdvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTUFvZ0lDQWdiV0YwWTJnZ2NISnZjRzl6WVd4VmNHZHlZV1JsUVhCd1UyaGhjR1VnY0hKdmNHOXpZV3hCWkdSUWJIVm5hVzVUYUdGd1pTQndjbTl3YjNOaGJFRmtaRTVoYldWa1VHeDFaMmx1VTJoaGNHVWdjSEp2Y0c5ellXeFNaVzF2ZG1WUWJIVm5hVzVUYUdGd1pTQndjbTl3YjNOaGJGSmxiVzkyWlU1aGJXVmtVR3gxWjJsdVUyaGhjR1VnY0hKdmNHOXpZV3hGZUdWamRYUmxVR3gxWjJsdVUyaGhjR1VnY0hKdmNHOXpZV3hGZUdWamRYUmxUbUZ0WldSUWJIVm5hVzVUYUdGd1pTQndjbTl3YjNOaGJGSmxiVzkyWlVWNFpXTjFkR1ZRYkhWbmFXNVRhR0Z3WlNCd2NtOXdiM05oYkVGa1pFRnNiRzkzWVc1alpYTlRhR0Z3WlNCd2NtOXdiM05oYkZKbGJXOTJaVUZzYkc5M1lXNWpaWE5UYUdGd1pTQndjbTl3YjNOaGJFNWxkMFZ6WTNKdmQxTm9ZWEJsSUhCeWIzQnZjMkZzVkc5bloyeGxSWE5qY205M1RHOWphMU5vWVhCbElIQnliM0J2YzJGc1ZYQmtZWFJsUm1sbGJHUlRhR0Z3WlFvZ0lDQWdaWEp5Q2dwdFlXbHVYMTlmWVd4bmIzUnpYMTh1WkdWbVlYVnNkRU55WldGMFpVQXlNRG9LSUNBZ0lDOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhjbU0xT0M5a1lXOHZkSGx3WlhNdVlXeG5ieTUwY3pvMENpQWdJQ0F2THlCbGVIQnZjblFnWTJ4aGMzTWdRV3RwZEdGRVFVOVVlWEJsY3lCbGVIUmxibVJ6SUVOdmJuUnlZV04wSUhzS0lDQWdJSFI0YmlCUGJrTnZiWEJzWlhScGIyNEtJQ0FnSUNFS0lDQWdJSFI0YmlCQmNIQnNhV05oZEdsdmJrbEVDaUFnSUNBaENpQWdJQ0FtSmdvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qcEJhMmwwWVVSQlQxUjVjR1Z6TG5CeWIzQnZjMkZzVlhCbmNtRmtaVUZ3Y0ZOb1lYQmxXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LY0hKdmNHOXpZV3hWY0dkeVlXUmxRWEJ3VTJoaGNHVTZDaUFnSUNBdkx5QnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYSmpOVGd2WkdGdkwzUjVjR1Z6TG1Gc1oyOHVkSE02TlFvZ0lDQWdMeThnUUdGaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdaR2xuSURFS0lDQWdJSEIxYzJocGJuUWdOREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lIUjFjR3hsSUdWdVkyOWthVzVuQ2lBZ0lDQmtkWEFLSUNBZ0lIQjFjMmhwYm5RZ05UZ0tJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ01pQnZaaUFvZFdsdWREWTBMSFZwYm5RNFd6TXlYU3dvYkdWdUszVnBiblE0V3pNeVhWdGRLU3gxYVc1ME5qUXNkV2x1ZERZMEtRb2dJQ0FnWkdsbklESUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXlDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2NIVnphR2x1ZENBek1nb2dJQ0FnS2dvZ0lDQWdjSFZ6YUdsdWRDQTJNQW9nSUNBZ0t3b2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NTBjem82VUhKdmNHOXpZV3hWY0dkeVlXUmxRWEJ3Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRnlZelU0TDJSaGJ5OTBlWEJsY3k1aGJHZHZMblJ6T2pwQmEybDBZVVJCVDFSNWNHVnpMbkJ5YjNCdmMyRnNRV1JrVUd4MVoybHVVMmhoY0dWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncHdjbTl3YjNOaGJFRmtaRkJzZFdkcGJsTm9ZWEJsT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qRXdDaUFnSUNBdkx5QkFZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCa2FXY2dNUW9nSUNBZ2NIVnphR2x1ZENBME1Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnY0hWemFHbHVkQ0F4TURZS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQjBZV2xzSUhCdmFXNTBaWElnWVhRZ2FXNWtaWGdnTWlCdlppQW9kV2x1ZERZMExIVnBiblE0V3pNeVhTd29iR1Z1SzNWMFpqaGJYU2tzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN3b2JHVnVLeWgxYVc1ME9GczBYU3gxYVc1ME5qUXBXMTBwTEdKdmIyd3hMR0p2YjJ3eExHSnZiMnd4TEdKdmIyd3hMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNLR3hsYml0MWRHWTRXMTBwTENoc1pXNHJLSFZwYm5RMk5DeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeGliMjlzTVNsYlhTa3BDaUFnSUNCa2FXY2dNZ29nSUNBZ2MzZGhjQW9nSUNBZ1pHbG5JRElLSUNBZ0lITjFZbk4wY21sdVp6TUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElERXdPQW9nSUNBZ0t3b2dJQ0FnWkdsbklESUtJQ0FnSUhCMWMyaHBiblFnTlRrS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJSFIxY0d4bElHVnVZMjlrYVc1bkNpQWdJQ0JrZFhBS0lDQWdJR1JwWnlBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdkR0ZwYkNCd2IybHVkR1Z5SUdGMElHbHVaR1Y0SURZZ2IyWWdLSFZwYm5RMk5DeDFhVzUwT0Zzek1sMHNLR3hsYml0MWRHWTRXMTBwTEhWcGJuUTRMSFZwYm5RMk5DeDFhVzUwTmpRc0tHeGxiaXNvZFdsdWREaGJORjBzZFdsdWREWTBLVnRkS1N4aWIyOXNNU3hpYjI5c01TeGliMjlzTVN4aWIyOXNNU3gxYVc1ME5qUXNkV2x1ZERZMExIVnBiblEyTkN4MWFXNTBOalFzZFdsdWREWTBMQ2hzWlc0cmRYUm1PRnRkS1N3b2JHVnVLeWgxYVc1ME5qUXNkV2x1ZERnc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNZbTl2YkRFcFcxMHBLUW9nSUNBZ1pHbG5JRE1LSUNBZ0lITjNZWEFLSUNBZ0lHUnBaeUF6Q2lBZ0lDQnpkV0p6ZEhKcGJtY3pDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdjSFZ6YUdsdWRDQXhNZ29nSUNBZ0tnb2dJQ0FnYVc1MFkxOHhJQzh2SURJS0lDQWdJQ3NLSUNBZ0lDc0tJQ0FnSUdScFp5QXlDaUFnSUNCd2RYTm9hVzUwSURFd01nb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnWkdsbklESUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ01UWWdiMllnS0hWcGJuUTJOQ3gxYVc1ME9Gc3pNbDBzS0d4bGJpdDFkR1k0VzEwcExIVnBiblE0TEhWcGJuUTJOQ3gxYVc1ME5qUXNLR3hsYmlzb2RXbHVkRGhiTkYwc2RXbHVkRFkwS1Z0ZEtTeGliMjlzTVN4aWIyOXNNU3hpYjI5c01TeGliMjlzTVN4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTENoc1pXNHJkWFJtT0Z0ZEtTd29iR1Z1S3loMWFXNTBOalFzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN4MWFXNTBOalFzWW05dmJERXBXMTBwS1FvZ0lDQWdaR2xuSURNS0lDQWdJSE4zWVhBS0lDQWdJR1JwWnlBekNpQWdJQ0J6ZFdKemRISnBibWN6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnYVc1MFkxOHhJQzh2SURJS0lDQWdJQ3NLSUNBZ0lDc0tJQ0FnSUdScFp5QXlDaUFnSUNCd2RYTm9hVzUwSURFd05Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnWkdsbklESUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ01UY2diMllnS0hWcGJuUTJOQ3gxYVc1ME9Gc3pNbDBzS0d4bGJpdDFkR1k0VzEwcExIVnBiblE0TEhWcGJuUTJOQ3gxYVc1ME5qUXNLR3hsYmlzb2RXbHVkRGhiTkYwc2RXbHVkRFkwS1Z0ZEtTeGliMjlzTVN4aWIyOXNNU3hpYjI5c01TeGliMjlzTVN4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTENoc1pXNHJkWFJtT0Z0ZEtTd29iR1Z1S3loMWFXNTBOalFzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN4MWFXNTBOalFzWW05dmJERXBXMTBwS1FvZ0lDQWdaR2xuSURNS0lDQWdJSE4zWVhBS0lDQWdJR1JwWnlBekNpQWdJQ0J6ZFdKemRISnBibWN6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnY0hWemFHbHVkQ0F6TkFvZ0lDQWdLZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNzS0lDQWdJQ3NLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlITnRZWEowWDJOdmJuUnlZV04wY3k5aGNtTTFPQzlrWVc4dmRIbHdaWE11ZEhNNk9sQnliM0J2YzJGc1FXUmtVR3gxWjJsdUNpQWdJQ0JpZVhSbFkxOHdJQzh2SURCNE1UVXhaamRqTnpVS0lDQWdJSE4zWVhBS0lDQWdJR052Ym1OaGRBb2dJQ0FnYkc5bkNpQWdJQ0JwYm5Salh6SWdMeThnTVFvZ0lDQWdjbVYwZFhKdUNnb0tMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qcEJhMmwwWVVSQlQxUjVjR1Z6TG5CeWIzQnZjMkZzUVdSa1RtRnRaV1JRYkhWbmFXNVRhR0Z3WlZ0eWIzVjBhVzVuWFNncElDMCtJSFp2YVdRNkNuQnliM0J2YzJGc1FXUmtUbUZ0WldSUWJIVm5hVzVUYUdGd1pUb0tJQ0FnSUM4dklITnRZWEowWDJOdmJuUnlZV04wY3k5aGNtTTFPQzlrWVc4dmRIbHdaWE11WVd4bmJ5NTBjem94TlFvZ0lDQWdMeThnUUdGaWFXMWxkR2h2WkNoN0lISmxZV1J2Ym14NU9pQjBjblZsSUgwcENpQWdJQ0IwZUc1aElFRndjR3hwWTJGMGFXOXVRWEpuY3lBeENpQWdJQ0JrZFhBS0lDQWdJR3hsYmdvZ0lDQWdaR2xuSURFS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmxlSFJ5WVdOMFgzVnBiblF4TmlBdkx5QnZiaUJsY25KdmNqb2dhVzUyWVd4cFpDQjBkWEJzWlNCbGJtTnZaR2x1WndvZ0lDQWdaSFZ3Q2lBZ0lDQndkWE5vYVc1MElERXdPQW9nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUhSaGFXd2djRzlwYm5SbGNpQmhkQ0JwYm1SbGVDQXdJRzltSUNnb2JHVnVLM1YwWmpoYlhTa3NkV2x1ZERZMExIVnBiblE0V3pNeVhTd29iR1Z1SzNWMFpqaGJYU2tzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN3b2JHVnVLeWgxYVc1ME9GczBYU3gxYVc1ME5qUXBXMTBwTEdKdmIyd3hMR0p2YjJ3eExHSnZiMnd4TEdKdmIyd3hMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNLR3hsYml0MWRHWTRXMTBwTENoc1pXNHJLSFZwYm5RMk5DeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeGliMjlzTVNsYlhTa3BDaUFnSUNCa2FXY2dNZ29nSUNBZ2MzZGhjQW9nSUNBZ1pHbG5JRElLSUNBZ0lITjFZbk4wY21sdVp6TUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElERXhNQW9nSUNBZ0t3b2dJQ0FnWkdsbklESUtJQ0FnSUhCMWMyaHBiblFnTkRJS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJSFIxY0d4bElHVnVZMjlrYVc1bkNpQWdJQ0JrZFhBS0lDQWdJR1JwWnlBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdkR0ZwYkNCd2IybHVkR1Z5SUdGMElHbHVaR1Y0SURNZ2IyWWdLQ2hzWlc0cmRYUm1PRnRkS1N4MWFXNTBOalFzZFdsdWREaGJNekpkTENoc1pXNHJkWFJtT0Z0ZEtTeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMQ2hzWlc0cktIVnBiblE0V3pSZExIVnBiblEyTkNsYlhTa3NZbTl2YkRFc1ltOXZiREVzWW05dmJERXNZbTl2YkRFc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNkV2x1ZERZMExIVnBiblEyTkN3b2JHVnVLM1YwWmpoYlhTa3NLR3hsYmlzb2RXbHVkRFkwTEhWcGJuUTRMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEdKdmIyd3hLVnRkS1NrS0lDQWdJR1JwWnlBekNpQWdJQ0J6ZDJGd0NpQWdJQ0JrYVdjZ013b2dJQ0FnYzNWaWMzUnlhVzVuTXdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQXJDaUFnSUNCa2FXY2dNZ29nSUNBZ2NIVnphR2x1ZENBMk1Rb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnWkdsbklESUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ055QnZaaUFvS0d4bGJpdDFkR1k0VzEwcExIVnBiblEyTkN4MWFXNTBPRnN6TWwwc0tHeGxiaXQxZEdZNFcxMHBMSFZwYm5RNExIVnBiblEyTkN4MWFXNTBOalFzS0d4bGJpc29kV2x1ZERoYk5GMHNkV2x1ZERZMEtWdGRLU3hpYjI5c01TeGliMjlzTVN4aWIyOXNNU3hpYjI5c01TeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNkV2x1ZERZMExDaHNaVzRyZFhSbU9GdGRLU3dvYkdWdUt5aDFhVzUwTmpRc2RXbHVkRGdzZFdsdWREWTBMSFZwYm5RMk5DeDFhVzUwTmpRc1ltOXZiREVwVzEwcEtRb2dJQ0FnWkdsbklETUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXpDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2NIVnphR2x1ZENBeE1nb2dJQ0FnS2dvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lDc0tJQ0FnSUNzS0lDQWdJR1JwWnlBeUNpQWdJQ0J3ZFhOb2FXNTBJREV3TkFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdkSFZ3YkdVZ1pXNWpiMlJwYm1jS0lDQWdJR1IxY0FvZ0lDQWdaR2xuSURJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQjBZV2xzSUhCdmFXNTBaWElnWVhRZ2FXNWtaWGdnTVRjZ2IyWWdLQ2hzWlc0cmRYUm1PRnRkS1N4MWFXNTBOalFzZFdsdWREaGJNekpkTENoc1pXNHJkWFJtT0Z0ZEtTeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMQ2hzWlc0cktIVnBiblE0V3pSZExIVnBiblEyTkNsYlhTa3NZbTl2YkRFc1ltOXZiREVzWW05dmJERXNZbTl2YkRFc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNkV2x1ZERZMExIVnBiblEyTkN3b2JHVnVLM1YwWmpoYlhTa3NLR3hsYmlzb2RXbHVkRFkwTEhWcGJuUTRMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEdKdmIyd3hLVnRkS1NrS0lDQWdJR1JwWnlBekNpQWdJQ0J6ZDJGd0NpQWdJQ0JrYVdjZ013b2dJQ0FnYzNWaWMzUnlhVzVuTXdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQXJDaUFnSUNCa2FXY2dNZ29nSUNBZ2NIVnphR2x1ZENBeE1EWUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUhSMWNHeGxJR1Z1WTI5a2FXNW5DaUFnSUNCa2RYQUtJQ0FnSUdScFp5QXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnZEdGcGJDQndiMmx1ZEdWeUlHRjBJR2x1WkdWNElERTRJRzltSUNnb2JHVnVLM1YwWmpoYlhTa3NkV2x1ZERZMExIVnBiblE0V3pNeVhTd29iR1Z1SzNWMFpqaGJYU2tzZFdsdWREZ3NkV2x1ZERZMExIVnBiblEyTkN3b2JHVnVLeWgxYVc1ME9GczBYU3gxYVc1ME5qUXBXMTBwTEdKdmIyd3hMR0p2YjJ3eExHSnZiMnd4TEdKdmIyd3hMSFZwYm5RMk5DeDFhVzUwTmpRc2RXbHVkRFkwTEhWcGJuUTJOQ3gxYVc1ME5qUXNLR3hsYml0MWRHWTRXMTBwTENoc1pXNHJLSFZwYm5RMk5DeDFhVzUwT0N4MWFXNTBOalFzZFdsdWREWTBMSFZwYm5RMk5DeGliMjlzTVNsYlhTa3BDaUFnSUNCa2FXY2dNd29nSUNBZ2MzZGhjQW9nSUNBZ1pHbG5JRE1LSUNBZ0lITjFZbk4wY21sdVp6TUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElETTBDaUFnSUNBcUNpQWdJQ0JwYm5Salh6RWdMeThnTWdvZ0lDQWdLd29nSUNBZ0t3b2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJRzUxYldKbGNpQnZaaUJpZVhSbGN5Qm1iM0lnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NTBjem82VUhKdmNHOXpZV3hCWkdST1lXMWxaRkJzZFdkcGJnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6bzZRV3RwZEdGRVFVOVVlWEJsY3k1d2NtOXdiM05oYkZKbGJXOTJaVkJzZFdkcGJsTm9ZWEJsVzNKdmRYUnBibWRkS0NrZ0xUNGdkbTlwWkRvS2NISnZjRzl6WVd4U1pXMXZkbVZRYkhWbmFXNVRhR0Z3WlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6b3lNQW9nSUNBZ0x5OGdRR0ZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ1pHbG5JREVLSUNBZ0lIQjFjMmhwYm5RZ05EQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUhSMWNHeGxJR1Z1WTI5a2FXNW5DaUFnSUNCa2RYQUtJQ0FnSUhCMWMyaHBiblFnTkRJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQjBZV2xzSUhCdmFXNTBaWElnWVhRZ2FXNWtaWGdnTWlCdlppQW9kV2x1ZERZMExIVnBiblE0V3pNeVhTd29iR1Z1SzNWMFpqaGJYU2twQ2lBZ0lDQmthV2NnTWdvZ0lDQWdjM2RoY0FvZ0lDQWdaR2xuSURJS0lDQWdJSE4xWW5OMGNtbHVaek1LSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCaGNuSmhlU0JzWlc1bmRHZ2dhR1ZoWkdWeUNpQWdJQ0J3ZFhOb2FXNTBJRFEwQ2lBZ0lDQXJDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMblJ6T2pwUWNtOXdiM05oYkZKbGJXOTJaVkJzZFdkcGJnb2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6bzZRV3RwZEdGRVFVOVVlWEJsY3k1d2NtOXdiM05oYkZKbGJXOTJaVTVoYldWa1VHeDFaMmx1VTJoaGNHVmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3B3Y205d2IzTmhiRkpsYlc5MlpVNWhiV1ZrVUd4MVoybHVVMmhoY0dVNkNpQWdJQ0F2THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMbUZzWjI4dWRITTZNalVLSUNBZ0lDOHZJRUJoWW1sdFpYUm9iMlFvZXlCeVpXRmtiMjVzZVRvZ2RISjFaU0I5S1FvZ0lDQWdkSGh1WVNCQmNIQnNhV05oZEdsdmJrRnlaM01nTVFvZ0lDQWdaSFZ3Q2lBZ0lDQnNaVzRLSUNBZ0lHUnBaeUF4Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ2RIVndiR1VnWlc1amIyUnBibWNLSUNBZ0lHUjFjQW9nSUNBZ2NIVnphR2x1ZENBME5Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJSFJoYVd3Z2NHOXBiblJsY2lCaGRDQnBibVJsZUNBd0lHOW1JQ2dvYkdWdUszVjBaamhiWFNrc2RXbHVkRFkwTEhWcGJuUTRXek15WFN3b2JHVnVLM1YwWmpoYlhTa3BDaUFnSUNCa2FXY2dNZ29nSUNBZ2MzZGhjQW9nSUNBZ1pHbG5JRElLSUNBZ0lITjFZbk4wY21sdVp6TUtJQ0FnSUdsdWRHTmZNQ0F2THlBd0NpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0JoY25KaGVTQnNaVzVuZEdnZ2FHVmhaR1Z5Q2lBZ0lDQndkWE5vYVc1MElEUTJDaUFnSUNBckNpQWdJQ0JrYVdjZ01nb2dJQ0FnY0hWemFHbHVkQ0EwTWdvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdkSFZ3YkdVZ1pXNWpiMlJwYm1jS0lDQWdJR1IxY0FvZ0lDQWdaR2xuSURJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQjBZV2xzSUhCdmFXNTBaWElnWVhRZ2FXNWtaWGdnTXlCdlppQW9LR3hsYml0MWRHWTRXMTBwTEhWcGJuUTJOQ3gxYVc1ME9Gc3pNbDBzS0d4bGJpdDFkR1k0VzEwcEtRb2dJQ0FnWkdsbklETUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXpDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNzS0lDQWdJQ3NLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlITnRZWEowWDJOdmJuUnlZV04wY3k5aGNtTTFPQzlrWVc4dmRIbHdaWE11ZEhNNk9sQnliM0J2YzJGc1VtVnRiM1psVG1GdFpXUlFiSFZuYVc0S0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1pQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWEpqTlRndlpHRnZMM1I1Y0dWekxtRnNaMjh1ZEhNNk9rRnJhWFJoUkVGUFZIbHdaWE11Y0hKdmNHOXpZV3hGZUdWamRYUmxVR3gxWjJsdVUyaGhjR1ZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwd2NtOXdiM05oYkVWNFpXTjFkR1ZRYkhWbmFXNVRhR0Z3WlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6b3pNQW9nSUNBZ0x5OGdRR0ZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ1pHbG5JREVLSUNBZ0lIQjFjMmhwYm5RZ09Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnY0hWemFHbHVkQ0EyTUFvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lIUmhhV3dnY0c5cGJuUmxjaUJoZENCcGJtUmxlQ0F4SUc5bUlDaDFhVzUwTmpRc0tHeGxiaXQxZEdZNFcxMHBMSFZwYm5RNFd6TXlYU3dvYkdWdUszVnBiblE0V3pNeVhWdGRLU3gxYVc1ME5qUXNkV2x1ZERZMEtRb2dJQ0FnWkdsbklESUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXlDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2NIVnphR2x1ZENBMk1nb2dJQ0FnS3dvZ0lDQWdaR2xuSURJS0lDQWdJSEIxYzJocGJuUWdORElLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lIUjFjR3hsSUdWdVkyOWthVzVuQ2lBZ0lDQmtkWEFLSUNBZ0lHUnBaeUF5Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2RHRnBiQ0J3YjJsdWRHVnlJR0YwSUdsdVpHVjRJRE1nYjJZZ0tIVnBiblEyTkN3b2JHVnVLM1YwWmpoYlhTa3NkV2x1ZERoYk16SmRMQ2hzWlc0cmRXbHVkRGhiTXpKZFcxMHBMSFZwYm5RMk5DeDFhVzUwTmpRcENpQWdJQ0JrYVdjZ013b2dJQ0FnYzNkaGNBb2dJQ0FnWkdsbklETUtJQ0FnSUhOMVluTjBjbWx1WnpNS0lDQWdJR2x1ZEdOZk1DQXZMeUF3Q2lBZ0lDQmxlSFJ5WVdOMFgzVnBiblF4TmlBdkx5QnZiaUJsY25KdmNqb2dhVzUyWVd4cFpDQmhjbkpoZVNCc1pXNW5kR2dnYUdWaFpHVnlDaUFnSUNCd2RYTm9hVzUwSURNeUNpQWdJQ0FxQ2lBZ0lDQnBiblJqWHpFZ0x5OGdNZ29nSUNBZ0t3b2dJQ0FnS3dvZ0lDQWdQVDBLSUNBZ0lHRnpjMlZ5ZENBdkx5QnBiblpoYkdsa0lHNTFiV0psY2lCdlppQmllWFJsY3lCbWIzSWdjMjFoY25SZlkyOXVkSEpoWTNSekwyRnlZelU0TDJSaGJ5OTBlWEJsY3k1MGN6bzZVSEp2Y0c5ellXeEZlR1ZqZFhSbFVHeDFaMmx1Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRnlZelU0TDJSaGJ5OTBlWEJsY3k1aGJHZHZMblJ6T2pwQmEybDBZVVJCVDFSNWNHVnpMbkJ5YjNCdmMyRnNSWGhsWTNWMFpVNWhiV1ZrVUd4MVoybHVVMmhoY0dWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncHdjbTl3YjNOaGJFVjRaV04xZEdWT1lXMWxaRkJzZFdkcGJsTm9ZWEJsT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qTTFDaUFnSUNBdkx5QkFZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCa2FXY2dNUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUhSMWNHeGxJR1Z1WTI5a2FXNW5DaUFnSUNCa2RYQUtJQ0FnSUhCMWMyaHBiblFnTlRJS0lDQWdJRDA5Q2lBZ0lDQmhjM05sY25RZ0x5OGdhVzUyWVd4cFpDQjBZV2xzSUhCdmFXNTBaWElnWVhRZ2FXNWtaWGdnTUNCdlppQW9LR3hsYml0MWRHWTRXMTBwTEhWcGJuUTRXek15WFN3b2JHVnVLM1ZwYm5RNFd6TXlYVnRkS1N4MWFXNTBOalFzZFdsdWREWTBLUW9nSUNBZ1pHbG5JRElLSUNBZ0lITjNZWEFLSUNBZ0lHUnBaeUF5Q2lBZ0lDQnpkV0p6ZEhKcGJtY3pDaUFnSUNCcGJuUmpYekFnTHk4Z01Bb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnWVhKeVlYa2diR1Z1WjNSb0lHaGxZV1JsY2dvZ0lDQWdjSFZ6YUdsdWRDQTFOQW9nSUNBZ0t3b2dJQ0FnWkdsbklESUtJQ0FnSUhCMWMyaHBiblFnTXpRS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJSFIxY0d4bElHVnVZMjlrYVc1bkNpQWdJQ0JrZFhBS0lDQWdJR1JwWnlBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdkR0ZwYkNCd2IybHVkR1Z5SUdGMElHbHVaR1Y0SURJZ2IyWWdLQ2hzWlc0cmRYUm1PRnRkS1N4MWFXNTBPRnN6TWwwc0tHeGxiaXQxYVc1ME9Gc3pNbDFiWFNrc2RXbHVkRFkwTEhWcGJuUTJOQ2tLSUNBZ0lHUnBaeUF6Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTXdvZ0lDQWdjM1ZpYzNSeWFXNW5Nd29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUdGeWNtRjVJR3hsYm1kMGFDQm9aV0ZrWlhJS0lDQWdJSEIxYzJocGJuUWdNeklLSUNBZ0lDb0tJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQXJDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMblJ6T2pwUWNtOXdiM05oYkVWNFpXTjFkR1ZPWVcxbFpGQnNkV2RwYmdvZ0lDQWdZbmwwWldOZk1DQXZMeUF3ZURFMU1XWTNZemMxQ2lBZ0lDQnpkMkZ3Q2lBZ0lDQmpiMjVqWVhRS0lDQWdJR3h2WndvZ0lDQWdhVzUwWTE4eUlDOHZJREVLSUNBZ0lISmxkSFZ5YmdvS0NpOHZJSE50WVhKMFgyTnZiblJ5WVdOMGN5OWhjbU0xT0M5a1lXOHZkSGx3WlhNdVlXeG5ieTUwY3pvNlFXdHBkR0ZFUVU5VWVYQmxjeTV3Y205d2IzTmhiRkpsYlc5MlpVVjRaV04xZEdWUWJIVm5hVzVUYUdGd1pWdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbkJ5YjNCdmMyRnNVbVZ0YjNabFJYaGxZM1YwWlZCc2RXZHBibE5vWVhCbE9nb2dJQ0FnTHk4Z2MyMWhjblJmWTI5dWRISmhZM1J6TDJGeVl6VTRMMlJoYnk5MGVYQmxjeTVoYkdkdkxuUnpPalF3Q2lBZ0lDQXZMeUJBWVdKcGJXVjBhRzlrS0hzZ2NtVmhaRzl1YkhrNklIUnlkV1VnZlNrS0lDQWdJSFI0Ym1FZ1FYQndiR2xqWVhScGIyNUJjbWR6SURFS0lDQWdJR1IxY0FvZ0lDQWdiR1Z1Q2lBZ0lDQndkWE5vYVc1MElETXlDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMblJ6T2pwUWNtOXdiM05oYkZKbGJXOTJaVVY0WldOMWRHVlFiSFZuYVc0S0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1pQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWEpqTlRndlpHRnZMM1I1Y0dWekxtRnNaMjh1ZEhNNk9rRnJhWFJoUkVGUFZIbHdaWE11Y0hKdmNHOXpZV3hCWkdSQmJHeHZkMkZ1WTJWelUyaGhjR1ZiY205MWRHbHVaMTBvS1NBdFBpQjJiMmxrT2dwd2NtOXdiM05oYkVGa1pFRnNiRzkzWVc1alpYTlRhR0Z3WlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6bzBOUW9nSUNBZ0x5OGdRR0ZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ1pHbG5JREVLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCMGRYQnNaU0JsYm1OdlpHbHVad29nSUNBZ1pIVndDaUFnSUNCcGJuUmpYek1nTHk4Z05Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJSFJoYVd3Z2NHOXBiblJsY2lCaGRDQnBibVJsZUNBd0lHOW1JQ2dvYkdWdUszVjBaamhiWFNrc0tHeGxiaXNvZFdsdWREWTBMSFZwYm5RNExIVnBiblEyTkN4MWFXNTBOalFzZFdsdWREWTBMR0p2YjJ3eEtWdGRLU2tLSUNBZ0lHUnBaeUF5Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTWdvZ0lDQWdjM1ZpYzNSeWFXNW5Nd29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUdGeWNtRjVJR3hsYm1kMGFDQm9aV0ZrWlhJS0lDQWdJSEIxYzJocGJuUWdOZ29nSUNBZ0t3b2dJQ0FnWkdsbklESUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0JsZUhSeVlXTjBYM1ZwYm5ReE5pQXZMeUJ2YmlCbGNuSnZjam9nYVc1MllXeHBaQ0IwZFhCc1pTQmxibU52WkdsdVp3b2dJQ0FnWkhWd0NpQWdJQ0JrYVdjZ01nb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJSFJoYVd3Z2NHOXBiblJsY2lCaGRDQnBibVJsZUNBeElHOW1JQ2dvYkdWdUszVjBaamhiWFNrc0tHeGxiaXNvZFdsdWREWTBMSFZwYm5RNExIVnBiblEyTkN4MWFXNTBOalFzZFdsdWREWTBMR0p2YjJ3eEtWdGRLU2tLSUNBZ0lHUnBaeUF6Q2lBZ0lDQnpkMkZ3Q2lBZ0lDQmthV2NnTXdvZ0lDQWdjM1ZpYzNSeWFXNW5Nd29nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUdGeWNtRjVJR3hsYm1kMGFDQm9aV0ZrWlhJS0lDQWdJSEIxYzJocGJuUWdNelFLSUNBZ0lDb0tJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0FyQ2lBZ0lDQXJDaUFnSUNBOVBRb2dJQ0FnWVhOelpYSjBJQzh2SUdsdWRtRnNhV1FnYm5WdFltVnlJRzltSUdKNWRHVnpJR1p2Y2lCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMblJ6T2pwUWNtOXdiM05oYkVGa1pFRnNiRzkzWVc1alpYTUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNaUF2THlBeENpQWdJQ0J5WlhSMWNtNEtDZ292THlCemJXRnlkRjlqYjI1MGNtRmpkSE12WVhKak5UZ3ZaR0Z2TDNSNWNHVnpMbUZzWjI4dWRITTZPa0ZyYVhSaFJFRlBWSGx3WlhNdWNISnZjRzl6WVd4U1pXMXZkbVZCYkd4dmQyRnVZMlZ6VTJoaGNHVmJjbTkxZEdsdVoxMG9LU0F0UGlCMmIybGtPZ3B3Y205d2IzTmhiRkpsYlc5MlpVRnNiRzkzWVc1alpYTlRhR0Z3WlRvS0lDQWdJQzh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6bzFNQW9nSUNBZ0x5OGdRR0ZpYVcxbGRHaHZaQ2g3SUhKbFlXUnZibXg1T2lCMGNuVmxJSDBwQ2lBZ0lDQjBlRzVoSUVGd2NHeHBZMkYwYVc5dVFYSm5jeUF4Q2lBZ0lDQmtkWEFLSUNBZ0lHeGxiZ29nSUNBZ1pHbG5JREVLSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCMGRYQnNaU0JsYm1OdlpHbHVad29nSUNBZ1pIVndDaUFnSUNCcGJuUmpYek1nTHk4Z05Bb2dJQ0FnUFQwS0lDQWdJR0Z6YzJWeWRDQXZMeUJwYm5aaGJHbGtJSFJoYVd3Z2NHOXBiblJsY2lCaGRDQnBibVJsZUNBd0lHOW1JQ2dvYkdWdUszVjBaamhiWFNrc0tHeGxiaXQxYVc1ME5qUmJYU2twQ2lBZ0lDQmthV2NnTWdvZ0lDQWdjM2RoY0FvZ0lDQWdaR2xuSURJS0lDQWdJSE4xWW5OMGNtbHVaek1LSUNBZ0lHbHVkR05mTUNBdkx5QXdDaUFnSUNCbGVIUnlZV04wWDNWcGJuUXhOaUF2THlCdmJpQmxjbkp2Y2pvZ2FXNTJZV3hwWkNCaGNuSmhlU0JzWlc1bmRHZ2dhR1ZoWkdWeUNpQWdJQ0J3ZFhOb2FXNTBJRFlLSUNBZ0lDc0tJQ0FnSUdScFp5QXlDaUFnSUNCcGJuUmpYekVnTHk4Z01nb2dJQ0FnWlhoMGNtRmpkRjkxYVc1ME1UWWdMeThnYjI0Z1pYSnliM0k2SUdsdWRtRnNhV1FnZEhWd2JHVWdaVzVqYjJScGJtY0tJQ0FnSUdSMWNBb2dJQ0FnWkdsbklESUtJQ0FnSUQwOUNpQWdJQ0JoYzNObGNuUWdMeThnYVc1MllXeHBaQ0IwWVdsc0lIQnZhVzUwWlhJZ1lYUWdhVzVrWlhnZ01TQnZaaUFvS0d4bGJpdDFkR1k0VzEwcExDaHNaVzRyZFdsdWREWTBXMTBwS1FvZ0lDQWdaR2xuSURNS0lDQWdJSE4zWVhBS0lDQWdJR1JwWnlBekNpQWdJQ0J6ZFdKemRISnBibWN6Q2lBZ0lDQnBiblJqWHpBZ0x5OGdNQW9nSUNBZ1pYaDBjbUZqZEY5MWFXNTBNVFlnTHk4Z2IyNGdaWEp5YjNJNklHbHVkbUZzYVdRZ1lYSnlZWGtnYkdWdVozUm9JR2hsWVdSbGNnb2dJQ0FnY0hWemFHbHVkQ0E0Q2lBZ0lDQXFDaUFnSUNCcGJuUmpYekVnTHk4Z01nb2dJQ0FnS3dvZ0lDQWdLd29nSUNBZ1BUMEtJQ0FnSUdGemMyVnlkQ0F2THlCcGJuWmhiR2xrSUc1MWJXSmxjaUJ2WmlCaWVYUmxjeUJtYjNJZ2MyMWhjblJmWTI5dWRISmhZM1J6TDJGeVl6VTRMMlJoYnk5MGVYQmxjeTUwY3pvNlVISnZjRzl6WVd4U1pXMXZkbVZCYkd4dmQyRnVZMlZ6Q2lBZ0lDQmllWFJsWTE4d0lDOHZJREI0TVRVeFpqZGpOelVLSUNBZ0lITjNZWEFLSUNBZ0lHTnZibU5oZEFvZ0lDQWdiRzluQ2lBZ0lDQnBiblJqWHpJZ0x5OGdNUW9nSUNBZ2NtVjBkWEp1Q2dvS0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRnlZelU0TDJSaGJ5OTBlWEJsY3k1aGJHZHZMblJ6T2pwQmEybDBZVVJCVDFSNWNHVnpMbkJ5YjNCdmMyRnNUbVYzUlhOamNtOTNVMmhoY0dWYmNtOTFkR2x1WjEwb0tTQXRQaUIyYjJsa09ncHdjbTl3YjNOaGJFNWxkMFZ6WTNKdmQxTm9ZWEJsT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qVTFDaUFnSUNBdkx5QkFZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCa2FXY2dNUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUhSMWNHeGxJR1Z1WTI5a2FXNW5DaUFnSUNCa2RYQUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdkR0ZwYkNCd2IybHVkR1Z5SUdGMElHbHVaR1Y0SURBZ2IyWWdLQ2hzWlc0cmRYUm1PRnRkS1NrS0lDQWdJR1JwWnlBeUNpQWdJQ0J6ZDJGd0NpQWdJQ0JrYVdjZ01nb2dJQ0FnYzNWaWMzUnlhVzVuTXdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNeUF2THlBMENpQWdJQ0FyQ2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYSmpOVGd2WkdGdkwzUjVjR1Z6TG5Sek9qcFFjbTl3YjNOaGJFNWxkMFZ6WTNKdmR3b2dJQ0FnWW5sMFpXTmZNQ0F2THlBd2VERTFNV1kzWXpjMUNpQWdJQ0J6ZDJGd0NpQWdJQ0JqYjI1allYUUtJQ0FnSUd4dlp3b2dJQ0FnYVc1MFkxOHlJQzh2SURFS0lDQWdJSEpsZEhWeWJnb0tDaTh2SUhOdFlYSjBYMk52Ym5SeVlXTjBjeTloY21NMU9DOWtZVzh2ZEhsd1pYTXVZV3huYnk1MGN6bzZRV3RwZEdGRVFVOVVlWEJsY3k1d2NtOXdiM05oYkZSdloyZHNaVVZ6WTNKdmQweHZZMnRUYUdGd1pWdHliM1YwYVc1blhTZ3BJQzArSUhadmFXUTZDbkJ5YjNCdmMyRnNWRzluWjJ4bFJYTmpjbTkzVEc5amExTm9ZWEJsT2dvZ0lDQWdMeThnYzIxaGNuUmZZMjl1ZEhKaFkzUnpMMkZ5WXpVNEwyUmhieTkwZVhCbGN5NWhiR2R2TG5Sek9qWXdDaUFnSUNBdkx5QkFZV0pwYldWMGFHOWtLSHNnY21WaFpHOXViSGs2SUhSeWRXVWdmU2tLSUNBZ0lIUjRibUVnUVhCd2JHbGpZWFJwYjI1QmNtZHpJREVLSUNBZ0lHUjFjQW9nSUNBZ2JHVnVDaUFnSUNCa2FXY2dNUW9nSUNBZ2FXNTBZMTh3SUM4dklEQUtJQ0FnSUdWNGRISmhZM1JmZFdsdWRERTJJQzh2SUc5dUlHVnljbTl5T2lCcGJuWmhiR2xrSUhSMWNHeGxJR1Z1WTI5a2FXNW5DaUFnSUNCa2RYQUtJQ0FnSUdsdWRHTmZNU0F2THlBeUNpQWdJQ0E5UFFvZ0lDQWdZWE56WlhKMElDOHZJR2x1ZG1Gc2FXUWdkR0ZwYkNCd2IybHVkR1Z5SUdGMElHbHVaR1Y0SURBZ2IyWWdLQ2hzWlc0cmRYUm1PRnRkS1NrS0lDQWdJR1JwWnlBeUNpQWdJQ0J6ZDJGd0NpQWdJQ0JrYVdjZ01nb2dJQ0FnYzNWaWMzUnlhVzVuTXdvZ0lDQWdhVzUwWTE4d0lDOHZJREFLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lHRnljbUY1SUd4bGJtZDBhQ0JvWldGa1pYSUtJQ0FnSUdsdWRHTmZNeUF2THlBMENpQWdJQ0FyQ2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2JuVnRZbVZ5SUc5bUlHSjVkR1Z6SUdadmNpQnpiV0Z5ZEY5amIyNTBjbUZqZEhNdllYSmpOVGd2WkdGdkwzUjVjR1Z6TG5Sek9qcFFjbTl3YjNOaGJGUnZaMmRzWlVWelkzSnZkMHh2WTJzS0lDQWdJR0o1ZEdWalh6QWdMeThnTUhneE5URm1OMk0zTlFvZ0lDQWdjM2RoY0FvZ0lDQWdZMjl1WTJGMENpQWdJQ0JzYjJjS0lDQWdJR2x1ZEdOZk1pQXZMeUF4Q2lBZ0lDQnlaWFIxY200S0Nnb3ZMeUJ6YldGeWRGOWpiMjUwY21GamRITXZZWEpqTlRndlpHRnZMM1I1Y0dWekxtRnNaMjh1ZEhNNk9rRnJhWFJoUkVGUFZIbHdaWE11Y0hKdmNHOXpZV3hWY0dSaGRHVkdhV1ZzWkZOb1lYQmxXM0p2ZFhScGJtZGRLQ2tnTFQ0Z2RtOXBaRG9LY0hKdmNHOXpZV3hWY0dSaGRHVkdhV1ZzWkZOb1lYQmxPZ29nSUNBZ0x5OGdjMjFoY25SZlkyOXVkSEpoWTNSekwyRnlZelU0TDJSaGJ5OTBlWEJsY3k1aGJHZHZMblJ6T2pZMUNpQWdJQ0F2THlCQVlXSnBiV1YwYUc5a0tIc2djbVZoWkc5dWJIazZJSFJ5ZFdVZ2ZTa0tJQ0FnSUhSNGJtRWdRWEJ3YkdsallYUnBiMjVCY21keklERUtJQ0FnSUdSMWNBb2dJQ0FnYkdWdUNpQWdJQ0JrYVdjZ01Rb2dJQ0FnYVc1MFkxOHdJQzh2SURBS0lDQWdJR1Y0ZEhKaFkzUmZkV2x1ZERFMklDOHZJRzl1SUdWeWNtOXlPaUJwYm5aaGJHbGtJSFIxY0d4bElHVnVZMjlrYVc1bkNpQWdJQ0JrZFhBS0lDQWdJR2x1ZEdOZk15QXZMeUEwQ2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2RHRnBiQ0J3YjJsdWRHVnlJR0YwSUdsdVpHVjRJREFnYjJZZ0tDaHNaVzRyZFhSbU9GdGRLU3dvYkdWdUszVnBiblE0VzEwcEtRb2dJQ0FnWkdsbklESUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXlDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2NIVnphR2x1ZENBMkNpQWdJQ0FyQ2lBZ0lDQmthV2NnTWdvZ0lDQWdhVzUwWTE4eElDOHZJRElLSUNBZ0lHVjRkSEpoWTNSZmRXbHVkREUySUM4dklHOXVJR1Z5Y205eU9pQnBiblpoYkdsa0lIUjFjR3hsSUdWdVkyOWthVzVuQ2lBZ0lDQmtkWEFLSUNBZ0lHUnBaeUF5Q2lBZ0lDQTlQUW9nSUNBZ1lYTnpaWEowSUM4dklHbHVkbUZzYVdRZ2RHRnBiQ0J3YjJsdWRHVnlJR0YwSUdsdVpHVjRJREVnYjJZZ0tDaHNaVzRyZFhSbU9GdGRLU3dvYkdWdUszVnBiblE0VzEwcEtRb2dJQ0FnWkdsbklETUtJQ0FnSUhOM1lYQUtJQ0FnSUdScFp5QXpDaUFnSUNCemRXSnpkSEpwYm1jekNpQWdJQ0JwYm5Salh6QWdMeThnTUFvZ0lDQWdaWGgwY21GamRGOTFhVzUwTVRZZ0x5OGdiMjRnWlhKeWIzSTZJR2x1ZG1Gc2FXUWdZWEp5WVhrZ2JHVnVaM1JvSUdobFlXUmxjZ29nSUNBZ2FXNTBZMTh4SUM4dklESUtJQ0FnSUNzS0lDQWdJQ3NLSUNBZ0lEMDlDaUFnSUNCaGMzTmxjblFnTHk4Z2FXNTJZV3hwWkNCdWRXMWlaWElnYjJZZ1lubDBaWE1nWm05eUlITnRZWEowWDJOdmJuUnlZV04wY3k5aGNtTTFPQzlrWVc4dmRIbHdaWE11ZEhNNk9sQnliM0J2YzJGc1ZYQmtZWFJsUm1sbGJHUUtJQ0FnSUdKNWRHVmpYekFnTHk4Z01IZ3hOVEZtTjJNM05Rb2dJQ0FnYzNkaGNBb2dJQ0FnWTI5dVkyRjBDaUFnSUNCc2IyY0tJQ0FnSUdsdWRHTmZNaUF2THlBeENpQWdJQ0J5WlhSMWNtNEsiLCJjbGVhciI6IkkzQnlZV2R0WVNCMlpYSnphVzl1SURFeENpTndjbUZuYldFZ2RIbHdaWFJ5WVdOcklHWmhiSE5sQ2dvdkx5QkFZV3huYjNKaGJtUm1iM1Z1WkdGMGFXOXVMMkZzWjI5eVlXNWtMWFI1Y0dWelkzSnBjSFF2WW1GelpTMWpiMjUwY21GamRDNWtMblJ6T2pwQ1lYTmxRMjl1ZEhKaFkzUXVZMnhsWVhKVGRHRjBaVkJ5YjJkeVlXMG9LU0F0UGlCMWFXNTBOalE2Q20xaGFXNDZDaUFnSUNCd2RYTm9hVzUwSURFS0lDQWdJSEpsZEhWeWJnbz0ifSwiYnl0ZUNvZGUiOnsiYXBwcm92YWwiOiJDeUFFQUFJQkJDWUJCQlVmZkhVeEcwRUFhakVaRkVReEdFU0NEUVNTczFpV0JIN2t1cmNFZ3RYei93VHpsQm9zQlBIUElzd0V2VTczTUFUcnExNFVCR3VLdlM4RS9LK0VJQVJWemx5cEJLWTR2aU1FVUtxNEhRUVVuVHpMTmhvQWpnMEFDUUF1QUpVQkVBRXlBV2dCb2dIYkFlb0NJUUpZQW5jQ2xnQXhHUlF4R0JRUVF6WWFBVWtWU3dHQktGbEpnVG9TUkVzQ1RFc0NVaUpaZ1NBTGdUd0lFa1FvVEZDd0pFTTJHZ0ZKRlVzQmdTaFpTWUZxRWtSTEFreExBbElpV1lGc0NFc0NnVHRaU1VzQ0VrUkxBMHhMQTFJaVdZRU1DeU1JQ0VzQ2dXWlpTVXNDRWtSTEEweExBMUlpV1NNSUNFc0NnV2haU1VzQ0VrUkxBMHhMQTFJaVdZRWlDeU1JQ0JKRUtFeFFzQ1JETmhvQlNSVkxBU0paU1lGc0VrUkxBa3hMQWxJaVdZRnVDRXNDZ1NwWlNVc0NFa1JMQTB4TEExSWlXU01JQ0VzQ2dUMVpTVXNDRWtSTEEweExBMUlpV1lFTUN5TUlDRXNDZ1doWlNVc0NFa1JMQTB4TEExSWlXU01JQ0VzQ2dXcFpTVXNDRWtSTEEweExBMUlpV1lFaUN5TUlDQkpFS0V4UXNDUkROaG9CU1JWTEFZRW9XVW1CS2hKRVN3Sk1Td0pTSWxtQkxBZ1NSQ2hNVUxBa1F6WWFBVWtWU3dFaVdVbUJMQkpFU3dKTVN3SlNJbG1CTGdoTEFvRXFXVWxMQWhKRVN3Tk1Td05TSWxrakNBZ1NSQ2hNVUxBa1F6WWFBVWtWU3dHQkNGbEpnVHdTUkVzQ1RFc0NVaUpaZ1Q0SVN3S0JLbGxKU3dJU1JFc0RURXNEVWlKWmdTQUxJd2dJRWtRb1RGQ3dKRU0yR2dGSkZVc0JJbGxKZ1RRU1JFc0NURXNDVWlKWmdUWUlTd0tCSWxsSlN3SVNSRXNEVEVzRFVpSlpnU0FMSXdnSUVrUW9URkN3SkVNMkdnRkpGWUVnRWtRb1RGQ3dKRU0yR2dGSkZVc0JJbGxKSlJKRVN3Sk1Td0pTSWxtQkJnaExBaU5aU1VzQ0VrUkxBMHhMQTFJaVdZRWlDeU1JQ0JKRUtFeFFzQ1JETmhvQlNSVkxBU0paU1NVU1JFc0NURXNDVWlKWmdRWUlTd0lqV1VsTEFoSkVTd05NU3dOU0lsbUJDQXNqQ0FnU1JDaE1VTEFrUXpZYUFVa1ZTd0VpV1VrakVrUkxBa3hMQWxJaVdTVUlFa1FvVEZDd0pFTTJHZ0ZKRlVzQklsbEpJeEpFU3dKTVN3SlNJbGtsQ0JKRUtFeFFzQ1JETmhvQlNSVkxBU0paU1NVU1JFc0NURXNDVWlKWmdRWUlTd0lqV1VsTEFoSkVTd05NU3dOU0lsa2pDQWdTUkNoTVVMQWtRdz09IiwiY2xlYXIiOiJDNEVCUXc9PSJ9LCJjb21waWxlckluZm8iOnsiY29tcGlsZXIiOiJwdXlhIiwiY29tcGlsZXJWZXJzaW9uIjp7Im1ham9yIjo1LCJtaW5vciI6OSwicGF0Y2giOjAsImNvbW1pdEhhc2giOm51bGx9fSwiZXZlbnRzIjpbXSwidGVtcGxhdGVWYXJpYWJsZXMiOnt9LCJzY3JhdGNoVmFyaWFibGVzIjp7fX0=";
    }

}
