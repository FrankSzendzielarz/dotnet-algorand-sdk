using Algorand.Algod.Model;

using FluentAssertions;
using TechTalk.SpecFlow;

namespace algorand_tests.StepDefinitions.Unit
{
    [Binding]
    public class Helpers
    {
        ScenarioContext _scenarioContext;

        public Helpers(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a signing account with address ""([^""]*)"" and mnemonic ""([^""]*)""$")]
        public void GivenASigningAccountWithAddress(string address, string mnemonic)
        {
            Account acct = new Account(mnemonic);
            acct.Address.ToString().Should().Be(address);
            _scenarioContext["account"]=acct; 
            
        }

    }
}
