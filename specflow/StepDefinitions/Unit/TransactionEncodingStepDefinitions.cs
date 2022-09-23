using Algorand;
using Algorand.Algod.Model.Transactions;
using Algorand.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Reflection;
using TechTalk.SpecFlow;

namespace algorand_tests.StepDefinitions
{
    [Binding]
    public class TransactionEncodingStepDefinitions
    {
        ScenarioContext _scenarioContext;

        public TransactionEncodingStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [Then(@"the base(.*) encoded signed transaction should equal ""([^""]*)""")]
        public void ThenTheBaseEncodedSignedTransactionShouldEqual(int p0, string p1)
        {
            var signedTransaction = _scenarioContext["signedTransaction"];
            var bytes = Algorand.Utils.Encoder.EncodeToMsgPackOrdered(signedTransaction);
            var b64 = Convert.ToBase64String(bytes);

         
            
            
            b64.ToLower().Should().Be(p1.ToLower());

        }

        [Then(@"the decoded transaction should equal the original")]
        public void ThenTheDecodedTransactionShouldEqualTheOriginal()
        {
            var signedTransaction = _scenarioContext["signedTransaction"];
            var bytes = Algorand.Utils.Encoder.EncodeToMsgPackOrdered(signedTransaction);
            var b64 = Convert.ToBase64String(bytes);

            var decodedSignedTransaction=Encoder.DecodeFromMsgPack<SignedTransaction>(bytes);

            string shouldBe = JsonConvert.SerializeObject(signedTransaction);
            string isActually = JsonConvert.SerializeObject(decodedSignedTransaction);

            isActually.Should().Be(shouldBe);



        }

        [When(@"I build a keyreg transaction with sender ""([^""]*)"", nonparticipation ""([^""]*)"", vote first (.*), vote last (.*), key dilution (.*), vote public key ""([^""]*)"", selection public key ""([^""]*)"", and state proof public key ""([^""]*)""")]
        public void WhenIBuildAKeyregTransactionWithSenderNonparticipationVoteFirstVoteLastKeyDilutionVotePublicKeySelectionPublicKeyAndStateProofPublicKey(
            string sender,
            bool nonpart, 
            ulong? votefirst, 
            ulong? votelast, 
            ulong? keydilution, 
            string votepk, 
            string selectpk, 
            string stateproofpk)
        {
            ulong fee = (ulong)_scenarioContext["fee"];
            bool flatFee = (bool)_scenarioContext["flatFee"];
            ulong firstValid = (ulong)_scenarioContext["firstValid"];
            ulong lastValid = (ulong)_scenarioContext["lastValid"];
            string genesisHash = (string)_scenarioContext["genesisHash"];
            string genesisId = (string)_scenarioContext["genesisId"];

            Address senderAddress = null;


            if (!String.IsNullOrWhiteSpace(sender)) senderAddress = new Algorand.Address(sender);

            KeyRegistrationTransaction keyreg = null;

            if ((votefirst??0) ==0)
            {

                keyreg = new KeyRegisterOfflineTransaction()
                {
                    Fee = fee,
                    FirstValid = firstValid,
                    LastValid = lastValid,
                    GenesisHash = new Digest(genesisHash),
                    GenesisID = genesisId,
                    Sender = senderAddress,
           

                };
            
            
            }
            else
            {

                keyreg = new KeyRegisterOnlineTransaction()
                {
                    Fee = fee,
                    FirstValid = firstValid,
                    LastValid = lastValid,
                    GenesisHash=new Digest(genesisHash),
                    GenesisID=genesisId,
                    Sender = senderAddress,
                    NonParticipation = nonpart,
                    VoteFirst = votefirst,
                    VoteLast = votelast,
                    VoteKeyDilution = keydilution,
                    VotePK = new Algorand.ParticipationPublicKey(Convert.FromBase64String(votepk)),
                    SelectionPK = new Algorand.VRFPublicKey(Convert.FromBase64String(selectpk))
                    

                };
            }

                   

            _scenarioContext["transaction"] = keyreg;

                
            

            


        }

        [Given(@"suggested transaction parameters fee (.*), flat-fee ""([^""]*)"", first-valid (.*), last-valid (.*), genesis-hash ""([^""]*)"", genesis-id ""([^""]*)""")]
        public void GivenSuggestedTransactionParametersFeeFlat_FeeFirst_ValidLast_ValidGenesis_HashGenesis_Id(ulong fee, bool flatFee, ulong firstValid, ulong lastValid, string genesisHash, string genesisId)
        {
            _scenarioContext["fee"] = fee;
            _scenarioContext["flatFee"] = flatFee;
            _scenarioContext["firstValid"] = firstValid;
            _scenarioContext["lastValid"] = lastValid;
            _scenarioContext["genesisHash"] = genesisHash;
            _scenarioContext["genesisId"] = genesisId;
        }

        [When(@"I build a payment transaction with sender ""([^""]*)"", receiver ""([^""]*)"", amount (.*), close remainder to ""([^""]*)""")]
        public void WhenIBuildAPaymentTransactionWithSenderReceiverAmountCloseRemainderTo(string sender, string receiver, ulong amount, string closeTo)
        {
            ulong fee=(ulong)_scenarioContext["fee"];
            bool flatFee=(bool)_scenarioContext["flatFee"];
            ulong firstValid= (ulong)_scenarioContext["firstValid"];
            ulong lastValid= (ulong)_scenarioContext["lastValid"];
            string genesisHash =(string)_scenarioContext["genesisHash"];
            string genesisId = (string)_scenarioContext["genesisId"];

            Address senderAddress = null;
            Address receiverAddress = null;
            Address closeRemainderToAddress = null;

            if (!String.IsNullOrWhiteSpace(sender)) senderAddress = new Algorand.Address(sender);
            if (!String.IsNullOrWhiteSpace(receiver)) receiverAddress = new Algorand.Address(receiver);
            if (!String.IsNullOrWhiteSpace(closeTo)) closeRemainderToAddress = new Algorand.Address(closeTo);

            var payment = new PaymentTransaction()
            {
                Amount = amount,
                Sender = senderAddress,
                Receiver = receiverAddress,
                CloseRemainderTo = closeRemainderToAddress,
                Fee = fee,
                FirstValid = firstValid,
                LastValid = lastValid,
                GenesisHash = new Digest(genesisHash),
                GenesisID = genesisId
            };

            _scenarioContext["transaction"] = payment;

        }
    }
}
