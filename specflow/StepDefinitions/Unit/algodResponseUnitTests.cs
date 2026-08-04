#if TEST_DEBUG
namespace specflow.StepDefinitions
{
    [Binding]
    public sealed class algodResponseUnitTests
    {
        public string error;
        
        [Given(@"mock http responses in ""([^""]*)"" loaded from ""([^""]*)""$")]
        public void GivenMockHttpResponsesInJsonFilesLoadedFromDirectory(string jsonfile, string directory)
        {
            byte[] mockResponseBytes;

            HttpResponseMessage resp;

            if (jsonfile.EndsWith("base64"))
            {
                mockResponseBytes = System.Convert.FromBase64String( File.ReadAllText(Path.Combine( "Features","Unit",directory,jsonfile)) )   ;

                var t = MessagePack.MessagePackSerializer.ConvertToJson(mockResponseBytes);
                

                resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content =  new ByteArrayContent(mockResponseBytes)
                };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/msgpack");

            }
            else
            {
                mockResponseBytes = File.ReadAllBytes(Path.Combine("Features", "Unit",directory,jsonfile));
                resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(mockResponseBytes)
                };
                resp.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            
            
            TestHttpMessageHandler.NextResponse = resp;
        }

        Algorand.Algod.Model.PendingTransactions? pendingTransactionInfo;
        [When(@"we make any Pending Transaction Information call")]
        public async Task WeMakeAnyPendingTransactionInformationCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                pendingTransactionInfo = await httpUtilities.algodDefaultApiInstance.GetPendingTransactionsByAddressAsync("", null) as PendingTransactions ;
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Pending Transaction Information response should have sender ""([^""]*)""")]
        public void ExpectPendingTransactionInformationResponseSenderToBe(string sender)
        {
            pendingTransactionInfo?.TopTransactions.Select(t=>t.Tx.Sender.Should().Be(sender));
        }

        PendingTransactions? pendingTransactions;
        [When(@"we make any Pending Transactions Information call")]
        public async Task WeMakeAnyPendingTransactionsInformationCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                pendingTransactions = await httpUtilities.algodDefaultApiInstance.GetPendingTransactionsAsync(null, null);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }

        }

        [Then(@"the parsed Pending Transactions Information response should contain an array of len (\d+) and element number (\d+) should have sender ""([^""]*)""")]
        public void ExpectArrayOfLenAndElementNumberAndSender(int len, int element, string sender)
        {
            pendingTransactions.TopTransactions.Count.Should().Be(len);
            pendingTransactions.TopTransactions.ToList()[element].Tx.Sender.ToString().Should().Be(sender);

        }

        PostTransactionsResponse? postTransactionsResponse;
        [When(@"we make any Send Raw Transaction call")]
        public async Task WeMakeAnySendRawTransactionCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                postTransactionsResponse = await httpUtilities.algodDefaultApiInstance.TransactionsAsync(new List<SignedTransaction>());
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Send Raw Transaction response should have txid ""([^""]*)""")]
        public void ExpectTransactionIdToBe(string txid)
        {
            postTransactionsResponse?.Txid.Should().Be(txid);
        }

        PendingTransactions? pendingTransactionsByAddress;
        [When(@"we make any Pending Transactions By Address call")]
        public async Task WeMakeAnyPendingTransactionsByAddressCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                pendingTransactionsByAddress = await httpUtilities.algodDefaultApiInstance.GetPendingTransactionsByAddressAsync("test", null, null);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Pending Transactions By Address response should contain an array of len (\d+) and element number (\d+) should have sender ""([^""]*)""")]
        public void ExpectPendingTransactionsByAddressArrayOfLenAndElementNumberAndSender(int len, int element, string sender)
        {
            pendingTransactionsByAddress.TopTransactions.Count.Should().Be(len);
            pendingTransactionsByAddress.TopTransactions.ToList()[element].Tx.Sender.ToString().Should().Be(sender);

        }

        NodeStatusResponse? status;
        [When(@"we make any Node Status call")]
        public async Task WeMakeAnyNodeStatusCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                status = await httpUtilities.algodDefaultApiInstance.GetStatusAsync();
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Node Status response should have a last round of (\d+)")]
        public void ExpectNodeStatusResponseRoundNum(ulong round)
        {
            status?.LastRound.Should().Be(round);

        }


        SupplyResponse? supply;
        [When(@"we make any Ledger Supply call")]
        public async Task WeMakeAnyLedgerSupplyCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                supply = await httpUtilities.algodDefaultApiInstance.GetSupplyAsync();
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Ledger Supply response should have totalMoney (\d+) onlineMoney (\d+) on round (\d+)")]
        public void ExpectNodeStatusResponseRoundNum(ulong total, ulong online, ulong round)
        {
            supply?.TotalMoney.Should().Be(total);
            supply?.OnlineMoney.Should().Be(online);
            supply?.OnlineMoney.Should().Be(online);


        }

        NodeStatusResponse? statusAfterBlock;
        [When(@"we make any Status After Block call")]
        public async Task WeMakeAnyStatusAfterBlockCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                statusAfterBlock = await httpUtilities.algodDefaultApiInstance.WaitForBlockAsync(0);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Status After Block response should have a last round of (\d+)")]
        public void ExpectParsedStatusAfterBlockLastRound(ulong total)
        {
            statusAfterBlock?.LastRound.Should().Be(total);
        }

        Account? accountInformation;
        [When(@"we make any Account Information call")]
        public async Task WeMakeAnyAccountInformationCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                accountInformation = await httpUtilities.algodDefaultApiInstance.AccountInformationAsync("test", null, null);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Account Information response should have address ""([^""]*)""")]
        public void ExpectParsedAccountInfoToHaveAddress(string addr)
        {
            accountInformation?.Address.ToString().Should().Be(addr);
        }

        CertifiedBlock? block;
        [When(@"we make any Get Block call")]
        public async Task WeMakeAnyGetBlockCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                block = await httpUtilities.algodDefaultApiInstance.GetBlockAsync(0, null);
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Get Block response should have rewards pool ""([^""]*)""")]
        public void ExpectParsedGetBlockToHaveRewardPool(string pool)
        {
            block?.Block.RewardsPool.Should().Be(new Address(Convert.FromBase64String(pool)));
        }

        TransactionParametersResponse? transactionParameters;
        [When(@"we make any Suggested Transaction Parameters call")]
        public async Task WeMakeAnySuggestedTransactionParametersCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                transactionParameters = await httpUtilities.algodDefaultApiInstance.TransactionParamsAsync();
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Suggested Transaction Parameters response should have first round valid of (\d+)")]
        public void ExpectParsedSuggestedTransactionParametersResponseShouldHaveFirstRoundValidOf(ulong round)
        {
            transactionParameters?.LastRound.Should().Be(round);
        }

        DryrunResponse? dryrunResponse;
        [When(@"we make any Dryrun call")]
        public async Task WeMakeAnyDryrunCall()
        {
            httpUtilities.setUp();
            try
            {
                error = "";
                dryrunResponse = await httpUtilities.algodDefaultApiInstance.TealDryrunAsync(new DryrunRequest());
            }
            catch (ApiException<ErrorResponse> ex)
            {
                error = ex.Result.Message;
            }
        }

        [Then(@"the parsed Dryrun Response should have global delta ""([^""]*)"" with (\d+)")]
        public void ExpectParsedDryrunResponseShouldHaveGlobalDelta(string key, ulong creator)
        {
            dryrunResponse?.Txns
                .SelectMany(t => t.GlobalDelta)
                .Where(kv => kv.Key == key)
                .Select(kv => kv.Value)
                .FirstOrDefault()
                ?.Action
                .Should()
                .Be(creator);
        }

        [Then(@"expect error string to contain ""([^""]*)""$")]
        public void ExpectErrorStringToBe(string err)
        {
            error.Trim().Should().Be(err.Trim());
        }

      

    }
}
#endif