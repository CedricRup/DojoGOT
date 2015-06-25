using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GOT.Specs
{
    [Binding]
    public sealed class RulesSteps
    {
        [Given(@"j'ai acheté ""(.*)"" exemplaires du livre numéro ""(.*)""")]
        public void GivenJAiAcheteExemplairesDuLivreNumero(int nbCopy, int numBook)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"je passe à la caisse")]
        public void WhenJePasseALaCaisse()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"le cout total est de ""(.*)""")]
        public void ThenLeCoutTotalEstDe(string amount)
        {
            ScenarioContext.Current.Pending();
        }


    }
}
