using specflow.StepDefinitions;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using Algorand.Algod.Model.Transactions;
using Algorand.Algod.Model;
using Algorand;
using System.Collections.Generic;
using Algorand.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace algorand_tests.StepDefinitions
{
    [Binding]
    public class SendingTransactionsStepDefinitions
    {
        ScenarioContext _scenarioContext;

        public SendingTransactionsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"the transaction should go through")]
        public void ThenTheTransactionShouldGoThrough()
        {
            bool error = (bool)_scenarioContext["error"];
            error.Should().BeFalse();
        }

        [Given(@"default multisig transaction with parameters (.*) ""([^""]*)""")]
        public async Task GivenDefaultMultisigTransactionWithParameters(int p0, string p1)
        {
            List<string> addresses = (List<string>)_scenarioContext["accounts"];
            var multiSigAddress = new MultisigAddress(1, 1, addresses.Select(a => new Algorand.Address(a).Bytes ).ToList() ).ToAddress();
            var suggestedParms = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();

            PaymentTransaction transaction = new PaymentTransaction()
            {
                Fee = 1000,
                FirstValid = suggestedParms.LastRound,
                LastValid = suggestedParms.LastRound + 1000,
                GenesisHash = new Digest(suggestedParms.GenesisHash),
                Receiver = new Address(addresses[0]),
                Amount = 1,
                GenesisId = suggestedParms.GenesisId,
                Sender = multiSigAddress
            };

            _scenarioContext["multisig"]=transaction;

        }

        [When(@"I sign the multisig transaction with the private key")]
        public void WhenISignTheMultisigTransactionWithThePrivateKey()
        {
            var transaction = (Transaction)_scenarioContext["multisig"];
            var account = (Account)_scenarioContext["privateAccount"];
            SignedTransaction st = transaction.Sign(account);
            _scenarioContext["signedTransaction"] = st;
        }

        [When(@"I send the multisig transaction")]
        public async Task WhenISendTheMultisigTransaction()
        {
            bool error = false;
            SignedTransaction st = (SignedTransaction)_scenarioContext["signedTransaction"];
            try
            {
                var id = (await Utils.SubmitTransaction(httpUtilities.algodDefaultApiInstance, st)).Txid;
                _scenarioContext["submittedTxnid"] = id;
            }
            catch (Exception)
            {
                error = true;
            }
            _scenarioContext["error"] = error;
        }

        [Then(@"the transaction should not go through")]
        public void ThenTheTransactionShouldNotGoThrough()
        {
            bool error = (bool)_scenarioContext["error"];
            error.Should().BeTrue();
        }

        [Given(@"default V(.*) key registration transaction ""([^""]*)""")]
        public async Task GivenDefaultVKeyRegistrationTransaction(int p0, string online)
        {
            var suggestedParms = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();

            // Marking an account non-participating is a one-way, protocol-level operation: once a "nonparticipation"
            // scenario has run against an address, that address can never send another key registration transaction
            // again, so reusing the wallet's shared addresses[0] here would permanently break these scenarios on
            // any long-lived LocalNet after the first "nonparticipation" run. Use a dedicated, freshly generated
            // (and funded) account instead, so these scenarios stay repeatable regardless of prior runs.
            List<string> addresses = (List<string>)_scenarioContext["accounts"];
            string keyRegAddress = await GetOrCreateFundedKeyRegAccount(addresses);
            addresses = new List<string> { keyRegAddress };
            _scenarioContext["accounts"] = addresses;

            KeyRegistrationTransaction? txn=null;
            switch (online)
            {
                case "online":
                    txn = new KeyRegisterOnlineTransaction()
                    {
                        FirstValid = suggestedParms.LastRound,
                        LastValid = suggestedParms.LastRound + 1000,
                        GenesisHash = new Digest(suggestedParms.GenesisHash),
                        GenesisId = suggestedParms.GenesisId,

                        Fee =1000,
                        Sender = new Address(addresses[0]),
                        NonParticipation=false,
                        Votepk = new ParticipationPublicKey(Convert.FromBase64String("9mr13Ri8rFepxN3ghIUrZNui6LqqM5hEzB45Rri5lkU=")),
                        SelectionPk = new VRFPublicKey(Convert.FromBase64String("dx717L3uOIIb/jr9OIyls1l5Ei00NFgRa380w7TnPr4=")),
                        VoteFirst=suggestedParms.LastRound,
                        VoteLast=suggestedParms.LastRound + 20000,
                        VoteKeyDilution=10000,
                        StateProofPK=Convert.FromBase64String("mYR0GVEObMTSNdsKM6RwYywHYPqVDqg3E4JFzxZOreH9NU8B+tKzUanyY8AQ144hETgSMX7fXWwjBdHz6AWk9w==")

                    };
                    break;
                case "nonparticipation":
                     txn = new KeyRegisterOnlineTransaction()
                    {
                         Fee = 1000,
                         Sender = new Address(addresses[0]),
                         FirstValid = suggestedParms.LastRound,
                         LastValid = suggestedParms.LastRound + 1000,
                         GenesisHash = new Digest(suggestedParms.GenesisHash),
                         GenesisId = suggestedParms.GenesisId,
                         NonParticipation = true,

                    };
                    break;
                case "offline":
                     txn = new KeyRegisterOfflineTransaction()
                    {
                         Fee = 1000,
                         Sender = new Address(addresses[0]),
                         FirstValid = suggestedParms.LastRound,
                         LastValid = suggestedParms.LastRound + 1000,
                         GenesisHash = new Digest(suggestedParms.GenesisHash),
                         GenesisId = suggestedParms.GenesisId,
                     };
                    break;
            }

            txn.Should().NotBeNull();
            _scenarioContext["transaction"] = txn;


        }

        private const string WalletName = "unencrypted-default-wallet";
        private const string WalletPassword = "";

        private async Task<string> GetOrCreateFundedKeyRegAccount(List<string> candidateFunders)
        {
            var wallets = await httpUtilities.kmdApi.ListWalletsAsync(new object());
            var wallet = wallets.Wallets.Where(w => w.Name == WalletName).FirstOrDefault();
            var handle = await httpUtilities.kmdApi.InitWalletHandleTokenAsync(new Algorand.KMD.InitWalletHandleTokenRequest() { Wallet_id = wallet!.Id, Wallet_password = WalletPassword });

            var generated = await httpUtilities.kmdApi.GenerateKeyAsync(new Algorand.KMD.GenerateKeyRequest() { Wallet_handle_token = handle.Wallet_handle_token });
            var newAddress = generated.Address;

            // Wallet addresses vary widely in balance (some are small utility accounts, others hold the LocalNet
            // genesis funds), so fund from whichever candidate currently has the most ALGO rather than assuming
            // addresses[0] is well-funded.
            string? funderAddress = null;
            ulong bestBalance = 0;
            foreach (var candidate in candidateFunders)
            {
                var info = await httpUtilities.algodDefaultApiInstance.AccountInformationAsync(candidate, null, null);
                if (funderAddress == null || info.Amount > bestBalance)
                {
                    funderAddress = candidate;
                    bestBalance = info.Amount;
                }
            }

            var funderExport = await httpUtilities.kmdApi.ExportKeyAsync(new Algorand.KMD.ExportKeyRequest() { Address = funderAddress!, Wallet_handle_token = handle.Wallet_handle_token, Wallet_password = WalletPassword });
            var funder = new Account(funderExport.Private_key);

            var suggestedParms = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();
            var fundingTxn = PaymentTransaction.GetPaymentTransactionFromNetworkTransactionParameters(funder.Address, new Address(newAddress), 1_000_000, "", suggestedParms);
            var signed = fundingTxn.Sign(funder);
            var resp = await Utils.SubmitTransaction(httpUtilities.algodDefaultApiInstance, signed);
            await Utils.WaitTransactionToComplete(httpUtilities.algodDefaultApiInstance, resp.Txid);

            return newAddress;
        }
    }
}
