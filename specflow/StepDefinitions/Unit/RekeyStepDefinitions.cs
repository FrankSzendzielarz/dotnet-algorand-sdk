#if TEST_DEBUG
namespace algorand_tests.StepDefinitions
{
    [Binding]
    public class RekeyStepDefinitions
    {
        ScenarioContext _scenarioContext;

        public RekeyStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [When(@"I add a rekeyTo field with address ""([^""]*)""")]
        public void WhenIAddARekeyToFieldWithAddress(string p0)
        {
            Transaction tx = (Transaction)_scenarioContext["transaction"] ;
            tx.RekeyTo = new Algorand.Address(p0);
        }

        [When(@"I set the from address to ""([^""]*)""")]
        public void WhenISetTheFromAddressTo(string p0)
        {
            Transaction tx = (Transaction)_scenarioContext["transaction"];
            tx.Sender = new Algorand.Address(p0);
        }
    }
}
#endif
