using specflow.StepDefinitions;
using System;
using System.Threading.Tasks;
using System.Linq;
using TechTalk.SpecFlow;
using FluentAssertions;
using Algorand.Algod.Model.Transactions;
using Algorand.Algod.Model;
using Algorand;
using System.Collections.Generic;
using System.Text;
using Algorand.Utils;

namespace algorand_tests.StepDefinitions
{
    [Binding]
    public class AlgodStepDefinitions
    {

        ScenarioContext _scenarioContext;
        string walletName = "unencrypted-default-wallet";
        string walletPswd = "";
        public AlgodStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"an algod client")]
        public async Task GivenAnAlgodClient()
        {
            httpUtilities.setUp();
            httpUtilities.setUpKmd();
            await getDefaultWalletAsync();
        }

        [Then(@"the node should be healthy")]
        public async Task ThenTheNodeShouldBeHealthy()
        {
            bool error = false;
            try
            {

                await httpUtilities.algodDefaultApiInstance.HealthCheckAsync();
            }
            catch (Exception)
            {
                error = true;
            }

            error.Should().BeFalse();

        }

        [When(@"I get the status")]
        public async Task WhenIGetTheStatus()
        {

            var status = await httpUtilities.algodDefaultApiInstance.GetStatusAsync();

            _scenarioContext["status"] = status;

        }


        private async Task selfPayTransaction()
        {
            var suggestedParms = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();

            List<string> addresses = (List<string>)_scenarioContext["accounts"];

            PaymentTransaction transaction = new PaymentTransaction()
            {
                Fee = 1000,
                FirstValid = suggestedParms.LastRound,
                LastValid = suggestedParms.LastRound + 1000,
                GenesisHash = new Digest(suggestedParms.GenesisHash),
                Receiver = new Address(addresses[0]),
                Amount = 1,
                GenesisId = suggestedParms.GenesisId,
                Sender = new Address(addresses[0])

            };

            _scenarioContext["transaction"] = transaction;
            string handle = await getWalletHandleTokenAsync();
            var resp = await httpUtilities.kmdApi.ExportKeyAsync(new Algorand.KMD.ExportKeyRequest() { Address = addresses[0], Wallet_handle_token = handle, Wallet_password = walletPswd });
            //Convert.FromBase64String(
            Account account = new Account(resp.Private_key);

            SignedTransaction st = transaction.Sign(account);

            await httpUtilities.algodDefaultApiInstance.TransactionsAsync(new List<SignedTransaction> { st });

        }

        [When(@"I get status after this block")]
        public async Task WhenIGetStatusAfterThisBlock()
        {
            bool error = false;
            try
            {
                NodeStatusResponse? status = (NodeStatusResponse?)_scenarioContext["status"];

                status.Should().NotBeNull();

                await selfPayTransaction();

                await httpUtilities.algodDefaultApiInstance.WaitForBlockAsync(status!.LastRound);
            }
            catch (Exception)
            {
                error = true;
            }
            error.Should().BeFalse();
        }

        [Then(@"I can get the block info")]
        public async Task ThenICanGetTheBlockInfo()
        {
            bool error = false;
            try
            {
                NodeStatusResponse? status = (NodeStatusResponse?)_scenarioContext["status"];

                status.Should().NotBeNull();

                await httpUtilities.algodDefaultApiInstance.GetBlockAsync(status!.LastRound, null);
            }
            catch (Exception)
            {
                error = true;
            }
            error.Should().BeFalse();
        }

        [Then(@"I get the ledger supply")]
        public async Task ThenIGetTheLedgerSupply()
        {
            bool error = false;
            try
            {
                await httpUtilities.algodDefaultApiInstance.GetSupplyAsync();
            }
            catch (Exception)
            {
                error = true;
            }
            error.Should().BeFalse();

        }

        [Given(@"a kmd client")]
        public async Task GivenAKmdClient()
        {
            httpUtilities.setUpKmd();
            await getDefaultWalletAsync();
        }

        [Given(@"wallet information")]
        public async Task GivenWalletInformation()
        {
            await getDefaultWalletAsync();

        }




        private async Task getDefaultWalletAsync()
        {

            string handle = await getWalletHandleTokenAsync();

            var accs = await httpUtilities.kmdApi.ListKeysInWalletAsync(new Algorand.KMD.ListKeysRequest() { Wallet_handle_token = handle });
            accs.Should().NotBeNull();
            accs.Addresses.Should().NotBeNull();
            accs.Addresses.Count.Should().BeGreaterThan(0);

            // Steps in this fixture (and SendingTransactionsStepDefinitions) always use addresses[0] as the
            // sender. Wallet addresses vary widely in balance (some are small utility accounts, others hold the
            // LocalNet genesis funds) and KMD's key order isn't guaranteed, so put whichever candidate currently
            // has the most ALGO first rather than assuming addresses[0] is well-funded.
            var addresses = accs.Addresses.ToList();
            string richestAddress = addresses[0];
            ulong richestBalance = 0;
            foreach (var candidate in addresses)
            {
                var info = await httpUtilities.algodDefaultApiInstance.AccountInformationAsync(candidate, null, null);
                if (info.Amount > richestBalance)
                {
                    richestBalance = info.Amount;
                    richestAddress = candidate;
                }
            }
            addresses.Remove(richestAddress);
            addresses.Insert(0, richestAddress);

            _scenarioContext["accounts"] = addresses;
        }

        private async Task<string> getWalletHandleTokenAsync()
        {
            var wallets = await httpUtilities.kmdApi.ListWalletsAsync(new object());
            wallets.Should().NotBeNull();

            var wallet = wallets.Wallets.Where(w => w.Name == walletName).FirstOrDefault();
            wallet.Should().NotBeNull();

            _scenarioContext["wallet"] = wallet;

            var handle = await httpUtilities.kmdApi.InitWalletHandleTokenAsync(new Algorand.KMD.InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = walletPswd });
            handle.Should().NotBeNull();


            return handle.Wallet_handle_token;
        }

        [Then(@"I get transactions by address and round")]
        public void ThenIGetTransactionsByAddressAndRound()
        {
            //not valid for v2 api
            return;
        }

        [Then(@"I get transactions by address only")]
        public async Task ThenIGetTransactionsByAddressOnly()
        {

            List<string> addresses = (List<string>)_scenarioContext["accounts"];
            var res = await httpUtilities.algodDefaultApiInstance.GetPendingTransactionsByAddressAsync(addresses[0], null, 0);
        }

        [Then(@"I get transactions by address and date")]
        public void ThenIGetTransactionsByAddressAndDate()
        {
            //not implemented for v2 api
            return;
        }

        [Given(@"default transaction with parameters (.*) ""([^""]*)""")]
        public async Task GivenDefaultTransactionWithParameters(int p0, string none)
        {
            var suggestedParms = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();


            List<string> addresses = (List<string>)_scenarioContext["accounts"];
            PaymentTransaction transaction = new PaymentTransaction()
            {

                Fee = 1000,
                FirstValid = suggestedParms.LastRound,
                LastValid = suggestedParms.LastRound + 1000,
                GenesisHash = new Digest(suggestedParms.GenesisHash),
                Receiver = new Address(addresses[0]),
                Amount = (ulong)p0,
                GenesisId = suggestedParms.GenesisId,
                Sender = new Address(addresses[0]),
                Note = Encoding.UTF8.GetBytes(none)

            };

            _scenarioContext["transaction"] = transaction;


        }

        [When(@"I get the private key")]
        public async Task WhenIGetThePrivateKey()
        {
            List<string> addresses = (List<string>)_scenarioContext["accounts"];
            string handle = await getWalletHandleTokenAsync();
            var resp = await httpUtilities.kmdApi.ExportKeyAsync(new Algorand.KMD.ExportKeyRequest() { Address = addresses[0], Wallet_handle_token = handle, Wallet_password = walletPswd });
            //Convert.FromBase64String
            Account account = new Account(resp.Private_key);
            _scenarioContext["privateAccount"] = account;

        }

        [When(@"I sign the transaction with the private key")]
        public void WhenISignTheTransactionWithThePrivateKey()
        {
            var transaction = (Transaction)_scenarioContext["transaction"];
            var account = (Account)_scenarioContext["privateAccount"];
            SignedTransaction st = transaction.Sign(account);
            _scenarioContext["signedTransaction"] = st;

        }


        [When(@"I send the transaction")]
        public async Task WhenISendTheTransaction()
        {
            _scenarioContext["error"] = false;
            SignedTransaction st = (SignedTransaction)_scenarioContext["signedTransaction"];

            var id = (await Utils.SubmitTransaction(httpUtilities.algodDefaultApiInstance, st)).Txid;
            _scenarioContext["submittedTxnid"] = id;

        }

        [Then(@"I can get the transaction by ID")]
        public async Task ThenICanGetTheTransactionByID()
        {
            string txid = (string)_scenarioContext["submittedTxnid"];

            var resp = await Utils.WaitTransactionToComplete(httpUtilities.algodDefaultApiInstance, txid) as Transaction;
        }

        [Then(@"I get pending transactions")]
        public async Task ThenIGetPendingTransactions()
        {
            await httpUtilities.algodDefaultApiInstance.GetPendingTransactionsAsync(null, null);
        }

        [When(@"I get recent transactions, limited by (.*) transactions")]
        public void WhenIGetRecentTransactionsLimitedByTransactions(int p0)
        {
            //Legacy function from V1
            return;
        }

        [When(@"I get the suggested params")]
        public void WhenIGetTheSuggestedParams()
        {
            throw new PendingStepException();
        }

        [When(@"I get the suggested fee")]
        public void WhenIGetTheSuggestedFee()
        {
            //deprecated test
            return;
        }

        [Then(@"the fee in the suggested params should equal the suggested fee")]
        public void ThenTheFeeInTheSuggestedParamsShouldEqualTheSuggestedFee()
        {
            //deprecated test
            return;

        }

        [When(@"I get versions with algod")]
        public void WhenIGetVersionsWithAlgod()
        {
            //deprecated test
            return;
        }

        [Then(@"v(.*) should be in the versions")]
        public void ThenVShouldBeInTheVersions(int p0)
        {
            //deprecated test
            return;
        }

        [Then(@"I get account information")]
        public async Task ThenIGetAccountInformation()
        {
            List<string> addresses = (List<string>)_scenarioContext["accounts"];

            await httpUtilities.algodDefaultApiInstance.AccountInformationAsync(addresses[0], null, null);
        }
    }
}
