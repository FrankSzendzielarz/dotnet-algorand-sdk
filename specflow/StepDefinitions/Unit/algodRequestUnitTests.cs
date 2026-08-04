#if TEST_DEBUG
namespace specflow.StepDefinitions
{
    [Binding]
    public sealed class v2AlgodBindings
    {
      
        [When(@"we make a Pending Transaction Information against txid ""([^""]*)"" with format ""([^""]*)""$")]
        public void WeMakeAPendingTransactionInformationAgainstTxIdWithFormat(string txId, string format)
        {
            Format fmt = (Format)Enum.Parse(typeof(Format), format.FirstCharToUpper());

            httpUtilities.algodDefaultApiInstance.PendingTransactionInformationAsync(txId, fmt);
        }
        
        [When(@"we make a Pending Transaction Information with max (\d+) and format ""([^""]*)""$")]
        public void WeMakeAPendingTransactionInformationAgainstTxIdWithMaxAndFormat(ulong max, string format)
        {
            Format fmt = (Format)Enum.Parse(typeof(Format), format.FirstCharToUpper());

            httpUtilities.algodDefaultApiInstance.GetPendingTransactionsAsync(fmt,max);
        }


        [When(@"we make a Pending Transactions By Address call against account ""([^""]*)"" and max (\d+) and format ""([^""]*)""$")]
        public void WeMakeAPendingTransactionsByAddressCallAgainstAccountAndMaxAndFormat(string address, ulong max, string format)
        {
            Format fmt = (Format)Enum.Parse(typeof(Format), format.FirstCharToUpper());

            httpUtilities.algodDefaultApiInstance.GetPendingTransactionsByAddressAsync(address, fmt, max);
        }

        [When(@"we make a Status after Block call with round (\d+)")]
        public void WeMakeAStatusAfterBlockCallWithRound(ulong round)
        {
            httpUtilities.algodDefaultApiInstance.WaitForBlockAsync(round);
        }

        [When(@"we make an Account Information call against account ""([^""]*)""$")]
        public void WeMakeAnAccountInformationCallAgainstAccount(string address)
        {

            httpUtilities.algodDefaultApiInstance.AccountInformationAsync(address,null,null);
        }

        [When(@"we make a Get Block call against block number (\d+) with format ""([^""]*)""$")]
        public void WeGetABlockCallAgainstBlockNumberWithFormat(ulong block, string format)
        {
            Format fmt = (Format)Enum.Parse(typeof(Format), format.FirstCharToUpper());

            httpUtilities.algodDefaultApiInstance.GetBlockAsync(block, fmt);
        }

        [When(@"we make a GetAssetByID call for assetID (\d+)")]
        public void WeMakeAGetAssetByIDCalForAssetID(ulong assetId)
        {
            httpUtilities.algodDefaultApiInstance.GetAssetByIDAsync(assetId);
        }

        [When(@"we make a GetApplicationByID call for applicationID (\d+)")]
        public void WeMakeACallGetApplicationByIDCallForApplicationID(ulong appId)
        {
            httpUtilities.algodDefaultApiInstance.GetApplicationByIDAsync(appId);
        }

        [When(@"we make an Account Information call against account ""([^""]*)"" with exclude ""([^""]*)""$")]
        public void WeMakeAnAccountInformationCallAgainstAccountWithExclude(string address, string exclude)
        {
            httpUtilities.algodDefaultApiInstance.AccountInformationAsync(address, exclude,null);
        }

        [When(@"we make an Account Asset Information call against account ""([^""]*)"" assetID (\d+)")]
        public void WeMakeAnAccountAssetInformationCallAgainstAccountAndAssetID(string account, ulong assetId)
        {
            httpUtilities.algodDefaultApiInstance.AccountAssetInformationAsync(account, assetId, null);
        }

        [When(@"we make an Account Application Information call against account ""([^""]*)"" applicationID (\d+)")]
        public void WeMakeAnAccountAssetInformationCallAgainstAccountAndApplicationId(string account, ulong appId)
        {
            httpUtilities.algodDefaultApiInstance.AccountApplicationInformationAsync(account, appId, null);
        }


      


        [Then(@"expect the path used to be ""([^""]*)""$")]
        public void ExpectThePathUsedToBe(string expectedPath)
        {
            expectedPath.Should().Be(TestHttpMessageHandler.LastRequest.RequestUri?.PathAndQuery);

        }



    }

}
#endif