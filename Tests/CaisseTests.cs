using GOT;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CaisseTests
    {
        [Test]
        public void Quand_j_achete_0_livre_il_coute_0_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 0);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(0m));
        }

        [Test]
        public void Quand_j_achete_1_livre_il_coute_8_euros()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(8m));
        }

        [Test]
        public void Quand_j_achete_2_livres_differents_ils_coutent_1520_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(15.20m));
        }

        [Test]
        public void Quand_j_achete_2_livres_identiques_ils_coutent_16_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 2);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(16m));
        }

        [Test]
        public void Quand_j_achete_3_livres_differetns_ils_coutent_2160_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(21.60m));
        }

        [Test]
        public void Quand_j_achete_3_livres_dont_2_differetns_ils_coutent_2320_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 2);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(23.20m));
        }

        [Test]
        public void Quand_j_achete_4_livres_differetns_ils_coutent_2560_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);
            calculatrice.Acheter(4, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(25.60m));
        }

        [Test]
        public void Quand_j_achete_5_livres_differetns_ils_coutent_30_euro()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);
            calculatrice.Acheter(4, 1);
            calculatrice.Acheter(5, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(30m));
        }

        [Test]
        public void Test_Fonctionnel()
        {
            var calculatrice = new Caisse();
            calculatrice.Acheter(1, 2);
            calculatrice.Acheter(2, 2);
            calculatrice.Acheter(3, 2);
            calculatrice.Acheter(4, 1);
            calculatrice.Acheter(5, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(51.20m));
        }
    }
}