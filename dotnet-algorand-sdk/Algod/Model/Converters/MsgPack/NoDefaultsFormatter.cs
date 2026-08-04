using Algorand.Algod.Model.Transactions;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Algorand.Algod.Model.Converters.MsgPack
{

    /// <summary>
    /// Implements a MessagePack formatter that leaves out object properties that have their default
    /// value, as well as properties marked as ignored.
    /// </summary>
    /// <remarks>
    /// Workaround for https://github.com/MessagePack-CSharp/MessagePack-CSharp/issues/678
    /// 
    /// https://github.com/Rafaeruo/pkl-csharp/blob/9cd4d725fa7e254b73fe322630edd1df04f79f89/Pkl/InternalMsgApi/NoDefaultsFormatter.cs
    /// </remarks>
    /// <typeparam name="T">The type to format.</typeparam>
    public class NoDefaultsFormatter<T> : IMessagePackFormatter<T>
    {
        /// <summary>
        /// Serializes the object.
        /// </summary>
        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            var dict = new Dictionary<string, object?>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<IgnoreMemberAttribute>() != null)
                {
                    continue;
                }

                var propValue = property.GetValue(value);
                // TODO: maybe add an attribute such as "OmitEmpty" or "OmitDefault" 
                // to flag which properties should be ommited when with default values
                if (property.PropertyType != typeof(bool) && object.Equals(propValue, GetDefault(property.PropertyType)))
                {
                    continue;
                }

                var name = property.Name;

                var keyAttribute = property.GetCustomAttribute<KeyAttribute>();
                if (keyAttribute != null && keyAttribute.StringKey != null)
                {
                    name = keyAttribute.StringKey;
                }
                if (propValue is ulong ulongValue)
                {
                    if (ulongValue < byte.MaxValue)
                    {
                        dict[name] = Convert.ToByte(ulongValue);
                    }
                    if (ulongValue < UInt16.MaxValue)
                    {
                        dict[name] = Convert.ToUInt16(ulongValue);
                    }
                    else if (ulongValue < UInt32.MaxValue)
                    {
                        dict[name] = Convert.ToUInt32(ulongValue);
                    }
                    else
                    {
                        dict[name] = ulongValue;
                    }
                }
                else
                {
                    if (propValue is Algorand.Signature objSgn)
                    {
                        dict[name] = objSgn.Bytes;
                    }
                    else if (propValue is Algorand.Address objAddress)
                    {
                        dict[name] = objAddress.Bytes;
                    }
                    else if (propValue is Algorand.Digest objDigest)
                    {
                        dict[name] = objDigest.Bytes;
                    }
                    else if (propValue is Algorand.ParticipationPublicKey objParticipationPublicKey)
                    {
                        dict[name] = objParticipationPublicKey.Bytes;
                    }
                    else if (propValue is Algorand.TEALProgram objTEALProgram)
                    {
                        dict[name] = objTEALProgram.Bytes;
                    }
                    else if (propValue is Algorand.VRFPublicKey objVRFPublicKey)
                    {
                        dict[name] = objVRFPublicKey.Bytes;
                    }
                    else
                    {
                        dict[name] = propValue;
                    }
                }
            }

            // sort dict
            dict = dict
                .OrderBy(kvp => kvp.Key, StringComparer.Ordinal)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            options.Resolver.GetFormatterWithVerify<Dictionary<string, object?>>().Serialize(ref writer, dict, options);
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return default!;
            }
            var dict = options.Resolver.GetFormatterWithVerify<Dictionary<object, object?>>().Deserialize(ref reader, options);
            var instance = dict.Deserialize<T>();
            return instance;
        }

        private static object? GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
    public static class Deserializer
    {
        public static T Deserialize<T>(this Dictionary<object, object?> dict)
        {
            var instance = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (!property.CanWrite) continue;
                if (property.GetCustomAttribute<IgnoreMemberAttribute>() != null)
                {
                    continue;
                }
                var keyAttribute = property.GetCustomAttribute<KeyAttribute>();
                var name = keyAttribute?.StringKey ?? property.Name;
                if (dict.TryGetValue(name, out var value))
                {
                    switch (property.PropertyType.Name)
                    {
                        case "List`1":
                            switch (property.Name)
                            {
                                case "Subsigs":
                                    var array = value as Object[];
                                    var ret = new List<MultisigSubsig>() { };
                                    foreach (var item in array)
                                    {
                                        var itemObj = item as Dictionary<object, object?>;
                                        if (itemObj == null)
                                        {
                                            throw new Exception("InnerTxns cannot be deserialized");
                                        }
                                        if (itemObj.ContainsKey("s"))
                                        {
                                            var txToAdd = new MultisigSubsig(itemObj["pk"] as byte[], itemObj["s"] as byte[]);
                                            ret.Add(txToAdd);
                                        }
                                        else
                                        {
                                            var txToAdd = new MultisigSubsig(itemObj["pk"] as byte[]);
                                            ret.Add(txToAdd);
                                        }

                                    }
                                    property.SetValue(instance, ret);
                                    continue; // do not continue to next switch
                            }
                            break;
                        case "ICollection`1":
                            switch (property.Name)
                            {
                                case "InnerTxns":
                                    var array = value as Object[];
                                    var ret = new List<SignedTransaction>() { };
                                    foreach (var item in array)
                                    {
                                        var itemObj = item as Dictionary<object, object?>;
                                        if (itemObj == null)
                                        {
                                            throw new Exception("InnerTxns cannot be deserialized");
                                        }
                                        var txToAdd = itemObj.Deserialize<SignedTransaction>();
                                        ret.Add(txToAdd);

                                    }
                                    property.SetValue(instance, ret.ToArray());
                                    continue; // do not continue to next switch
                                case "ApplicationArgs":
                                    var arrayApplicationArgs = value as Object[];
                                    var retApplicationArgs = new List<byte[]>() { };
                                    foreach (var item in arrayApplicationArgs)
                                    {
                                        var itemObj = item as byte[];
                                        if (itemObj == null)
                                        {
                                            throw new Exception("ApplicationArgs cannot be deserialized");
                                        }
                                        retApplicationArgs.Add(itemObj);

                                    }
                                    property.SetValue(instance, retApplicationArgs.ToArray());
                                    continue; // do not continue to next switch
                                case "Accounts":
                                    var arrayAccounts = value as Object[];
                                    var retAccounts = new List<Address>() { };
                                    foreach (var item in arrayAccounts)
                                    {
                                        var itemObj = item as byte[];
                                        if (itemObj == null)
                                        {
                                            throw new Exception("ApplicationArgs cannot be deserialized");
                                        }
                                        retAccounts.Add(new Address(itemObj));

                                    }
                                    property.SetValue(instance, retAccounts.ToArray());
                                    continue; // do not continue to next switch
                                case "Boxes":
                                    var arrayBoxes = value as Object[];
                                    var retBoxes = new List<BoxRef>() { };
                                    foreach (var item in arrayBoxes)
                                    {
                                        var itemObj = item as Dictionary<object, object?>;
                                        if (itemObj == null)
                                        {
                                            throw new Exception("ApplicationArgs cannot be deserialized");
                                        }
                                        retBoxes.Add(itemObj.Deserialize<BoxRef>());
                                    }
                                    property.SetValue(instance, retBoxes.ToArray());
                                    continue; // do not continue to next switch
                                case "ForeignAssets":
                                case "ForeignApps":
                                    var arrayInts = value as Object[];
                                    var retInts = new List<ulong>() { };
                                    foreach (var item in arrayInts)
                                    {
                                        retInts.Add(Convert.ToUInt64(item));
                                    }
                                    property.SetValue(instance, retInts.ToArray());
                                    continue; // do not continue to next switch
                                default:
                                    throw new Exception($"Not implemented list {property.Name}");
                            }

                    }

                    switch (property.PropertyType.FullName)
                    {
                        case "Algorand.Algod.Model.Transactions.Transaction":
                            var txDict = value as Dictionary<object, object?>;
                            if (txDict == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }

                            switch (txDict["type"])
                            {
                                case "acfg":
                                    if (txDict.ContainsKey("caid"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<AssetUpdateTransaction>());
                                    }
                                    else
                                    {
                                        property.SetValue(instance, txDict.Deserialize<AssetCreateTransaction>());
                                    }
                                    break;
                                case "afrz":
                                    property.SetValue(instance, txDict.Deserialize<AssetFreezeTransaction>());
                                    break;
                                case "appl":
                                    if (txDict.ContainsKey("apan"))
                                    {
                                        switch (txDict["apan"]?.ToString())
                                        {
                                            case "0":
                                            case "noop":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationNoopTransaction>());
                                                break;
                                            case "1":
                                            case "optin":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationOptInTransaction>());
                                                break;
                                            case "2":
                                            case "aclose":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationCloseOutTransaction>());
                                                break;
                                            case "3":
                                            case "clear":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationClearStateTransaction>());
                                                break;
                                            case "4":
                                            case "update":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationUpdateTransaction>());
                                                break;
                                            case "5":
                                            case "delete":
                                                property.SetValue(instance, txDict.Deserialize<ApplicationDeleteTransaction>());
                                                break;
                                            default:
                                                property.SetValue(instance, txDict.Deserialize<ApplicationNoopTransaction>());
                                                break;
                                        }
                                    }
                                    else if (txDict.ContainsKey("apap") && txDict.ContainsKey("apid"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<ApplicationUpdateTransaction>());
                                    }
                                    else if (txDict.ContainsKey("apap"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<ApplicationCreateTransaction>());
                                    }
                                    else
                                    {
                                        property.SetValue(instance, txDict.Deserialize<ApplicationNoopTransaction>());
                                    }
                                    break;
                                case "axfer":
                                    if (txDict.ContainsKey("asnd"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<AssetClawbackTransaction>());
                                    }
                                    else if (txDict.ContainsKey("aclose"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<AssetCloseTransaction>());
                                    }
                                    else
                                    {
                                        property.SetValue(instance, txDict.Deserialize<AssetTransferTransaction>());
                                    }
                                    break;
                                case "hb":
                                    property.SetValue(instance, txDict.Deserialize<HeartBeatTransaction>());
                                    break;
                                case "keyreg":
                                    if (txDict.ContainsKey("selkey"))
                                    {
                                        property.SetValue(instance, txDict.Deserialize<KeyRegisterOnlineTransaction>());
                                    }
                                    else
                                    {
                                        property.SetValue(instance, txDict.Deserialize<KeyRegisterOfflineTransaction>());
                                    }
                                    break;
                                case "pay":
                                    property.SetValue(instance, txDict.Deserialize<PaymentTransaction>());
                                    break;
                                case "stpf":
                                    property.SetValue(instance, txDict.Deserialize<StateProofTransaction>());
                                    break;
                            }
                            break;
                        case "Algorand.Signature":
                            property.SetValue(instance, new Algorand.Signature(value as byte[]));
                            break;
                        case "Algorand.Address":
                            property.SetValue(instance, new Algorand.Address(value as byte[]));
                            break;
                        case "Algorand.Digest":
                            property.SetValue(instance, new Algorand.Digest(value as byte[]));
                            break;
                        case "Algorand.ParticipationPublicKey":
                            property.SetValue(instance, new Algorand.ParticipationPublicKey(value as byte[]));
                            break;
                        case "Algorand.TEALProgram":
                            property.SetValue(instance, new Algorand.TEALProgram(value as byte[]));
                            break;
                        case "Algorand.VRFPublicKey":
                            property.SetValue(instance, new Algorand.VRFPublicKey(value as byte[]));
                            break;
                        case "Algorand.Algod.Model.AssetParams":
                            var txDictAssetParams = value as Dictionary<object, object?>;
                            if (txDictAssetParams == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictAssetParams.Deserialize<Algorand.Algod.Model.AssetParams>());
                            break;
                        case "Algorand.Algod.Model.Transactions.SignedTransactionDetail":
                            var txDictSignedTransactionDetail = value as Dictionary<object, object?>;
                            if (txDictSignedTransactionDetail == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictSignedTransactionDetail.Deserialize<Algorand.Algod.Model.Transactions.SignedTransactionDetail>());
                            break;
                        case "Algorand.Algod.Model.Transactions.StateSchema":
                            var txDictStateSchema = value as Dictionary<object, object?>;
                            if (txDictStateSchema == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictStateSchema.Deserialize<Algorand.Algod.Model.Transactions.StateSchema>());
                            break;
                        case "Algorand.LogicsigSignature":
                            var txDictLogicsigSignature = value as Dictionary<object, object?>;
                            if (txDictLogicsigSignature == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictLogicsigSignature.Deserialize<Algorand.LogicsigSignature>());
                            break;
                        case "Algorand.MultisigSignature":
                            var txDictMultisigSignature = value as Dictionary<object, object?>;
                            if (txDictMultisigSignature == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictMultisigSignature.Deserialize<Algorand.MultisigSignature>());
                            break;
                        case "Algorand.MultisigSubsig":
                            var txDictMultisigSubsig = value as Dictionary<object, object?>;
                            if (txDictMultisigSubsig == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictMultisigSubsig.Deserialize<Algorand.MultisigSubsig>());
                            break;
                        case "Algorand.Algod.Model.HeartBeat":
                            var txDictHeartBeat = value as Dictionary<object, object?>;
                            if (txDictHeartBeat == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictHeartBeat.Deserialize<Algorand.Algod.Model.HeartBeat>());
                            break;
                        case "Algorand.Algod.Model.BlockCertVoteSig":
                            var txDictBlockCertVoteSig = value as Dictionary<object, object?>;
                            if (txDictBlockCertVoteSig == null)
                            {
                                throw new InvalidOperationException($"Expected a dictionary for property {property.Name}, but got {value?.GetType().FullName}");
                            }
                            property.SetValue(instance, txDictBlockCertVoteSig.Deserialize<Algorand.Algod.Model.BlockCertVoteSig>());
                            break;
                        case "System.UInt64":
                            property.SetValue(instance, Convert.ToUInt64(value));
                            break;
                        case "System.UInt32":
                            property.SetValue(instance, Convert.ToUInt32(value));
                            break;
                        case "System.UInt16":
                            property.SetValue(instance, Convert.ToUInt16(value));
                            break;
                        case "System.Object":
                            property.SetValue(instance, value);
                            break;
                        case "System.Byte[]":
                            var bytes = value as byte[];
                            property.SetValue(instance, bytes);
                            break;
                        default:

                            switch (property.PropertyType.GenericTypeArguments.FirstOrDefault()?.FullName)
                            {
                                case "System.Object":
                                    if (property.PropertyType.GenericTypeArguments.Length == 2)
                                    {
                                        var name2 = property.PropertyType.GenericTypeArguments.Skip(1).First().Name;
                                        if (name2 == "ValueDelta")
                                        {
                                            // dictionary of state change.. first item is string, second value delta

                                            var dictGS = new Dictionary<object, ValueDelta>();
                                            if (value is Dictionary<object, object?> valueDict)
                                            {
                                                foreach (var item in valueDict)
                                                {
                                                    var itemValueDict = item.Value as Dictionary<object, object?>;
                                                    if (itemValueDict != null)
                                                    {
                                                        dictGS[item.Key] = itemValueDict.Deserialize<ValueDelta>();
                                                    }
                                                }
                                            }
                                            property.SetValue(instance, dictGS);
                                        }
                                        else
                                        {
                                            property.SetValue(instance, value);
                                        }
                                    }
                                    else
                                    {
                                        property.SetValue(instance, value);
                                    }
                                    break;
                                case "System.UInt64":
                                    if (property.PropertyType.GenericTypeArguments.Length == 2)
                                    {
                                        if (property.Name == "LocalDelta")
                                        {
                                            // dictionary<ulong,dictionary<object,valuedelta>>
                                            var localDeltaOut = new Dictionary<ulong, Dictionary<object, ValueDelta>>();

                                            if (value is Dictionary<object, object?> valueDict)
                                            {
                                                foreach (var item in valueDict)
                                                {
                                                    if (item.Value is Dictionary<object, object?> itemValueDict)
                                                    {
                                                        var dictGS = new Dictionary<object, ValueDelta>();

                                                        foreach (var item2 in itemValueDict)
                                                        {
                                                            if (item2.Value is Dictionary<object, object?> itemValueDictDict)
                                                            {
                                                                dictGS[item2.Key] = itemValueDictDict.Deserialize<ValueDelta>();
                                                            }
                                                        }
                                                        localDeltaOut[Convert.ToUInt64(item.Key)] = dictGS;
                                                    }
                                                }
                                            }

                                            property.SetValue(instance, localDeltaOut);

                                        }
                                        else
                                        {
                                            property.SetValue(instance, Convert.ToUInt64(value));

                                        }
                                    }
                                    else
                                    {
                                        property.SetValue(instance, Convert.ToUInt64(value));
                                    }
                                    break;
                                case "System.UInt32":
                                    property.SetValue(instance, Convert.ToUInt32(value));
                                    break;
                                case "System.UInt16":
                                    property.SetValue(instance, Convert.ToUInt16(value));
                                    break;
                                default:
                                    property.SetValue(instance, value);
                                    break;
                            }

                            break;
                    }
                }
            }
            return instance;
        }
    }
}
