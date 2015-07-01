using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GOT.Specs
{
    [Binding]
    public sealed class RulesSteps
    {
        private readonly Caisse _caisse = new Caisse();
        private decimal _total;

        [Given(@"j'ai acheté ""(.*)"" exemplaires du livre numéro ""(.*)""")]
        public void GivenJAiAcheteExemplairesDuLivreNumero(int nbCopy, int numBook)
        {
            _caisse.Acheter(numBook,nbCopy);
        }

        [When(@"je passe à la caisse")]
        public void WhenJePasseALaCaisse()
        {
            _total = _caisse.OptimiserTotal();
        }

        [Then(@"le cout total est de (.*)€")]
        public void ThenLeCoutTotalEstDe(decimal amount)
        {
            Assert.That(_total,Is.EqualTo(amount));
        }


    }
}
