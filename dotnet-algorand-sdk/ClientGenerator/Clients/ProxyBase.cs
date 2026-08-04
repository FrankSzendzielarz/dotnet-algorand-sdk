using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Exceptions;
using Algorand.Algod.Model.Transactions;
using AVM.ClientGenerator.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlgoUtils = Algorand.Utils;

namespace AVM.ClientGenerator
{


    public class ProxyException : Exception
    {
        public ProxyException(string message, Exception inner) : base(message, inner)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public ProxyException(string message) : base(message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

    }

    public class ProxyBase
    {
        public virtual AVM.ClientGenerator.ABI.ARC56.AppDescriptionArc56 App { get; set; }
        /// <summary>
        /// Developers can set the override for the tx params response, so that when creating new transactions the parameters are not fetched.
        /// 
        /// When user will request multiple transactions from the client or the user wants to rewrite the tx params, he can supply the tx params here.
        /// </summary>
        public TransactionParametersResponse? TransParamsOverride { get; set; }

        DefaultApi client;
        public ulong appId { get; protected set; }
        /// <summary>
        /// The raw logs (including any ARC-28 event logs) produced by the most recent CallApp/SimApp invocation.
        /// Generated ARC56 clients emit an Events.&lt;Name&gt;Event.Decode(log)/Matches(log) helper per contract
        /// event; callers can filter these logs against those helpers to recover the emitted events.
        /// </summary>
        public ICollection<byte[]> LastCallLogs { get; protected set; } = new List<byte[]>();
        public Address AppAddress
        {
            get
            {
                return Address.ForApplication(appId);
            }
        }
        /// <summary>
        /// Base64 TEAL code of approval program
        /// </summary>
        protected virtual string SourceApproval { get; set; } = "";
        /// <summary>
        /// Base64 TEAL code of clear state program
        /// </summary>
        protected virtual string SourceClear { get; set; } = "";
        /// <summary>
        /// Base64 AVM code of approval program
        /// </summary>
        protected virtual string SourceApprovalAVM { get; set; } = "";
        /// <summary>
        /// Base64 AVM code of clear state program
        /// </summary>
        protected virtual string SourceClearAVM { get; set; } = "Cg==";
        /// <summary>
        /// Number of maximum global byte[] variables
        /// </summary>
        protected virtual ulong? GlobalNumByteSlices { get; set; } = 0;
        /// <summary>
        /// Number of maximum global uint64 variables
        /// </summary>
        protected virtual ulong? GlobalNumUints { get; set; } = 0;
        /// <summary>
        /// Number of maximum local byte[] variables
        /// </summary>
        protected virtual ulong? LocalNumByteSlices { get; set; } = 0;
        /// <summary>
        /// Number of maximum local uint64 variables
        /// </summary>
        protected virtual ulong? LocalNumUints { get; set; } = 0;
        /// <summary>
        /// Up to 3 extensions to the program size
        /// </summary>
        protected virtual ulong? ExtraProgramPages { get; set; } = 0;

        public ProxyBase(DefaultApi algodApi, ulong appId)
        {
            this.client = algodApi;
            this.appId = appId;
        }


        protected byte[] ReverseIfLittleEndian(byte[] bytes)
        {
            return bytes.ReverseIfLittleEndian();
        }

        protected decimal GetDecimalFromBytes(byte[] resultBytes)
        {
            byte[] lo;
            byte[] mid;
            byte[] hi;
            byte[] flags;
            if (BitConverter.IsLittleEndian)
            {
                lo = resultBytes.Take(4).Reverse().ToArray();
                mid = resultBytes.Skip(4).Take(4).Reverse().ToArray();
                hi = resultBytes.Skip(8).Take(4).Reverse().ToArray();
                flags = resultBytes.Skip(12).Take(4).Reverse().ToArray();
            }
            else
            {
                lo = resultBytes.Take(4).ToArray();
                mid = resultBytes.Skip(4).Take(4).ToArray();
                hi = resultBytes.Skip(8).Take(4).ToArray();
                flags = resultBytes.Skip(12).Take(4).ToArray();
            }

            int intlo = BitConverter.ToInt32(lo, 0);
            int intmid = BitConverter.ToInt32(mid, 0);
            int inthi = BitConverter.ToInt32(hi, 0);
            int intflags = BitConverter.ToInt32(flags, 0);
            Decimal r = new decimal(new int[] { intlo, intmid, inthi, intflags });

            return r;

        }


        protected BigInteger GetBigIntegerFromByte(byte[] bytes)
        {
            //BigInteger is ALWAYS little endian and bytes is always bigendian, so we must first reverse them, which
            //is why we must not use ReverseIfLittleEndian:
            var reversed = bytes.Reverse().ToArray();

            //we treat the bytes as a 64 byte number, so we first must make sure it's 64 bytes long
            if (reversed.Length < 64)
            {
                reversed = reversed.Concat(new byte[64 - reversed.Length]).ToArray();
            }

            //and if it's over 64 bytes long we at this point truncate it to 64 bytes
            if (reversed.Length > 64)
            {
                reversed = reversed.Take(64).ToArray();
            }

            //we now have a 64 byte number in little endian format, but we must determine if the highest bit is 1 to see if it is negative
            bool isNegative = (reversed[reversed.Length - 1] & 0x80) != 0;

            //now we convert it to a BigInteger
            BigInteger result = new BigInteger(reversed);

            //if it was determined that the number was negative, we need to convert the value to its two's complement, and then negate that result to set the sign bit:
            if (isNegative)
            {
                //make a byte array of FF to the length of the 'reversed' array:
                BigInteger ones = new BigInteger(Enumerable.Repeat((byte)0xFF, reversed.Length).ToArray());

                result = -((result ^ ones) + 1);
            }

            return result;


        }

        private List<byte[]> toByteArrays(List<object> args)
        {
            //   return args.Select(a => TealTypeUtils.ToByteArray(a)).ToList();
            return args.Select(a => TealTypeUtils.EncodeArgument(a)).Where(a => a != null).ToList();
        }

        protected async Task<byte[]> GetGlobalByteSlice(string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.GetApplicationByIDAsync(appId);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val = result.Params.GlobalState.Where(tk => tk.Key == key && tk.Value.Type == 1).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                return new byte[] { };
            }

            return Convert.FromBase64String(val.Bytes);
        }

        protected async Task<byte[]> GetLocalByteSlice(Account caller, string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.AccountApplicationInformationAsync(caller.Address.ToString(), appId, null);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val = result.AppLocalState.KeyValue.Where(tk => tk.Key == key && tk.Value.Type == 1).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                return new byte[] { };
            }
            return Convert.FromBase64String(val.Bytes);
        }

        protected async Task<ulong> GetGlobalUInt(string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.GetApplicationByIDAsync(appId);
            if (result == null)
            {
                throw new ProxyException($"Could not get application state for {appId}");
            }
            TealValue val = result.Params.GlobalState.Where(tk => tk.Key == key && tk.Value.Type == 2).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                return 0;
            }
            return val.Uint;
        }

        protected async Task<ulong> GetLocalUInt(Account caller, string key)
        {
            key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
            var result = await client.AccountApplicationInformationAsync(caller.Address.ToString(), appId, null);
            if (result == null)
            {
                return 0;
            }
            TealValue val = result.AppLocalState.KeyValue.Where(tk => tk.Key == key && tk.Value.Type == 1).Select(tk => tk.Value).FirstOrDefault();
            if (val == null)
            {
                throw new ProxyException($"Key of byte slice type not found for app {appId} with key {key}");
            }
            return val.Uint;
        }


        protected async Task<List<Transaction>> MakeTransactionList(List<object> args, Account _tx_sender, ulong? _tx_fee, string _tx_note, ulong _tx_roundValidity, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            TransactionParametersResponse transParams;
            try
            {
                if (TransParamsOverride != null)
                {
                    transParams = TransParamsOverride;
                }
                else
                {
                    transParams = await client.TransactionParamsAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {
                ApplicationCallTransaction tx = makeStandardAppCallTxn(_tx_fee, _tx_callType, _tx_roundValidity, _tx_note, _tx_sender, args, _tx_apps, _tx_assets, _tx_accounts, _tx_boxes, transParams);

                List<Transaction> txs = new List<Transaction>();
                if (_tx_transactions != null && _tx_transactions.Count > 0)
                {
                    _tx_transactions.Add(tx);
                    Digest gid = TxGroup.ComputeGroupID(_tx_transactions.ToArray());
                    foreach (var txToSign in _tx_transactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign);
                    }
                }
                else
                {
                    txs.Add(tx);
                }


                return txs;

            }
            catch (Exception ex)
            {
                throw new ProxyException("Call failed.", ex);
            }
        }

        protected async Task<List<Transaction>> MakeArc4TransactionList(List<Transaction> preTransactions, ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, byte[] selector, List<ABI.ARC4.Types.WireType> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes = null)
        {
            TransactionParametersResponse transParams;
            try
            {
                if (TransParamsOverride != null)
                {
                    transParams = TransParamsOverride;
                }
                else
                {
                    transParams = await client.TransactionParamsAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {
                ApplicationCallTransaction tx = makeArc4AppCallTxn(fee, onComplete, roundValidity, note, sender, selector, args, foreignApps, foreignAssets, accounts, boxes, transParams);

                List<Transaction> txs = new List<Transaction>();
                if (preTransactions != null && preTransactions.Count > 0)
                {
                    preTransactions.Add(tx);
                    Digest gid = TxGroup.ComputeGroupID(preTransactions.ToArray());
                    foreach (var txToSign in preTransactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign);
                    }
                }
                else
                {
                    txs.Add(tx);
                }


                return txs;

            }
            catch (Exception ex)
            {
                throw new ProxyException("Call failed.", ex);
            }
        }


        protected async Task<ICollection<byte[]>> CallApp(List<object> args, Account _tx_sender, ulong? _tx_fee, string _tx_note, ulong _tx_roundValidity, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            TransactionParametersResponse transParams;
            try
            {
                if (TransParamsOverride != null)
                {
                    transParams = TransParamsOverride;
                }
                else
                {
                    transParams = await client.TransactionParamsAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {


                ApplicationCallTransaction tx = makeStandardAppCallTxn(_tx_fee, _tx_callType, _tx_roundValidity, _tx_note, _tx_sender, args, _tx_apps, _tx_assets, _tx_accounts, _tx_boxes, transParams);

                List<SignedTransaction> txs = new List<SignedTransaction>();
                if (_tx_transactions != null && _tx_transactions.Count > 0)
                {
                    tx.Group = null;
                    _tx_transactions.ForEach(tx => tx.Group = null);
                    _tx_transactions.Add(tx);
                    foreach (var txToSign in _tx_transactions)
                    {
                        if (string.IsNullOrEmpty(txToSign.GenesisId))
                        {
                            txToSign.FillInParams(transParams);
                        }
                    }
                    Digest gid = TxGroup.ComputeGroupID(_tx_transactions.ToArray());
                    foreach (var txToSign in _tx_transactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign.Sign(_tx_sender));
                    }
                }
                else
                {
                    txs.Add(tx.Sign(_tx_sender));
                }
                //TODO verify it's the last txn that's returned when a group is sent

                await client.TransactionsAsync(txs);
                if (_tx_callType == Core.OnCompleteType.CreateApplication)
                {

                    var resp = await AlgoUtils.Utils.WaitTransactionToComplete(client, tx.TxID()) as ApplicationCreateTransaction;

#if UNITY
                    this.appId = resp.ApplicationIndex;
#else
                    this.appId = resp.ApplicationIndex ?? throw new Exception("Application index is null after successful call to create aplication");
#endif
                    LastCallLogs = resp.Logs ?? new List<byte[]>();
                    return resp.Logs;
                }
                else
                {
                    var resp = await AlgoUtils.Utils.WaitTransactionToComplete(client, tx.TxID()) as ApplicationCallTransaction;

                    LastCallLogs = resp.Logs ?? new List<byte[]>();
                    return resp.Logs;
                }

            }
            catch (Exception ex)
            {
                if (ex is Algorand.ApiException<ErrorResponse> exApi)
                {
                    Match match = Regex.Match(exApi.Result.Message, @"pc\s*=\s*(\d+)");
                    if (match.Success)
                    {
                        if (int.TryParse(match.Groups[1].Value, out var pc))
                        {
                            // App (ARC56 source-map info) is only populated for ARC56-generated proxies; ARC32
                            // ones leave it null, so this pc-lookup is best-effort and must not blow up the
                            // original algod error with an unrelated NullReferenceException.
                            var si = App?.SourceInfo?.Approval?.SourceInfo?.Where(si => si.Pc.Contains(pc) && !string.IsNullOrEmpty(si.ErrorMessage)).FirstOrDefault();
                            if (si != null)
                            {
                                throw new ProxyException(si.ErrorMessage + $" [pc {pc}]", ex);
                            }
                            else
                            {
                                throw new ProxyException(exApi.Result.Message + $" [pc {pc}]", ex);
                            }
                        }
                    }
                    throw new ProxyException(exApi.Result.Message, ex);
                }
                throw new ProxyException("Call failed.", ex);
            }
        }

        protected async Task<ICollection<byte[]>> SimApp(List<object> args, Account _tx_sender, ulong? _tx_fee, string _tx_note, ulong _tx_roundValidity, List<BoxRef>? _tx_boxes = null, List<Transaction>? _tx_transactions = null, List<ulong>? _tx_assets = null, List<ulong>? _tx_apps = null, List<Address>? _tx_accounts = null, AVM.ClientGenerator.Core.OnCompleteType _tx_callType = AVM.ClientGenerator.Core.OnCompleteType.NoOp)
        {
            TransactionParametersResponse transParams;
            try
            {
                if (TransParamsOverride != null)
                {
                    transParams = TransParamsOverride;
                }
                else
                {
                    transParams = await client.TransactionParamsAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ProxyException("Unable to get transaction parameters.", ex);
            }

            try
            {


                ApplicationCallTransaction tx = makeStandardAppCallTxn(_tx_fee, _tx_callType, _tx_roundValidity, _tx_note, _tx_sender, args, _tx_apps, _tx_assets, _tx_accounts, _tx_boxes, transParams);

                List<SignedTransaction> txs = new List<SignedTransaction>();
                if (_tx_transactions != null && _tx_transactions.Count > 0)
                {
                    tx.Group = null;
                    _tx_transactions.ForEach(tx => tx.Group = null);
                    _tx_transactions.Add(tx);
                    Digest gid = TxGroup.ComputeGroupID(_tx_transactions.ToArray());
                    foreach (var txToSign in _tx_transactions)
                    {
                        txToSign.Group = gid;
                        txs.Add(txToSign.Sign(_tx_sender));
                    }
                }
                else
                {
                    txs.Add(tx.Sign(_tx_sender));
                }
                //TODO verify it's the last txn that's returned when a group is sent
                var simResult = await client.SimulateTransactionAsync(new SimulateRequest()
                {
                    AllowEmptySignatures = true,
                    AllowMoreLogging = true,
                    
                    AllowUnnamedResources = true,
                    ExecTraceConfig = new SimulateTraceConfig()
                    {
                        Enable = true,
                        ScratchChange = true,
                        StackChange = true,
                        StateChange = true
                    },
                    TxnGroups = new List<SimulateRequestTransactionGroup>()
                    {
                        new SimulateRequestTransactionGroup()
                        {
                            Txns = txs
                        }
                    }
                });
                var txGroup = simResult.TxnGroups.FirstOrDefault();
                if (txGroup.TxnResults.LastOrDefault().TxnResult is Algorand.Algod.Model.Transactions.ApplicationNoopTransaction appTx)
                {
                    var error = txGroup.FailureMessage;
                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception(error);
                    }
                    if (txGroup.UnnamedResourcesAccessed != null)
                    {
                        if (txGroup.UnnamedResourcesAccessed.Assets.Count > 0 || txGroup.UnnamedResourcesAccessed.Apps.Count > 0 || txGroup.UnnamedResourcesAccessed.Boxes.Count > 0 || txGroup.UnnamedResourcesAccessed.Accounts.Count > 0 || txGroup.UnnamedResourcesAccessed.AssetHoldings.Count > 0)
                        {
                            throw new UnnamedResourceException(txGroup.UnnamedResourcesAccessed);
                        }
                    }

                    LastCallLogs = appTx.Logs ?? new List<byte[]>();
                    return appTx.Logs;
                }
                var ret = new List<byte[]>();
                LastCallLogs = ret;
                return ret;
            }
            catch (Exception ex)
            {
                if (ex is Algorand.ApiException<ErrorResponse> exApi)
                {
                    Match match = Regex.Match(exApi.Result.Message, @"pc\s*=\s*(\d+)");
                    if (match.Success)
                    {
                        if (int.TryParse(match.Groups[1].Value, out var pc))
                        {
                            // App (ARC56 source-map info) is only populated for ARC56-generated proxies; ARC32
                            // ones leave it null, so this pc-lookup is best-effort and must not blow up the
                            // original algod error with an unrelated NullReferenceException.
                            var si = App?.SourceInfo?.Approval?.SourceInfo?.Where(si => si.Pc.Contains(pc) && !string.IsNullOrEmpty(si.ErrorMessage)).FirstOrDefault();
                            if (si != null)
                            {
                                throw new ProxyException(si.ErrorMessage + $" [pc {pc}]", ex);
                            }
                            else
                            {
                                throw new ProxyException(exApi.Result.Message + $" [pc {pc}]", ex);
                            }
                        }
                    }
                    throw new ProxyException("Call failed: " + exApi.Result.Message, ex);
                }
                if(ex is UnnamedResourceException)
                {
                    throw;
                }
                throw new ProxyException("Call failed.", ex);
            }
        }

        private ApplicationCallTransaction makeArc4AppCallTxn(ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, byte[] selector, List<ABI.ARC4.Types.WireType> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes, TransactionParametersResponse transParams)
        {
            //if the arg list is 16 or less, encode each wiretype, otherwise the first 15 args are encoded and the remainder are made into a Tuple WireType and the tuple is passed as the 16th arg:
            List<byte[]> bargs;
            if (args.Count <= 15)
            {
                bargs = args.Select(a => a.Encode()).ToList();
            }
            else
            {
                bargs = args.Take(14).Select(a => a.Encode()).ToList();
                ABI.ARC4.Types.Tuple tuple = new ABI.ARC4.Types.Tuple();
                foreach (var a in args.Skip(14))
                {
                    tuple.Value.Add(a);
                }
                bargs.Add(tuple.Encode());
            }
            //selector is always the first arg
            bargs.Insert(0, selector);


            return makeAppCallTxn(fee, onComplete, roundValidity, note, sender, bargs, foreignApps, foreignAssets, accounts, boxes, transParams);
        }

        private ApplicationCallTransaction makeStandardAppCallTxn(ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, List<object> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes, TransactionParametersResponse transParams)
        {

            var bargs = toByteArrays(args);

            return makeAppCallTxn(fee, onComplete, roundValidity, note, sender, bargs, foreignApps, foreignAssets, accounts, boxes, transParams);

        }

        private ApplicationCallTransaction makeAppCallTxn(ulong? fee, Core.OnCompleteType onComplete, ulong roundValidity, string note, Account sender, List<byte[]> args, List<ulong> foreignApps, List<ulong> foreignAssets, List<Address> accounts, List<BoxRef> boxes, TransactionParametersResponse transParams)
        {
            ApplicationCallTransaction tx;
            switch (onComplete)
            {
                case Core.OnCompleteType.NoOp:
                    tx = new ApplicationNoopTransaction() { ApplicationId = appId };
                    break;
                case Core.OnCompleteType.OptIn:
                    tx = new ApplicationOptInTransaction() { ApplicationId = appId };
                    break;
                case Core.OnCompleteType.CloseOut:
                    tx = new ApplicationCloseOutTransaction() { ApplicationId = appId };
                    break;

                case Core.OnCompleteType.CreateApplication:
#if UNITY
                    tx = new ApplicationCreateTransaction()
                    {
                        ApprovalProgram = new TEALProgram(SourceApprovalAVM),
                        ClearStateProgram = new TEALProgram(SourceClearAVM),
                        GlobalStateSchema = new StateSchema()
                        {
                            NumByteSlice = GlobalNumByteSlices.Value,
                            NumUint = GlobalNumUints.Value
                        },
                        LocalStateSchema = new StateSchema()
                        {
                            NumByteSlice = LocalNumByteSlices.Value,
                            NumUint = LocalNumUints.Value
                        },
                        ExtraProgramPages = ExtraProgramPages.Value
                    };
#else
                    tx = new ApplicationCreateTransaction()
                    {
                        ApprovalProgram = new TEALProgram(App?.ByteCode?.Approval ?? SourceApprovalAVM),
                        ClearStateProgram = new TEALProgram(App?.ByteCode?.Clear ?? SourceClearAVM),
                        GlobalStateSchema = new StateSchema()
                        {
                            NumByteSlice = App?.State?.Schema?.Global?.Bytes ?? GlobalNumByteSlices,
                            NumUint = App?.State?.Schema?.Global?.Ints ?? GlobalNumUints
                        },
                        LocalStateSchema = new StateSchema()
                        {
                            NumByteSlice = App?.State?.Schema?.Local?.Bytes ?? LocalNumByteSlices,
                            NumUint = App?.State?.Schema?.Local?.Ints ?? LocalNumUints
                        },
                        ExtraProgramPages = ExtraProgramPages
                    };
#endif
                    break;
                case Core.OnCompleteType.UpdateApplication:
#if UNITY
                    tx = new ApplicationUpdateTransaction()
                    {
                        ApplicationId = appId,

                        ApprovalProgram = new TEALProgram(SourceApprovalAVM),
                        ClearStateProgram = new TEALProgram(SourceClearAVM),
                        GlobalStateSchema = new StateSchema()
                        {
                            NumByteSlice = GlobalNumByteSlices.Value,
                            NumUint = GlobalNumUints.Value
                        },
                        LocalStateSchema = new StateSchema()
                        {
                            NumByteSlice = LocalNumByteSlices.Value,
                            NumUint = LocalNumUints.Value
                        },
                        //ExtraProgramPages = ExtraProgramPages
                    };
#else
                    tx = new ApplicationUpdateTransaction()
                    {
                        ApplicationId = appId,

                        ApprovalProgram = new TEALProgram(App?.ByteCode?.Approval ?? SourceApprovalAVM),
                        ClearStateProgram = new TEALProgram(App?.ByteCode?.Clear ?? SourceClearAVM),
                        GlobalStateSchema = new StateSchema()
                        {
                            NumByteSlice = App?.State?.Schema?.Global?.Bytes ?? GlobalNumByteSlices,
                            NumUint = App?.State?.Schema?.Global?.Ints ?? GlobalNumUints
                        },
                        LocalStateSchema = new StateSchema()
                        {
                            NumByteSlice = App?.State?.Schema?.Local?.Bytes ?? LocalNumByteSlices,
                            NumUint = App?.State?.Schema?.Local?.Ints ?? LocalNumUints
                        },
                        //ExtraProgramPages = ExtraProgramPages
                    };
#endif
                    break;
                case Core.OnCompleteType.DeleteApplication:
                    tx = new ApplicationDeleteTransaction() { ApplicationId = appId };
                    break;
                default:
                    throw new ProxyException("Unknown on-complete type.");
            }

            tx.Sender = sender.Address;
            if (fee == null) tx.Fee = transParams.Fee >= 1000 ? transParams.Fee : 1000;
            else
                tx.Fee = fee.Value;
            tx.FirstValid = transParams.LastRound;
            tx.LastValid = transParams.LastRound + roundValidity;
            tx.GenesisId = transParams.GenesisId;
            tx.GenesisHash = new Digest(transParams.GenesisHash);
            tx.Note = Encoding.UTF8.GetBytes(note);
            tx.ApplicationArgs = args;
            tx.ForeignApps = foreignApps;
            tx.ForeignAssets = foreignAssets;
            tx.Accounts = accounts;
            tx.Boxes = boxes;



            return tx;
        }
    }
}
