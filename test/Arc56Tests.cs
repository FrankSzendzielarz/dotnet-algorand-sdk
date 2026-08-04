using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;
using Algorand.AVM.ClientGenerator.ABI.ARC56;
using Algorand.KMD;
using Algorand.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static AVMTypes.AvmTypesProxy.Structs;

namespace test
{
    [TestFixture]
    public class Arc56Tests
    {

        [Test]
        public async Task GenerateXgovRegistryClient()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/algorandfoundation/xgov-beta-sc/refs/heads/main/smart_contracts/artifacts/xgov_registry/XGovRegistry.arc56.json");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("XGovRegistry");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("XGovRegistryProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("XGovRegistryProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateXgovProposalClient()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/algorandfoundation/xgov-beta-sc/refs/heads/main/smart_contracts/artifacts/proposal/Proposal.arc56.json");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("XGovProposal");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("XGovProposalProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("XGovProposalProxy.cs", appProxy);
        }


        [Test]
        public async Task GenerateARC1400Client()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/arc-1400/refs/heads/main/projects/arc-1400/smart_contracts/artifacts/security_token/Arc1644.arc56.json");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("ARC1400");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("ARC1400Proxy.cs", appProxy);
            CompileAndPublishGeneratedClient("ARC1400Proxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateAVMTypesClient()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/AVMTypes/refs/heads/main/projects/AVMTypes/smart_contracts/artifacts/avm_types/AvmTypes.arc56.json");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("AVMTypes");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("AVMTypesProxy.cs", appProxy);

            // Compile the freshly generated source code to prove it is valid, standalone C#
            var assembly = CompileGeneratedClient(appProxy, "AVMTypesGenerated_" + Guid.NewGuid().ToString("N"));
            var proxyType = assembly.GetType("AVMTypes.AvmTypesProxy");
            Assert.That(proxyType, Is.Not.Null, "Could not locate generated AvmTypesProxy type in the compiled assembly");
            CompileAndPublishGeneratedClient("AVMTypesProxy.cs", appProxy);

            // Exercise the freshly compiled client against a running node to prove it actually works
            Account acct1 = await GetAccount();
            dynamic generatedContract = Activator.CreateInstance(proxyType, algodApiInstance, (ulong)0);
            await generatedContract.CreateApplication(acct1, (ulong)1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);

            Assert.That((ulong)generatedContract.appId, Is.GreaterThan(0UL));
            Assert.That((Address)generatedContract.AppAddress, Is.Not.Null);

            byte byteResult = await generatedContract.Arc4Byte((byte)255, acct1, (ulong)1000);
            Assert.That(byteResult, Is.EqualTo(255));

            bool boolResult = await generatedContract.Boolean(true, acct1, (ulong)1000);
            Assert.That(boolResult, Is.True);
        }

        private static Assembly CompileGeneratedClient(string sourceCode, string assemblyName)
            => GeneratedClientCompiler.CompileGeneratedClient(sourceCode, assemblyName);

        private static void CompileAndPublishGeneratedClient(string fileName, string appProxy)
            => GeneratedClientCompiler.CompileAndPublishGeneratedClient(fileName, appProxy);

        [Test]
        public async Task GenerateARC200Client()
        {
            using var client = new HttpClient();
            //var response = await client.GetAsync("https://raw.githubusercontent.com/SatishGAXL/arc200-ts/refs/heads/main/projects/arc200-ts/contracts/artifacts/arc200.arc56_draft.json");
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/arc200/refs/heads/main/contracts/artifacts/Arc200.arc56.json");
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("ARC200");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("ARC200Proxy.cs", appProxy);
            CompileAndPublishGeneratedClient("ARC200Proxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientAmm()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecClammPool.arc56.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BiatecClammPoolArc56");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("Arc56BiatecClammPoolProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("Arc56BiatecClammPoolProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientPP()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecPoolProvider.arc56.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BiatecPoolProviderArc56");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("Arc56BiatecPoolProviderProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("Arc56BiatecPoolProviderProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientConf()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecConfigProvider.arc56.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BiatecConfigArc56");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("Arc56BiatecConfigProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("Arc56BiatecConfigProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientBI()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecIdentityProvider.arc56.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BiatecIdentityArc56");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("Arc56BiatecIdentityProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("Arc56BiatecIdentityProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateClientGasStation()
        {
            var content = File.ReadAllText("Arc56/GasStation.arc56.json");
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("AVMGasStation.GeneratedClients");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            File.WriteAllText("GasStationProxy.cs", appProxy);
            CompileAndPublishGeneratedClient("GasStationProxy.cs", appProxy);
        }

        // LocalNet only genesis-funds the accounts in the "unencrypted-default-wallet" wallet;
        // other wallets (e.g. AlgoKit's "DEPLOYER") start empty and only hold whatever a prior
        // test run happened to send them. KMD's ListWalletsAsync has no guaranteed ordering, so
        // picking the first wallet returned can silently select an unfunded one.
        private const string FundedWalletName = "unencrypted-default-wallet";
        // Contract-deployment tests in this fixture fund app accounts and pay fees repeatedly,
        // so require a healthy safety margin rather than just "greater than zero".
        private const ulong MinAccountBalanceMicroAlgos = 1_000_000_000_000UL; // 1,000,000 Algos
        private const ulong DispenseAmountMicroAlgos = 2_000_000_000_000UL; // 2,000,000 Algos

        private async Task<Account> GetAccount(int index = 0)
        {

            //A standard sandbox connection
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var kmdApi = new Api(client);
            kmdApi.BaseUrl = @"http://localhost:4002";
            var handle = await getWalletHandleToken(kmdApi);
            var accs = await kmdApi.ListKeysInWalletAsync(new ListKeysRequest() { Wallet_handle_token = handle });
            while (accs.Addresses.Count <= index)
            {
                await kmdApi.GenerateKeyAsync(new GenerateKeyRequest() { Wallet_handle_token = handle });
                accs = await kmdApi.ListKeysInWalletAsync(new ListKeysRequest() { Wallet_handle_token = handle });
            }
            var a = accs.Addresses.Reverse().Skip(index).First();
            var resp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = a, Wallet_handle_token = handle, Wallet_password = "" });
            var account = new Account(resp.Private_key);

            await EnsureFundedAsync(account, accs.Addresses, kmdApi, handle);

            return account;
        }
        private static async Task<string> getWalletHandleToken(Api kmdApi)
        {
            var wallets = await kmdApi.ListWalletsAsync(null);
            var wallet = wallets.Wallets.FirstOrDefault(w => w.Name == FundedWalletName) ?? wallets.Wallets.FirstOrDefault();
            var handle = await kmdApi.InitWalletHandleTokenAsync(new InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = "" });
            return handle.Wallet_handle_token;
        }

        // KMD key order isn't stable across runs, and newly generated keys start unfunded, so
        // `GetAccount` can hand back an account with a near-zero balance. Top it up from whichever
        // key in the same wallet currently holds the most funds before handing it to the caller.
        private async Task EnsureFundedAsync(Account account, ICollection<string> walletAddresses, Api kmdApi, string handle)
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            var algodApiInstance = new DefaultApi(httpClient);

            var address = account.Address.ToString();
            var info = await algodApiInstance.AccountInformationAsync(address, null, null);
            if (info.Amount >= MinAccountBalanceMicroAlgos) return;

            string dispenserAddress = null;
            ulong dispenserBalance = 0;
            foreach (var candidateAddress in walletAddresses)
            {
                if (candidateAddress == address) continue;
                var candidateInfo = await algodApiInstance.AccountInformationAsync(candidateAddress, null, null);
                if (candidateInfo.Amount > dispenserBalance)
                {
                    dispenserBalance = candidateInfo.Amount;
                    dispenserAddress = candidateAddress;
                }
            }
            if (dispenserAddress == null || dispenserBalance < DispenseAmountMicroAlgos)
            {
                throw new InvalidOperationException(
                    $"LocalNet KMD wallet has no account funded with at least {DispenseAmountMicroAlgos} microAlgos to dispense to {address}. " +
                    "Restart AlgoKit LocalNet (`algokit localnet reset`) to restore genesis-funded accounts.");
            }

            var keyResp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = dispenserAddress, Wallet_handle_token = handle, Wallet_password = "" });
            var dispenser = new Account(keyResp.Private_key);
            await FundAccount.PayTo(account.Address, DispenseAmountMicroAlgos, dispenser, algodApiInstance);
        }
        [Test]
        public void ObjectConversionToByteArray()
        {
            var obj = new BiatecIdentityArc56.BiatecIdentityProviderProxy.Structs.IdentityInfo()
            {
                VerificationStatus = 1,
                VerificationClass = 2,
                IsCompany = true,
                PersonUuid = "00000000-0000-0000-0000-000000000000",
                LegalEntityUuid = "00000000-0000-0000-0000-000000000000",
                BiatecEngagementPoints = 3,
                BiatecEngagementRank = 4,
                AvmEngagementPoints = 5,
                AvmEngagementRank = 6,
                TradingEngagementPoints = 7,
                TradingEngagementRank = 8,
                IsLocked = true,
                KycExpiration = 9,
                InvestorForExpiration = 10,
                IsProfessionalInvestor = true,
                FeeMultiplier = 2_000_000_000,
                FeeMultiplierBase = 1_000_000_000,

            };
            var data = BitConverter.ToString(obj.ToByteArray()).Replace("-", "").ToLower();
            Assert.That(data, Is.EqualTo("0000000000000002800000000077359400000000003b9aca000000000000000009000000000000000a0000000000000001800067008d00000000000000030000000000000004000000000000000500000000000000060000000000000007000000000000000880002430303030303030302d303030302d303030302d303030302d303030303030303030303030002430303030303030302d303030302d303030302d303030302d303030303030303030303030"));

            obj = new BiatecIdentityArc56.BiatecIdentityProviderProxy.Structs.IdentityInfo()
            {
                VerificationStatus = 1,
                VerificationClass = 2,
                IsCompany = false,
                PersonUuid = "00000000-0000-0000-0000-000000000000",
                LegalEntityUuid = "00000000-0000-0000-0000-000000000000",
                BiatecEngagementPoints = 3,
                BiatecEngagementRank = 4,
                AvmEngagementPoints = 5,
                AvmEngagementRank = 6,
                TradingEngagementPoints = 7,
                TradingEngagementRank = 8,
                IsLocked = false,
                KycExpiration = 9,
                InvestorForExpiration = 10,
                IsProfessionalInvestor = false,
                FeeMultiplier = 2_000_000_000,
                FeeMultiplierBase = 1_000_000_000,

            };
            data = BitConverter.ToString(obj.ToByteArray()).Replace("-", "").ToLower();
            Assert.That(data, Is.EqualTo("0000000000000002000000000077359400000000003b9aca000000000000000009000000000000000a0000000000000001000067008d00000000000000030000000000000004000000000000000500000000000000060000000000000007000000000000000800002430303030303030302d303030302d303030302d303030302d303030303030303030303030002430303030303030302d303030302d303030302d303030302d303030303030303030303030"));

            obj = new BiatecIdentityArc56.BiatecIdentityProviderProxy.Structs.IdentityInfo()
            {
                LegalEntityUuid = "00000000-0000-0000-0000-000000000000",
                PersonUuid = "00000000-0000-0000-0000-000000000000",
                VerificationStatus = 1

            };
            data = BitConverter.ToString(obj.ToByteArray()).Replace("-", "").ToLower();
            Assert.That(data, Is.EqualTo("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000067008d00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000002430303030303030302d303030302d303030302d303030302d303030303030303030303030002430303030303030302d303030302d303030302d303030302d303030303030303030303030"));
        }

        [Test]
        public async Task DeployBI()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var trans = await algodApiInstance.TransactionParamsAsync();
            Account acct1 = await GetAccount();

            var contractConf = new BiatecConfigArc56.BiatecConfigProviderProxy(algodApiInstance, 0);
            var contractBI = new BiatecIdentityArc56.BiatecIdentityProviderProxy(algodApiInstance, 0);
            var contractPP = new BiatecPoolProviderArc56.BiatecPoolProviderProxy(algodApiInstance, 0);
            try
            {
                await contractConf.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
                await contractBI.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
                await contractPP.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);

                await contractConf.Bootstrap(
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    biatecFee: new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1),
                    appBiatecIdentityProvider: contractBI.appId,
                    appBiatecPoolProvider: contractPP.appId,
                    _tx_note: "",
                    _tx_boxes: new List<BoxRef>()
                    );
                await contractPP.Bootstrap(
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    appBiatecConfigProvider: contractConf.appId,
                    _tx_note: "",
                    _tx_apps: new List<ulong>() { contractConf.appId }
                    );
                await contractBI.Bootstrap(
                    governor: acct1.Address,
                    verificationSetter: acct1.Address,
                    engagementSetter: acct1.Address,
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    appBiatecConfigProvider: contractConf.appId,
                    _tx_note: "",
                    _tx_apps: new List<ulong>() { contractConf.appId }
                    );
                
                Console.WriteLine($"contractConf: {contractConf.appId}");
                Console.WriteLine($"contractBI: {contractBI.appId}");

                var transParams = await algodApiInstance.TransactionParamsAsync();
                var tx = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, contractBI.AppAddress, 1_000_000, "", transParams);
                var signedTx = tx.Sign(acct1);

                var id = await Utils.SubmitTransaction(algodApiInstance, signedTx);

                byte[] prefix = new byte[] { (byte)'i' };
                byte[] box = prefix.Concat(acct1.Address.Bytes).ToArray();
                var ulongV = (ulong)DateTimeOffset.Now.Ticks;
                await contractBI.SelfRegistration(acct1.Address, new BiatecIdentityArc56.BiatecIdentityProviderProxy.Structs.IdentityInfo()
                {
                    LegalEntityUuid = "00000000-0000-0000-0000-000000000000",
                    PersonUuid = "00000000-0000-0000-0000-000000000000",
                    VerificationStatus = 1,
                    IsCompany = false,
                    AvmEngagementPoints = 0,
                    IsLocked = false,
                    KycExpiration = 0,
                    FeeMultiplierBase = 1_000_000_000,
                    FeeMultiplier = 2_000_000_000
                }, acct1, 1000, _tx_apps: new List<ulong>() { contractConf.appId, contractBI.appId },
                _tx_boxes: new List<BoxRef>()
                {
                    new BoxRef()
                    {
                        App = 0,
                        Name = box,

                    }
                }
                );

                var data = await contractBI.GetUser(acct1.Address, (byte)1, acct1, 1000,
                    _tx_boxes: new List<BoxRef>()
                    {
                        new BoxRef()
                        {
                            App = 0,
                            Name = box,

                        }
                    }
                    );
                Assert.That(data.Version, Is.EqualTo(1));
                Assert.That(data.VerificationStatus, Is.EqualTo(1));
                Assert.That(data.LegalEntityUuid, Is.EqualTo("00000000-0000-0000-0000-000000000000"));
                Assert.That(data.PersonUuid, Is.EqualTo("00000000-0000-0000-0000-000000000000"));

                await contractBI.SetInfo(acct1.Address, new BiatecIdentityArc56.BiatecIdentityProviderProxy.Structs.IdentityInfo()
                {
                    LegalEntityUuid = "00000000-0000-0000-0000-000000000001",
                    PersonUuid = "00000000-0000-0000-0000-000000000002",
                    VerificationStatus = 1,
                    IsCompany = true,
                    AvmEngagementPoints = 123,
                    IsLocked = false,
                    KycExpiration = ulongV,
                    FeeMultiplierBase = 1_000_000_000,
                    FeeMultiplier = 1_000_000_000
                }, acct1, 1000, _tx_apps: new List<ulong>() { contractConf.appId, contractBI.appId },
                _tx_boxes: new List<BoxRef>()
                {
                    new BoxRef()
                    {
                        App = 0,
                        Name = box,

                    }
                }
                );

                data = await contractBI.GetUser(acct1.Address, (byte)1, acct1, 1000,
                   _tx_boxes: new List<BoxRef>()
                   {
                        new BoxRef()
                        {
                            App = 0,
                            Name = box,

                        }
                   }
                   );
                Assert.That(data.Version, Is.EqualTo(1));
                Assert.That(data.VerificationStatus, Is.EqualTo(1));
                Assert.That(data.LegalEntityUuid, Is.EqualTo("00000000-0000-0000-0000-000000000001"));
                Assert.That(data.PersonUuid, Is.EqualTo("00000000-0000-0000-0000-000000000002"));
                Assert.That(data.IsCompany, Is.EqualTo(true));
                Assert.That(data.IsLocked, Is.EqualTo(false));
                Assert.That(data.AvmEngagementPoints, Is.EqualTo(123));
                Assert.That(data.KycExpiration, Is.EqualTo(ulongV));

            }
            catch (Algorand.ApiException<Algorand.Algod.Model.ErrorResponse> e)
            {
                Trace.TraceError(e.Message);
                throw;
            }
            catch (Algorand.ApiException e)
            {

                Trace.TraceError(e.Message);
                throw;
            }
            catch (AVM.ClientGenerator.ProxyException e)
            {
                var eApi = e.InnerException as Algorand.ApiException<Algorand.Algod.Model.ErrorResponse>;
                if (eApi != null)
                {
                    Trace.TraceError(eApi.Result.Message.ToString());
                }
                Trace.TraceError(e.Message);
                throw;
            }
        }
        [Test]
        public async Task AVMTypesTests()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var trans = await algodApiInstance.TransactionParamsAsync();
            Account acct1 = await GetAccount();

            var contractConf = new AVMTypes.AvmTypesProxy(algodApiInstance, 0);
            await contractConf.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);

            Assert.That(contractConf.appId, Is.GreaterThan(0));
            Assert.That(contractConf.AppAddress, Is.Not.Null);

            Assert.That(await contractConf.Arc4Byte(255, acct1, 1000), Is.EqualTo(255));

            Assert.That(await contractConf.Arc4Byte(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4Byte(255, acct1, 1000), Is.EqualTo(255));

            Assert.That(await contractConf.Boolean(false, acct1, 1000), Is.EqualTo(false));
            Assert.That(await contractConf.Boolean(true, acct1, 1000), Is.EqualTo(true));


            Assert.That(await contractConf.Arc4Bool(false, acct1, 1000), Is.EqualTo(false));
            Assert.That(await contractConf.Arc4Bool(true, acct1, 1000), Is.EqualTo(true));


            Assert.That(await contractConf.Arc4UintN8(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4UintN8(255, acct1, 1000), Is.EqualTo(255));
            Assert.That(await contractConf.Arc4UintN16Alias(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4UintN16Alias(65535, acct1, 1000), Is.EqualTo(65535));

            Assert.That(await contractConf.Arc4UintN64Alias(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4UintN64Alias(18446744073709551615, acct1, 1000), Is.EqualTo(18446744073709551615));
            var UInt256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(BigInteger.Parse("0"));
            Assert.That(await contractConf.Arc4UintN256Alias(UInt256, acct1, 1000), Is.EqualTo(UInt256));
            UInt256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(BigInteger.Parse("18446744073709551616"));
            Assert.That(await contractConf.Arc4UintN256Alias(UInt256, acct1, 1000), Is.EqualTo(UInt256));

            var UInt512 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt512(BigInteger.Parse("13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084095"));
            Assert.That(await contractConf.Arc4UintN512(UInt512, acct1, 1000), Is.EqualTo(UInt512));
            UInt512 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt512(BigInteger.Parse("0"));
            Assert.That(await contractConf.Arc4UintN512(UInt512, acct1, 1000), Is.EqualTo(UInt512));


            var UInt128 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt128(BigInteger.Parse("18446744073709551616"));
            Assert.That(await contractConf.Arc4UintN128Alias(UInt128, acct1, 1000), Is.EqualTo(UInt128));
            UInt128 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt128(BigInteger.Parse("0"));
            Assert.That(await contractConf.Arc4UintN128Alias(UInt128, acct1, 1000), Is.EqualTo(UInt128));

            Assert.That(await contractConf.Arc4UintN16Alias(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4UintN16Alias(65535, acct1, 1000), Is.EqualTo(65535));
            Assert.That(await contractConf.Arc4UintN8Alias(0, acct1, 1000), Is.EqualTo(0));
            Assert.That(await contractConf.Arc4UintN8Alias(255, acct1, 1000), Is.EqualTo(255));
            var bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.That(await contractConf.Arc4StaticBytes8(bytes, acct1, 1000), Is.EqualTo(bytes));
            Assert.That(await contractConf.Arc4DynamicBytes(bytes, acct1, 1000), Is.EqualTo(bytes));
            Assert.That(await contractConf.Bytes(bytes, acct1, 1000), Is.EqualTo(bytes));
            bytes = new byte[32];
            RandomNumberGenerator.Fill(bytes);
            Assert.That(await contractConf.Arc4StaticBytes32(bytes, acct1, 1000), Is.EqualTo(bytes));
            Assert.That(await contractConf.Arc4DynamicBytes(bytes, acct1, 1000), Is.EqualTo(bytes));
            Assert.That(await contractConf.Bytes(bytes, acct1, 1000), Is.EqualTo(bytes));
            bytes = new byte[1018];
            RandomNumberGenerator.Fill(bytes);
            Assert.That(await contractConf.Arc4DynamicBytes(bytes, acct1, 1000), Is.EqualTo(bytes));

            // Unlike arc4DynamicBytes, the "bytes" ARC56 method also emits a matching ARC-28 "bytes" event (see
            // its `events` entry in AvmTypes.arc56.json), and AVM caps the *total* bytes logged by a single app
            // call at 1024 (log() opcode budget). A single-arg event's ARC4 tuple-wrapping adds a 2-byte offset
            // header on top of the dynamic byte[]'s own 2-byte length prefix, so the "bytes" event log alone is
            // 4 (selector) + 2 (tuple offset) + 2 (length) + N bytes - for N=1018 that's already 1026 bytes,
            // over budget before the ARC4 return-value log is even appended. Use a smaller payload here so both
            // logs fit within the 1024-byte total.
            var smallBytes = new byte[500];
            RandomNumberGenerator.Fill(smallBytes);
            Assert.That(await contractConf.Bytes(smallBytes, acct1, 1000), Is.EqualTo(smallBytes));

            bytes = new byte[1020];
            RandomNumberGenerator.Fill(bytes);
            Assert.That(await contractConf.Arc4StaticBytes1020(bytes, acct1, 1000), Is.EqualTo(bytes));

            Assert.That(await contractConf.String("Test", acct1, 1000), Is.EqualTo("Test"));

            Assert.That(await contractConf.StringArray(["Hello", "world"], acct1, 1000), Is.EqualTo(["Hello", "world"]));


            bytes = new byte[] { 1, 2 };
            Assert.That(await contractConf.Arc4StaticArrayOf2Bytes(bytes, acct1, 1000), Is.EqualTo(bytes));


            Assert.That(await contractConf.Arc4Address(acct1.Address, acct1, 1000), Is.EqualTo(acct1.Address));


            var innerStruct = new InnerStruct()
            {
                Num = 1,
                Struct = new StructAddressUint256()
                {
                    Address = acct1.Address,
                    Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1)
                }
            };
            Assert.That(await contractConf.InnerStruct(innerStruct, acct1, 1000), Is.EqualTo(innerStruct));

            /**/
        }
        [Test]
        public async Task Arc200Tests()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var trans = await algodApiInstance.TransactionParamsAsync();
            Account acct1 = await GetAccount();
            Account acct2 = await GetAccount(1);

            var contractConf = new ARC200.Arc200Proxy(algodApiInstance, 0);
            await contractConf.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
            await acct1.MakePaymentTo(contractConf.AppAddress, 1_000_000, "", algodApiInstance); // fund MBR

            Assert.That(contractConf.appId, Is.GreaterThan(0));
            Assert.That(contractConf.AppAddress, Is.Not.Null);
            var boxes = new List<BoxRef>()
                {
                    new BoxRef()
                    {
                        App = 0,
                        Name = Encoding.ASCII.GetBytes("b").Concat(acct1.Address.Bytes).ToArray(),
                    },
                    new BoxRef()
                    {
                        App = 0,
                        Name = Encoding.ASCII.GetBytes("b").Concat(acct2.Address.Bytes).ToArray(),
                    }
                };
            await contractConf.Bootstrap(
                name: Encoding.ASCII.GetBytes("MyToken"),
                symbol: Encoding.ASCII.GetBytes("T"),
                decimals: 6,
                totalSupply: new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(BigInteger.Parse("10000000000") * BigInteger.Parse("1000000")),
                _tx_sender: acct1,
                _tx_fee: 1000,
                _tx_note: "",
                _tx_boxes: boxes
                );
            var am = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256();

            var balance = await contractConf.Arc200BalanceOf(acct1.Address, acct1, 1000, _tx_boxes: boxes);
            Assert.That(balance, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(BigInteger.Parse("10000000000") * BigInteger.Parse("1000000"))));

            await contractConf.Arc200Transfer(acct2.Address, AVM.ClientGenerator.ABI.ARC4.Types.UInt256.FromValue(1_000_000_000), acct1, 1000, _tx_boxes: boxes);

            balance = await contractConf.Arc200BalanceOf(acct2.Address, acct1, 1000, _tx_boxes: boxes);
            Assert.That(balance, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(BigInteger.Parse("1000000000"))));

        }

        private async Task<(AVMTypes.AvmTypesProxy contract, Account acct)> CreateAvmTypesContract()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            Account acct1 = await GetAccount();

            var contract = new AVMTypes.AvmTypesProxy(algodApiInstance, 0);
            await contract.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
            return (contract, acct1);
        }

        // ufixed<N>x<M> is ABI-encoded identically to uint<N>, so the client represents it as the raw scaled
        // BigInteger value (e.g. ufixed8x16 value "1.5" would be encoded as BigInteger 1 << 16 | ...); here we
        // just round-trip raw scaled integers to prove the bitwidth-aware encode/decode path works end to end.
        [Test]
        public async Task AVMTypes_Arc4UFixed8x16()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            Assert.That(await contract.Arc4UFixed8X16(BigInteger.Zero, acct1, 1000), Is.EqualTo(BigInteger.Zero));
            Assert.That(await contract.Arc4UFixed8X16(new BigInteger(255), acct1, 1000), Is.EqualTo(new BigInteger(255)));
        }

        [Test]
        public async Task AVMTypes_Arc4UFixed512x160()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            Assert.That(await contract.Arc4UFixed512X160(BigInteger.Zero, acct1, 1000), Is.EqualTo(BigInteger.Zero));
            var big = BigInteger.Parse("13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084095");
            Assert.That(await contract.Arc4UFixed512X160(big, acct1, 1000), Is.EqualTo(big));
        }

        [Test]
        public async Task AVMTypes_Arc4UintN82Tuple()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.Arc4UintN82TupleArgData() { Field0 = 1, Field1 = 255 };
            var result = await contract.Arc4UintN82Tuple(data, acct1, 1000);
            Assert.That(result.Field0, Is.EqualTo(1));
            Assert.That(result.Field1, Is.EqualTo(255));
        }

        [Test]
        public async Task AVMTypes_Arc4UintN83Tuple()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.Arc4UintN83TupleArgData() { Field0 = 1, Field1 = 2, Field2 = 255 };
            var result = await contract.Arc4UintN83Tuple(data, acct1, 1000);
            Assert.That(result.Field0, Is.EqualTo(1));
            Assert.That(result.Field1, Is.EqualTo(2));
            Assert.That(result.Field2, Is.EqualTo(255));
        }

        [Test]
        public async Task AVMTypes_Arc4ComplexTuple()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.Arc4ComplexTupleArgData()
            {
                Field0 = acct1.Address,
                Field1 = new AVMTypes.AvmTypesProxy.Structs.Arc4ComplexTupleArgDataField1()
                {
                    Field0 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1),
                    Field1 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(2),
                },
                Field2 = new AVMTypes.AvmTypesProxy.Structs.Arc4ComplexTupleArgDataField1()
                {
                    Field0 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(3),
                    Field1 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(4),
                },
                Field3 = new AVMTypes.AvmTypesProxy.Structs.Arc4ComplexTupleArgDataField1()
                {
                    Field0 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(5),
                    Field1 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(6),
                },
            };
            var result = await contract.Arc4ComplexTuple(data, acct1, 1000);
            Assert.That(result.Field0, Is.EqualTo(acct1.Address));
            Assert.That(result.Field1.Field0, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1)));
            Assert.That(result.Field1.Field1, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(2)));
            Assert.That(result.Field2.Field0, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(3)));
            Assert.That(result.Field3.Field1, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(6)));
        }

        [Test]
        public async Task AVMTypes_Arc4Tuple()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.Arc4TupleArgData() { Field0 = 12345, Field1 = "hello arc4 tuple" };
            var result = await contract.Arc4Tuple(data, acct1, 1000);
            Assert.That(result.Field0, Is.EqualTo(12345));
            Assert.That(result.Field1, Is.EqualTo("hello arc4 tuple"));
        }

        [Test]
        public async Task AVMTypes_NativeTuple()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.NativeTupleArgData() { Field0 = 6789, Field1 = "native tuple", Field2 = true };
            var result = await contract.NativeTuple(data, acct1, 1000);
            Assert.That(result.Field0, Is.EqualTo(6789));
            Assert.That(result.Field1, Is.EqualTo("native tuple"));
            Assert.That(result.Field2, Is.True);
        }

        [Test]
        public async Task AVMTypes_Arc4DynamicArrayOfStruct()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256[]
            {
                new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1) },
                new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(2) },
                new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(3) },
            };
            var result = await contract.Arc4DynamicArrayOfStruct(data, acct1, 1000);
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].Uint256, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1)));
            Assert.That(result[2].Uint256, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(3)));
        }

        [Test]
        public async Task AVMTypes_Arc4StaticArrayOf2Structs()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var data = new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256[]
            {
                new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(10) },
                new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(20) },
            };
            var result = await contract.Arc4StaticArrayOf2Structs(data, acct1, 1000);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result[0].Uint256, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(10)));
            Assert.That(result[1].Uint256, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(20)));
        }

        [Test]
        public async Task AVMTypes_Events_PrimitiveAndStruct()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            await contract.String("event test", acct1, 1000);
            var stringLog = contract.LastCallLogs.FirstOrDefault(l => AVMTypes.AvmTypesProxy.Events.StringEvent.Matches(l));
            Assert.That(stringLog, Is.Not.Null, "Expected a StringEvent log entry");
            var stringEvent = AVMTypes.AvmTypesProxy.Events.StringEvent.Decode(stringLog);
            Assert.That(stringEvent.Field0, Is.EqualTo("event test"));

            var structData = new AVMTypes.AvmTypesProxy.Structs.StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(42) };
            await contract.Struct(structData, acct1, 1000);
            var structLog = contract.LastCallLogs.FirstOrDefault(l => AVMTypes.AvmTypesProxy.Events.StructEvent.Matches(l));
            Assert.That(structLog, Is.Not.Null, "Expected a StructEvent log entry");
            var structEvent = AVMTypes.AvmTypesProxy.Events.StructEvent.Decode(structLog);
            Assert.That(structEvent.Field0.Address, Is.EqualTo(acct1.Address));
            Assert.That(structEvent.Field0.Uint256, Is.EqualTo(new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(42)));
        }

        [Test]
        public async Task AVMTypes_InnerStruct()
        {
            var (contract, acct1) = await CreateAvmTypesContract();

            var innerStruct = new InnerStruct()
            {
                Num = 1,
                Struct = new StructAddressUint256()
                {
                    Address = acct1.Address,
                    Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1)
                }
            };
            Assert.That(await contract.InnerStruct(innerStruct, acct1, 1000), Is.EqualTo(innerStruct));
        }

        private async Task<(AVMTypes.AvmTypesProxy contract, Account acct, DefaultApi algodApiInstance)> CreateAvmTypesContractWithApi()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            Account acct1 = await GetAccount();

            var contract = new AVMTypes.AvmTypesProxy(algodApiInstance, 0);
            await contract.CreateApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
            return (contract, acct1, algodApiInstance);
        }

        private async Task<ulong> CreateTestAsset(DefaultApi algodApiInstance, Account acct1)
        {
            var transParams = await algodApiInstance.TransactionParamsAsync();
            var assetCreate = new AssetCreateTransaction
            {
                Sender = acct1.Address,
                AssetParams = new Algorand.Algod.Model.AssetParams { Total = 1000, Decimals = 0, UnitName = "TST", Name = "Test Asset", DefaultFrozen = false }
            };
            assetCreate.FillInParams(transParams);
            var signed = assetCreate.Sign(acct1);
            await Utils.SubmitTransaction(algodApiInstance, signed);
            var confirmed = await Utils.WaitTransactionToComplete(algodApiInstance, assetCreate.TxID()) as AssetCreateTransaction;
            return confirmed?.AssetIndex ?? throw new Exception("Asset index missing after creation");
        }

        [Test]
        public async Task AVMTypes_StringReadonly()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.StringReadonly("readonly", acct1, 1000), Is.EqualTo("readonly"));
        }

        [Test]
        public async Task AVMTypes_Biguint()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var value = new AVM.ClientGenerator.ABI.ARC4.Types.UInt512(BigInteger.Parse("123456789012345678901234567890"));
            Assert.That(await contract.Biguint(value, acct1, 1000), Is.EqualTo(value));
        }

        [Test]
        public async Task AVMTypes_Struct()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var data = new StructAddressUint256() { Address = acct1.Address, Uint256 = new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(7) };
            Assert.That(await contract.Struct(data, acct1, 1000), Is.EqualTo(data));
        }

        [Test]
        public async Task AVMTypes_Uint64()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.Uint64(0, acct1, 1000), Is.EqualTo(0UL));
            Assert.That(await contract.Uint64(18446744073709551615UL, acct1, 1000), Is.EqualTo(18446744073709551615UL));
        }

        [Test]
        public async Task AVMTypes_Uint64Array()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var data = new ulong[] { 1, 2, 3, ulong.MaxValue };
            Assert.That(await contract.Uint64Array(data, acct1, 1000), Is.EqualTo(data));
        }

        [Test]
        public async Task AVMTypes_BooleanArray()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var data = new bool[] { true, false, true };
            Assert.That(await contract.BooleanArray(data, acct1, 1000), Is.EqualTo(data));
        }

        [Test]
        public async Task AVMTypes_Arc4Str()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.Arc4Str("arc4 string", acct1, 1000), Is.EqualTo("arc4 string"));
        }

        [Test]
        public async Task AVMTypes_FixedArrayUint64()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var data = new ulong[] { 10, 20, 30 };
            Assert.That(await contract.FixedArrayUint64(data, acct1, 1000), Is.EqualTo(data));
        }

        [Test]
        public async Task AVMTypes_Account()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.Account(acct1.Address, acct1, 1000), Is.EqualTo(acct1.Address));
        }

        [Test]
        public async Task AVMTypes_Asset()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.Asset(12345UL, acct1, 1000), Is.EqualTo(12345UL));
        }

        [Test]
        public async Task AVMTypes_Application()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.Application(contract.appId, acct1, 1000), Is.EqualTo(contract.appId));
        }

        [Test]
        public async Task AVMTypes_AccountIndexed()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.AccountIndexed(acct1.Address, acct1, 1000), Is.EqualTo(acct1.Address));
        }

        [Test]
        public async Task AVMTypes_AssetIndexed()
        {
            var (contract, acct1, algodApiInstance) = await CreateAvmTypesContractWithApi();
            var assetId = await CreateTestAsset(algodApiInstance, acct1);
            Assert.That(await contract.AssetIndexed(assetId, acct1, 1000, _tx_assets: new List<ulong> { assetId }), Is.EqualTo(assetId));
        }

        [Test]
        public async Task AVMTypes_ApplicationIndexed()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            Assert.That(await contract.ApplicationIndexed(contract.appId, acct1, 1000, _tx_apps: new List<ulong> { contract.appId }), Is.EqualTo(contract.appId));
        }

        [Test]
        public async Task AVMTypes_PaymentTxn()
        {
            var (contract, acct1, algodApiInstance) = await CreateAvmTypesContractWithApi();
            var transParams = await algodApiInstance.TransactionParamsAsync();
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, acct1.Address, 0, "", transParams);

            var result = await contract.PaymentTxn(payment, acct1, 1000);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(32));
        }

        [Test]
        public async Task AVMTypes_Transaction()
        {
            var (contract, acct1, algodApiInstance) = await CreateAvmTypesContractWithApi();
            var transParams = await algodApiInstance.TransactionParamsAsync();
            var payment = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, acct1.Address, 0, "", transParams);

            var result = await contract.Transaction(payment, acct1, 1000);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(32));
        }

        [Test]
        public async Task AVMTypes_ApplicationCallTxn()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var innerTxns = await contract.Boolean_Transactions(true, acct1, 1000);
            var innerAppCall = innerTxns[0] as ApplicationCallTransaction;
            Assert.That(innerAppCall, Is.Not.Null);

            var result = await contract.ApplicationCallTxn(innerAppCall, acct1, 1000);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(32));
        }

        [Test]
        public async Task AVMTypes_AssetTransferTxn()
        {
            var (contract, acct1, algodApiInstance) = await CreateAvmTypesContractWithApi();
            var assetId = await CreateTestAsset(algodApiInstance, acct1);

            var axfer = new AssetTransferTransaction()
            {
                Sender = acct1.Address,
                AssetReceiver = acct1.Address,
                XferAsset = assetId,
                AssetAmount = 0
            };

            var result = await contract.AssetTransferTxn(axfer, acct1, 1000, _tx_assets: new List<ulong> { assetId });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(32));
        }

        [Test]
        public async Task AVMTypes_KeyRegistrationTxn()
        {
            var (contract, acct1) = await CreateAvmTypesContract();
            var keyReg = new KeyRegisterOfflineTransaction() { Sender = acct1.Address };

            var result = await contract.KeyRegistrationTxn(keyReg, acct1, 1000);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(32));
        }

        // Regression coverage for a batch of Arc56Registry-reported generator crashes/compile failures, all
        // rooted in the same handful of bugs: FixedArray<T>/VariableArray<T> required `T : WireType, new()`, which
        // blew up (either a runtime TypeLoadException inside the generator, or a CS0310 in the generated output)
        // for any element type without a public parameterless constructor - a parametric-bitwidth uintN/ufixedNxM,
        // or a nested FixedArray/VariableArray used as another array's element (e.g. "byte[64][]"). Fixing that
        // uncovered further latent bugs in raw-tuple-array and struct-array field handling once the generator
        // stopped crashing before ever reaching that code. Each of these downloads the exact spec from the
        // Arc56Registry report and proves the generator produces valid, compilable C# for it - see
        // https://github.com/scholtz/dotnet-algorand-sdk/issues (search "Arc56Registry" reports around 2026-07-16).
        private static async Task<string> DownloadArc56Spec(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Failed to download {url}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");
            return content;
        }

        [Test]
        public async Task GenerateTruthRegistryClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/0xYuvi/TrustAnchor/HEAD/projects/TrustAnchor-contracts/smart_contracts/artifacts/truth_registry/TruthRegistry.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("TruthRegistryRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("TruthRegistryProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateAbstractedAccountFactoryClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/akita-protocol/akita-sc/HEAD/projects/akita-sc/smart_contracts/artifacts/arc58/account/AbstractedAccountFactory.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("AbstractedAccountFactoryRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("AbstractedAccountFactoryProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateAkitaDAOTypesClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/akita-protocol/akita-sc/HEAD/projects/akita-sc/smart_contracts/artifacts/arc58/dao/AkitaDAOTypes.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("AkitaDAOTypesRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("AkitaDAOTypesProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateMerkleAssetGateClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/akita-protocol/akita-sc/HEAD/projects/akita-sc/smart_contracts/artifacts/gates/sub-gates/merkle-asset/MerkleAssetGate.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("MerkleAssetGateRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("MerkleAssetGateProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateStakingPoolPluginClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/akita-protocol/akita-sc/HEAD/projects/akita-sc/smart_contracts/artifacts/arc58/plugins/staking-pool/StakingPoolPlugin.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("StakingPoolPluginRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("StakingPoolPluginProxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateARC55Client()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/algorandfoundation/arc55-encryption/HEAD/smart_contracts/artifacts/multisig/ARC55.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("ARC55Regression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("ARC55Proxy.cs", appProxy);
        }

        [Test]
        public async Task GenerateMultisigClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/algorandfoundation/arc55-encryption/HEAD/smart_contracts/artifacts/multisig/Multisig.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("MultisigRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("MultisigProxy.cs", appProxy);
        }

        // Regression tests for incident BizKorE_12a3f575: generated code used
        // `new VariableArray<Byte>(x)` with a non-string argument, which does not compile
        // (CS1729 - VariableArray<T>'s only 1-arg constructor takes a string ABI element spec).
        [Test]
        public async Task GenerateBizKorEClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/A-Maugli/akt03/HEAD/biz_kor/projects/biz_kor-contracts/smart_contracts/artifacts/biz_kor/BizKorE.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BizKorERegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("BizKorEProxy.cs", appProxy);
        }

        // Regression test for incident WhisperBid_ad7b42ec (see BizKorE_12a3f575 above for root cause).
        [Test]
        public async Task GenerateWhisperBidClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/0M3REXE/whisperbid/HEAD/projects/whisperbid-contracts/smart_contracts/artifacts/whisper_bid/WhisperBid.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("WhisperBidRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("WhisperBidProxy.cs", appProxy);
        }

        // Regression test for incident IdentityRegistry_8b7065c3 (see BizKorE_12a3f575 above for root cause).
        [Test]
        public async Task GenerateIdentityRegistryClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/0xYuvi/TrustAnchor/HEAD/projects/TrustAnchor-contracts/smart_contracts/artifacts/identity_registry/IdentityRegistry.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("IdentityRegistryRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("IdentityRegistryProxy.cs", appProxy);
        }

        // Regression test for incident TrustAnchor_3ac8ef3e (see BizKorE_12a3f575 above for root cause).
        [Test]
        public async Task GenerateTrustAnchorClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/0xYuvi/TrustAnchor/HEAD/projects/TrustAnchor-contracts/smart_contracts/artifacts/trust_anchor/TrustAnchor.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("TrustAnchorRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("TrustAnchorProxy.cs", appProxy);
        }

        // Regression test for incident VerifiedProtocol_6470122f (see BizKorE_12a3f575 above for root cause).
        [Test]
        public async Task GenerateVerifiedProtocolClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/Aarti-panchal01/verifi.ed/HEAD/smart_contracts/artifacts/verified_protocol/VerifiedProtocol.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("VerifiedProtocolRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("VerifiedProtocolProxy.cs", appProxy);
        }

        // Regression test for incident BountyEscrowContract_0ac88497 (see BizKorE_12a3f575 above for root cause).
        [Test]
        public async Task GenerateBountyEscrowContractClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/AnuragAJ25/BaseCase-Web3-Algorand/HEAD/contracts/bounty_escrow/backend/contract_artifacts/BountyEscrowContract.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("BountyEscrowContractRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("BountyEscrowContractProxy.cs", appProxy);
        }

        // Regression test for incident CadenciaEscrow_08254c5a: this spec puts the compiled bytecode under a
        // snake_case "byte_code" key instead of the ARC-56 spec's camelCase "byteCode", which left
        // AppDescriptionArc56.ByteCode null and crashed ToProxy with a NullReferenceException while computing
        // ExtraProgramPages from Contract.ByteCode.Approval.
        [Test]
        public async Task GenerateCadenciaEscrowClient()
        {
            var content = await DownloadArc56Spec("https://raw.githubusercontent.com/AdityaWagh19/Cadencia-A2A-Platform/HEAD/backend/artifacts/CadenciaEscrow.arc56.json");

            var generator = new ClientGeneratorARC56();
            generator.LoadFromByteArray(Encoding.UTF8.GetBytes(content));
            var appProxy = await generator.ToProxy("CadenciaEscrowRegression");
            Assert.That(appProxy.Length, Is.GreaterThan(1));
            CompileAndPublishGeneratedClient("CadenciaEscrowProxy.cs", appProxy);
        }
    }
}
