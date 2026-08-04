using Algorand.Algod;
using Algorand;
using AVM.ClientGenerator.ABI.ARC32;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Algorand.Algod.Model;
using System.Diagnostics;
using Algorand.KMD;
using System.Linq;
using BiatecClammPool;
using BiatecConfig;
using BiatecIdentity;
using BiatecPoolProvider;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class Arc4Tests
    {

        [Test]
        public async Task GenerateClientAmm()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecClammPool.arc32.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var app = await AppDescription.LoadFromByteArray(Encoding.UTF8.GetBytes(content), algodApiInstance);

            Assert.That(app.Bare_call_config.No_op, Is.EqualTo(CallConfig.NEVER));
            Assert.That(app.Hints.Keys.Count, Is.GreaterThan(1));
            Assert.That(app.State.Global, Is.Not.Null);
            Assert.That(app.Contract.Methods.Count, Is.GreaterThan(1));
            Assert.That(app.Source.Approval.Length, Is.GreaterThan(1));

            var appref = app.ToSmartContractReference("BiatecClammPool", "");
            Assert.That(appref.Length, Is.GreaterThan(1));
            var appProxy = app.ToProxy("BiatecClammPool");
            Assert.That(appProxy.Length, Is.GreaterThan(1));

            File.WriteAllText("BiatecClammPoolRef.cs", appref);
            File.WriteAllText("BiatecClammPoolProxy.cs", appProxy);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecClammPoolRef.cs", appref);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecClammPoolProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientPP()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecPoolProvider.arc32.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var app = await AppDescription.LoadFromByteArray(Encoding.UTF8.GetBytes(content), algodApiInstance);

            Assert.That(app.Bare_call_config.No_op, Is.EqualTo(CallConfig.NEVER));
            Assert.That(app.Hints.Keys.Count, Is.GreaterThan(1));
            Assert.That(app.State.Global, Is.Not.Null);
            Assert.That(app.Contract.Methods.Count, Is.GreaterThan(1));
            Assert.That(app.Source.Approval.Length, Is.GreaterThan(1));

            var appref = app.ToSmartContractReference("BiatecPoolProvider", "");
            Assert.That(appref.Length, Is.GreaterThan(1));
            var appProxy = app.ToProxy("BiatecPoolProvider");
            Assert.That(appProxy.Length, Is.GreaterThan(1));

            File.WriteAllText("BiatecPoolProviderRef.cs", appref);
            File.WriteAllText("BiatecPoolProviderProxy.cs", appProxy);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecPoolProviderRef.cs", appref);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecPoolProviderProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientConf()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecConfigProvider.arc32.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var app = await AppDescription.LoadFromByteArray(Encoding.UTF8.GetBytes(content), algodApiInstance);

            Assert.That(app.Bare_call_config.No_op, Is.EqualTo(CallConfig.NEVER));
            Assert.That(app.Hints.Keys.Count, Is.GreaterThan(1));
            Assert.That(app.State.Global, Is.Not.Null);
            Assert.That(app.Contract.Methods.Count, Is.GreaterThan(1));
            Assert.That(app.Source.Approval.Length, Is.GreaterThan(1));

            var appref = app.ToSmartContractReference("BiatecConfig", "");
            Assert.That(appref.Length, Is.GreaterThan(1));
            var appProxy = app.ToProxy("BiatecConfig");
            Assert.That(appProxy.Length, Is.GreaterThan(1));

            File.WriteAllText("BiatecConfigProviderRef.cs", appref);
            File.WriteAllText("BiatecConfigProviderProxy.cs", appProxy);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecConfigProviderRef.cs", appref);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecConfigProviderProxy.cs", appProxy);
        }
        [Test]
        public async Task GenerateClientBI()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://raw.githubusercontent.com/scholtz/BiatecCLAMM/refs/heads/main/contracts/artifacts/BiatecIdentityProvider.arc32.json");

            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Failed to download file");
            var content = await response.Content.ReadAsStringAsync();
            Assert.That(content.Trim().StartsWith("{"), Is.True, "File content is not valid JSON");

            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            var app = await AppDescription.LoadFromByteArray(Encoding.UTF8.GetBytes(content), algodApiInstance);

            Assert.That(app.Bare_call_config.No_op, Is.EqualTo(CallConfig.NEVER));
            Assert.That(app.Hints.Keys.Count, Is.GreaterThan(1));
            Assert.That(app.State.Global, Is.Not.Null);
            Assert.That(app.Contract.Methods.Count, Is.GreaterThan(1));
            Assert.That(app.Source.Approval.Length, Is.GreaterThan(1));

            var appref = app.ToSmartContractReference("BiatecIdentity", "");
            Assert.That(appref.Length, Is.GreaterThan(1));
            var appProxy = app.ToProxy("BiatecIdentity");
            Assert.That(appProxy.Length, Is.GreaterThan(1));

            File.WriteAllText("BiatecIdentityProviderRef.cs", appref);
            File.WriteAllText("BiatecIdentityProviderProxy.cs", appProxy);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecIdentityProviderRef.cs", appref);
            GeneratedClientCompiler.CompileAndPublishGeneratedClient("BiatecIdentityProviderProxy.cs", appProxy);
        }
        private async Task<Account> GetAccount()
        {

            //A standard sandbox connection
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var kmdApi = new Api(client);
            kmdApi.BaseUrl = @"http://localhost:4002";
            var handle = await getWalletHandleToken(kmdApi);
            var accs = await kmdApi.ListKeysInWalletAsync(new ListKeysRequest() { Wallet_handle_token = handle });
            var a = accs.Addresses.First();
            var resp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = a, Wallet_handle_token = handle, Wallet_password = "" });
            return new Account(resp.Private_key);
        }
        private static async Task<string> getWalletHandleToken(Api kmdApi)
        {
            var wallets = await kmdApi.ListWalletsAsync(null);
            var wallet = wallets.Wallets.FirstOrDefault();
            var handle = await kmdApi.InitWalletHandleTokenAsync(new InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = "" });
            return handle.Wallet_handle_token;
        }

        [Test]
        public async Task DeployProxiedApp()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);

            Account acct1 = await GetAccount();


            var contract = new BiatecClammPoolProxy(algodApiInstance, 0);
            try
            {
                await contract.createApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
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
        public async Task DeployAppMakeAppCall()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var trans = await algodApiInstance.TransactionParamsAsync();
            Account acct1 = await GetAccount();


            var contract = new BiatecConfigProviderProxy(algodApiInstance, 0);
            try
            {
                await contract.createApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
                await contract.bootstrap(
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    biatecFee: new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1),
                    appBiatecIdentityProvider: 101,
                    appBiatecPoolProvider: 102,
                    _tx_note: "",
                    _tx_boxes: new List<BoxRef>()
                    );
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
        public async Task DeployBI()
        {
            var ALGOD_API_ADDR = "http://localhost:4001/";
            var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
            DefaultApi algodApiInstance = new DefaultApi(httpClient);
            var trans = await algodApiInstance.TransactionParamsAsync();
            Account acct1 = await GetAccount();

            var contractConf = new BiatecConfigProviderProxy(algodApiInstance, 0);
            var contractBI = new BiatecIdentityProviderProxy(algodApiInstance, 0);
            var contractPP = new BiatecPoolProviderProxy(algodApiInstance, 0);
            try
            {
                await contractConf.createApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
                await contractBI.createApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
                await contractPP.createApplication(acct1, 1000, "", _tx_callType: AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);

                await contractConf.bootstrap(
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    biatecFee: new AVM.ClientGenerator.ABI.ARC4.Types.UInt256(1),
                    appBiatecIdentityProvider: contractBI.appId,
                    appBiatecPoolProvider: contractPP.appId,
                    _tx_note: "",
                    _tx_boxes: new List<BoxRef>()
                    );
                await contractPP.bootstrap(
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    appBiatecConfigProvider: contractConf.appId,
                    _tx_note: "",
                    _tx_apps: new List<ulong>() { contractConf.appId }
                    );
                await contractBI.bootstrap(
                    governor: acct1.Address,
                    verificationSetter: acct1.Address,
                    engagementSetter: acct1.Address,
                    _tx_sender: acct1,
                    _tx_fee: 1000,
                    appBiatecConfigProvider: contractConf.appId,
                    _tx_note: "",
                    _tx_apps: new List<ulong>() { contractConf.appId }
                    );
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

        //[Test]
        //public async Task DeployAppMakeAppCall()
        //{
        //    var ALGOD_API_ADDR = "http://localhost:4001/";
        //    var ALGOD_API_TOKEN = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        //    var httpClient = HttpClientConfigurator.ConfigureHttpClient(ALGOD_API_ADDR, ALGOD_API_TOKEN);
        //    DefaultApi algodApiInstance = new DefaultApi(httpClient);
        //    var trans = await algodApiInstance.TransactionParamsAsync();
        //    Account acct1 = await GetAccount();


        //    var contract = new BiatecClammPoolProxy(algodApiInstance, 0);
        //    try
        //    {
        //        await contract.createApplication(acct1, 1000, "", new List<BoxRef>(), AVM.ClientGenerator.Core.OnCompleteType.CreateApplication);
        //        await contract.bootstrap(
        //            acct1,
        //            2000,
        //            PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(acct1.Address, contract.AppAddress, 400000, "", trans),
        //            assetA: 100,
        //            assetB: 101,
        //            appBiatecConfigProvider: 110,
        //            appBiatecPoolProvider: 111,
        //            fee: 2000,
        //            priceMin: 0,
        //            priceMax: 1000,
        //            currentPrice: 1000,
        //            verificationClass: 2,
        //            note: "",
        //            boxes: new List<BoxRef>()
        //        );
        //    }
        //    catch (Algorand.ApiException<Algorand.Algod.Model.ErrorResponse> e)
        //    {
        //        Trace.TraceError(e.Message);
        //        throw;
        //    }
        //    catch (Algorand.ApiException e)
        //    {

        //        Trace.TraceError(e.Message);
        //        throw;
        //    }
        //    catch (AVM.ClientGenerator.ProxyException e)
        //    {
        //        var eApi = e.InnerException as Algorand.ApiException<Algorand.Algod.Model.ErrorResponse>;
        //        if (eApi != null)
        //        {
        //            Trace.TraceError(eApi.Result.Message.ToString());
        //        }
        //        Trace.TraceError(e.Message);
        //        throw;
        //    }

        //}
        /**/
    }
}
