using System;
using System.Threading.Tasks;

namespace sdk_examples
{
    public class Program
    {
        // Dispatcher to run selected example based on environment variable or first arg.
        public static async Task Main(string[] args)
        {
            var example = Environment.GetEnvironmentVariable("EXAMPLE") ?? (args.Length > 0 ? args[0] : "BasicExample");
            switch (example)
            {
                case nameof(BasicExample):
                    await BasicExample.Main(args);
                    break;
                case nameof(ConnectToGossipNetwork):
                    await ConnectToGossipNetwork.Main(args);
                    break;
                case nameof(AccountExample):
                    await AccountExample.Main(args);
                    break;
                case nameof(LogicSignatureExample):
                    await LogicSignatureExample.Main(args);
                    break;
                case nameof(DryrunDebuggingExample):
                    await DryrunDebuggingExample.Main(args);
                    break;
                case nameof(LogicSignatureContractAccountExample):
                    await LogicSignatureContractAccountExample.Main(args);
                    break;
                case nameof(AssetExample):
                    await AssetExample.Main(args);
                    break;
                case nameof(SimpleBoxExample):
                    await SimpleBoxExample.Main(args);
                    break;
                case nameof(StatefulContractExample):
                    await StatefulContractExample.Main(args);
                    break;
                case nameof(IndexerExample):
                    await IndexerExample.Main(args);
                    break;
                case nameof(KMDExample):
                    await KMDExample.Main(args);
                    break;
                case nameof(RekeyExample):
                    await RekeyExample.Main(args);
                    break;
                case nameof(CompileTeal):
                    await CompileTeal.Main(args);
                    break;
                case nameof(AtomicTransferExample):
                    await AtomicTransferExample.Main(args);
                    break;
                case nameof(MultisigExample):
                    await MultisigExample.Main(args);
                    break;
                case nameof(KeyRegistrationExample):
                    await KeyRegistrationExample.Main(args);
                    break;
                case nameof(SimulateTransactionExample):
                    await SimulateTransactionExample.Main(args);
                    break;
                case nameof(NodeAndBlockExample):
                    await NodeAndBlockExample.Main(args);
                    break;
                case nameof(OfflineSigningExample):
                    await OfflineSigningExample.Main(args);
                    break;
                default:
                    Console.WriteLine($"Unknown example '{example}'. Defaulting to BasicExample.");
                    await BasicExample.Main(args);
                    break;
            }
        }
    }
}
