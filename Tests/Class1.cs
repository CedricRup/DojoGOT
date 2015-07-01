using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Quand_j_achete_0_livre_il_coute_0_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 0);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(0m));
        }

        [Test]
        public void Quand_j_achete_1_livre_il_coute_8_euros()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(8m));
        }

        [Test]
        public void Quand_j_achete_2_livres_differents_ils_coutent_1520_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(15.20m));
        }

        [Test]
        public void Quand_j_achete_2_livres_identiques_ils_coutent_16_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 2);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(16m));
        }

        [Test]
        public void Quand_j_achete_3_livres_differetns_ils_coutent_2160_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(21.60m));
        }

        [Test]
        public void Quand_j_achete_3_livres_dont_2_differetns_ils_coutent_2320_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 2);
            calculatrice.Acheter(2, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(23.20m));
        }

        [Test]
        public void Quand_j_achete_4_livres_differetns_ils_coutent_2560_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);
            calculatrice.Acheter(4, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(25.60m));
        }

        [Test]
        public void Quand_j_achete_5_livres_differetns_ils_coutent_30_euro()
        {
            var calculatrice = new Calculatrice();
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
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 2);
            calculatrice.Acheter(2, 2);
            calculatrice.Acheter(3, 2);
            calculatrice.Acheter(4, 1);
            calculatrice.Acheter(5, 1);

            Assert.That(calculatrice.OptimiserTotal(), Is.EqualTo(51.20m));
        }
    }

    public class Calculatrice
    {
        private readonly Reduction[] _reductions;

        public Calculatrice()
        {
            Panier = new List<Item>();
            _reductions = new[]
            {
                new Reduction(2, 0.95m),
                new Reduction(3, 0.90m),
                new Reduction(4, 0.80m),
                new Reduction(5, 0.75m)
            };
        }

        public List<Item> Panier { get; set; }

        public void Acheter(int id, int quantite)
        {
            Panier.Add(new Item(id, quantite));
        }

        public decimal OptimiserTotal()
        {
            return OptimiserTotal(Panier, _reductions.ToList());
        }

        private static decimal OptimiserTotal(IEnumerable<Item> livres, List<Reduction> reductions)
        {
            var livresNonVide = EliminerLesQuantitesVides(livres);

            if (!livresNonVide.Any()) return 0m;

            var totalSansLaProchaineReduction = CalculerTotalSansLaProchaineReduction(livresNonVide, reductions);
            var totalAvecLaProchaineReduction = TotalAvecLaProchaineReduction(livresNonVide, reductions);

            return Math.Min(totalSansLaProchaineReduction, totalAvecLaProchaineReduction);
        }

        private static List<Item> EliminerLesQuantitesVides(IEnumerable<Item> livres)
        {
            return livres.Where(i => i.Quantite > 0).ToList();
        }

        private static decimal TotalAvecLaProchaineReduction(List<Item> livres, List<Reduction> reductions)
        {
            var reduction = TrouverReduction(livres, reductions);
            var livresRestants = EliminerLivresUtilises(livres, reduction);
            return reduction.Taux * reduction.Nombre * 8 + OptimiserTotal(livresRestants, reductions);
        }

        private static decimal CalculerTotalSansLaProchaineReduction(IEnumerable<Item> livres, List<Reduction> reductions)
        {
            return reductions.Any() ? OptimiserTotal(livres, reductions.Skip(1).ToList()) : Decimal.MaxValue;
        }

        private static IEnumerable<Item> EliminerLivresUtilises(IEnumerable<Item> livres, Reduction reduction)
        {
            var livresRestants = new List<Item>();
            var quantiteTotaleARetrancher = reduction.Nombre;

            foreach (var item in livres)
            {
                var aSoustraire = quantiteTotaleARetrancher > 0 ? 1 : 0;
                livresRestants.Add(new Item(item.Id,  item.Quantite - aSoustraire));
                quantiteTotaleARetrancher--;
            }
            return livresRestants;
        }

        private static Reduction TrouverReduction(IEnumerable<Item> items, IEnumerable<Reduction> reductions)
        {
            var nombreDeLivresDifferents = items.Count();
            var reduction = reductions.FirstOrDefault(r => nombreDeLivresDifferents >= r.Nombre) ?? new Reduction(1, 1);
            return reduction;
        }

        public class Reduction
        {
            private readonly int _nombre;
            private readonly decimal _taux;

            public Reduction(int nombre, decimal taux)
            {
                _nombre = nombre;
                _taux = taux;
            }

            public int Nombre
            {
                get { return _nombre; }
            }

            public decimal Taux
            {
                get { return _taux; }
            }
        }
    }

    public class Item
    {
        public Item(int id, int quantite)
        {
            Id = id;
            Quantite = quantite;
        }

        public int Id { get; set; }
        public int Quantite { get; set; }
    }
}