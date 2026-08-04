#if TEST_DEBUG
namespace algorand_tests.StepDefinitions
{

    // Scoped to @unit scenarios: WhenISignTheTransactionWithThePrivateKey() has an identically-worded
    // (but differently-implemented, context-key-incompatible) counterpart in
    // Integration.AlgodStepDefinitions. Without scoping, SpecFlow can't disambiguate which binding to
    // use for a matching step text and throws BindingException: Ambiguous step definitions found.
    [Binding]
    [Scope(Tag = "unit")]
    public class OfflineStepDefinitions
    {

        ScenarioContext _scenarioContext;

        public OfflineStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I generate a key")]
        public void WhenIGenerateAKey()
        {
            Account x = new Account();
            _scenarioContext["account"] = x;
        }

        [When(@"I decode the address")]
        public void WhenIDecodeTheAddress()
        {
            _scenarioContext["addressAsString"] = ((Account)_scenarioContext["account"]).Address.ToString();
            
        }

        [When(@"I encode the address")]
        public void WhenIEncodeTheAddress()
        {
            Address x = new Address((string)_scenarioContext["addressAsString"]);
            _scenarioContext["address"] = x;
        }

        [Then(@"the address should still be the same")]
        public void ThenTheAddressShouldStillBeTheSame()
        {
            Account a = (Account)_scenarioContext["account"];
            Address ad = (Address)_scenarioContext["address"];

            a.Address.Equals(ad).Should().BeTrue();
        }

        [When(@"I convert the private key back to a mnemonic")]
        public void WhenIConvertThePrivateKeyBackToAMnemonic()
        {
            var x = new Account((string)_scenarioContext["mnemonic"]);
            var newMnemonic = x.ToMnemonic();
            _scenarioContext["newMnemonic"] = newMnemonic ;
        }

        [Then(@"the mnemonic should still be the same as ""([^""]*)""")]
        public void ThenTheMnemonicShouldStillBeTheSameAs(string p0)
        {
            _scenarioContext["newMnemonic"].Should().Be(_scenarioContext["mnemonic"]);
        }

        [Given(@"mnemonic for master derivation key ""([^""]*)""")]
        public void GivenMnemonicForMasterDerivationKey(string mnemonic)
        {
            _scenarioContext["mnemonic"] = mnemonic;
        }

        [When(@"I convert the master derivation key back to a mnemonic")]
        public void WhenIConvertTheMasterDerivationKeyBackToAMnemonic()
        {
            var x = new Account((string)_scenarioContext["mnemonic"]);
            var newMnemonic = x.ToMnemonic();
            _scenarioContext["newMnemonic"] = newMnemonic;
        }

        [When(@"I create the flat fee payment transaction")]
        public void WhenICreateTheFlatFeePaymentTransaction()
        {
            ulong fee = (ulong)_scenarioContext["fee"];
            ulong fv = (ulong)_scenarioContext["fv"];
            ulong lv = (ulong)_scenarioContext["lv"];
            string gh = (string)_scenarioContext["gh"];
            string to = (string)_scenarioContext["to"];
            string close = (string)_scenarioContext["close"];
            ulong amt = (ulong)_scenarioContext["amt"];
            string gen = (string)_scenarioContext["gen"];
            string note = (string)_scenarioContext["note"];

            PaymentTransaction transaction = new PaymentTransaction()
            {
                Fee=fee,
                FirstValid = fv,
                LastValid = lv,
                GenesisHash = new Digest(gh),
                Receiver = new Address(to),
                CloseRemainderTo = new Address(close),
                Amount = amt,
                GenesisId = gen,
                Note = Convert.FromBase64String(note)
            };
            

            _scenarioContext["transaction"] = transaction;
        }

        [When(@"I sign the transaction with the private key")]
        public void WhenISignTheTransactionWithThePrivateKey()
        {
            Account account = new Account((string)_scenarioContext["mnemonic"]);
            var tx = (Transaction)_scenarioContext["transaction"];
            if (tx.Sender==null) tx.Sender = account.Address;
            var signed=tx.Sign(account);
            _scenarioContext["signedTransaction"]=signed;
            
            

        }

        [Then(@"the signed transaction should equal the golden ""([^""]*)""")]
        public void ThenTheSignedTransactionShouldEqualTheGolden(string p0)
        {
            var signed=_scenarioContext["signedTransaction"];
            var bytes = Algorand.Utils.Encoder.EncodeToMsgPackOrdered(signed);
            var b64 = Convert.ToBase64String(bytes);

         

            b64.Should().Be(p0);
        }

        [Then(@"the multisig address should equal the golden ""([^""]*)""")]
        public void ThenTheMultisigAddressShouldEqualTheGolden(string p0)
        {
            var addresses = (List<Address>)_scenarioContext["multisigs"];
            var multiSigAddress = new MultisigAddress(1,2, addresses.Select(a => a.Bytes).ToList());
            multiSigAddress.ToAddress().ToString().Should().Be(p0);
        }

        [Then(@"the multisig transaction should equal the golden ""([^""]*)""")]
        public void ThenTheMultisigTransactionShouldEqualTheGolden(string p0)
        {
            SignedTransaction tx = (SignedTransaction)_scenarioContext["signedTransaction"];
            var by=Algorand.Utils.Encoder.EncodeToMsgPackOrdered(tx);

            var should = Algorand.Utils.Encoder.DecodeFromMsgPack<SignedTransaction>(Convert.FromBase64String(p0));
            Convert.ToBase64String(by).Should().Be(p0);


        }

        [Given(@"encoded multisig transaction ""([^""]*)""")]
        public void GivenEncodedMultisigTransaction(string p0)
        {
            _scenarioContext["mtx"] = p0;
        }

        [When(@"I append a signature to the multisig transaction")]
        public void WhenIAppendASignatureToTheMultisigTransaction()
        {
            SignedTransaction tx = Algorand.Utils.Encoder.DecodeFromMsgPack<SignedTransaction>(Convert.FromBase64String((string)_scenarioContext["mtx"]));

            string mnemnonic = (string)_scenarioContext["mnemonic"];
            Account acct = new Account(mnemnonic);

            var newSigned = tx.Tx.Sign(acct);

            var msigToUse=tx.MSig.Subsigs.Where(k=> new Address(k.key.GetEncoded()).EncodeAsString()==acct.Address.EncodeAsString()).FirstOrDefault();

            msigToUse.sig = newSigned.Sig;


            _scenarioContext["signedTransaction"] = tx;  


        }

        [Given(@"encoded multisig transactions ""([^""]*)""")]
        public void GivenEncodedMultisigTransactions(string p0)
        {
            
            List<SignedTransaction> tx = p0.Split(' ').Select(s=> Algorand.Utils.Encoder.DecodeFromMsgPack<SignedTransaction>(Convert.FromBase64String(s))).ToList();

            _scenarioContext["msigtxns"] = tx;

        }

        [When(@"I merge the multisig transactions")]
        public void WhenIMergeTheMultisigTransactions()
        {
            List<SignedTransaction> tx = (List<SignedTransaction>)_scenarioContext["msigtxns"];

            var signed=SignedTransaction.MergeMultisigTransactions(tx.ToArray());

            _scenarioContext["signedTransaction"] = signed;
        }

        [When(@"I convert (.*) microalgos to algos and back")]
        public void WhenIConvertMicroalgosToAlgosAndBack(ulong p0)
        {
            var a=Utils.MicroalgosToAlgos(p0);
            var ma=Utils.AlgosToMicroalgos(a);
            _scenarioContext["ma"] = ma;
            ma.Should().Be(p0);

        }

        [Then(@"it should still be the same amount of microalgos (.*)")]
        public void ThenItShouldStillBeTheSameAmountOfMicroalgos(ulong p0)
        {
            ulong ma = (ulong)_scenarioContext["ma"];
            ma.Should().Be(p0);
        }
    }
}
#endif
