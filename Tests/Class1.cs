using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Quand_j_achete_1_livre_il_coute_8_euros()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(8m));
        }

        [Test]
        public void Quand_j_achete_0_livre_il_coute_0_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 0);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(0m));            
        }

        [Test]
        public void Quand_j_achete_2_livres_differents_ils_coutent_1520_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(15.20m));
        }

        [Test]
        public void Quand_j_achete_2_livres_identiques_ils_coutent_16_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 2);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(16m));
        }

        [Test]
        public void Quand_j_achete_3_livres_differetns_ils_coutent_2160_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(21.60m));
        }

        [Test]
        public void Quand_j_achete_3_livres_dont_2_differetns_ils_coutent_2320_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 2);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(23.20m));
        }


    }

    public class Calculatrice
    {
        public Dictionary<int,int> Quantites { get; set; }

        public Calculatrice()
        {
            Quantites = new Dictionary<int, int>();
        }

        public void Acheter(int id, int quantite)
        {
            Quantites[id] = quantite;
        }

        public decimal CalculerTotal()
        {
            var valeurs = Quantites.Select(kv => kv.Value).ToList();

            var reductionnables = valeurs.Count(v => v >= 1);

            var total = 0m;
            var taux = 1m;
            switch (reductionnables)
            {
                case 1:
                    total += taux * 8;
                    break;
                case 2:
                    taux = 0.95m;
                    total += 2*taux*8;
                    break;
                case 3:
                    taux = 0.90m;
                    total += 3*taux*8;
                    break;
            }

            return total;
        }
    }
}
