
namespace Algorand.Algod.Model
{
    using Algorand.Algod.Model.Transactions;

    using System = global::System;
#if UNITY
using UnityEngine;
#endif

#if UNITY
[System.Serializable]
#endif
    [MessagePack.MessagePackObject]
    public partial class SimulationTransactionExecTrace
    {

        [Newtonsoft.Json.JsonProperty("approval-program-hash", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("approval-program-hash")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"SHA512_256 hash digest of the approval program executed in transaction.")]
    [field:InspectorName(@"ApprovalProgramHash")]
    public byte[] ApprovalProgramHash {get;set;}
#else
        public byte[] ApprovalProgramHash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("approval-program-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("approval-program-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Program trace that contains a trace of opcode effects in an approval program.")]
    [field:InspectorName(@"ApprovalProgramTrace")]
    public System.Collections.Generic.List<SimulationOpcodeTraceUnit> ApprovalProgramTrace {get;set;} = new System.Collections.Generic.List<SimulationOpcodeTraceUnit>();
#else
        public System.Collections.Generic.ICollection<SimulationOpcodeTraceUnit> ApprovalProgramTrace { get; set; } = new System.Collections.ObjectModel.Collection<SimulationOpcodeTraceUnit>();
#endif

        [Newtonsoft.Json.JsonProperty("clear-state-program-hash", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("clear-state-program-hash")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"SHA512_256 hash digest of the clear state program executed in transaction.")]
    [field:InspectorName(@"ClearStateProgramHash")]
    public byte[] ClearStateProgramHash {get;set;}
#else
        public byte[] ClearStateProgramHash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("clear-state-program-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("clear-state-program-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Program trace that contains a trace of opcode effects in a clear state program.")]
    [field:InspectorName(@"ClearStateProgramTrace")]
    public System.Collections.Generic.List<SimulationOpcodeTraceUnit> ClearStateProgramTrace {get;set;} = new System.Collections.Generic.List<SimulationOpcodeTraceUnit>();
#else
        public System.Collections.Generic.ICollection<SimulationOpcodeTraceUnit> ClearStateProgramTrace { get; set; } = new System.Collections.ObjectModel.Collection<SimulationOpcodeTraceUnit>();
#endif

        [Newtonsoft.Json.JsonProperty("clear-state-rollback", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("clear-state-rollback")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"If true, indicates that the clear state program failed and any persistent state changes it produced should be reverted once the program exits.")]
    [field:InspectorName(@"ClearStateRollback")]
    public bool ClearStateRollback {get;set;}
#else
        public bool? ClearStateRollback { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("clear-state-rollback-error", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("clear-state-rollback-error")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"The error message explaining why the clear state program failed. This field will only be populated if clear-state-rollback is true and the failure was due to an execution error.")]
    [field:InspectorName(@"ClearStateRollbackError")]
    public string ClearStateRollbackError {get;set;}
#else
        public string ClearStateRollbackError { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("inner-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("inner-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"An array of SimulationTransactionExecTrace representing the execution trace of any inner transactions executed.")]
    [field:InspectorName(@"InnerTrace")]
    public System.Collections.Generic.List<SimulationTransactionExecTrace> InnerTrace {get;set;} = new System.Collections.Generic.List<SimulationTransactionExecTrace>();
#else
        public System.Collections.Generic.ICollection<SimulationTransactionExecTrace> InnerTrace { get; set; } = new System.Collections.ObjectModel.Collection<SimulationTransactionExecTrace>();
#endif

        [Newtonsoft.Json.JsonProperty("logic-sig-hash", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-hash")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"SHA512_256 hash digest of the logic sig executed in transaction.")]
    [field:InspectorName(@"LogicSigHash")]
    public byte[] LogicSigHash {get;set;}
#else
        public byte[] LogicSigHash { get; set; }
#endif



        [Newtonsoft.Json.JsonProperty("logic-sig-trace", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [MessagePack.Key("logic-sig-trace")]
#if UNITY
    [field:SerializeField]
    [Tooltip(@"Program trace that contains a trace of opcode effects in a logic sig.")]
    [field:InspectorName(@"LogicSigTrace")]
    public System.Collections.Generic.List<SimulationOpcodeTraceUnit> LogicSigTrace {get;set;} = new System.Collections.Generic.List<SimulationOpcodeTraceUnit>();
#else
        public System.Collections.Generic.ICollection<SimulationOpcodeTraceUnit> LogicSigTrace { get; set; } = new System.Collections.ObjectModel.Collection<SimulationOpcodeTraceUnit>();
#endif

    }


}
