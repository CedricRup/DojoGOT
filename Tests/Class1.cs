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

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(0m));
        }

        [Test]
        public void Quand_j_achete_1_livre_il_coute_8_euros()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(8m));
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

        [Test]
        public void Quand_j_achete_4_livres_differetns_ils_coutent_2560_euro()
        {
            var calculatrice = new Calculatrice();
            calculatrice.Acheter(1, 1);
            calculatrice.Acheter(2, 1);
            calculatrice.Acheter(3, 1);
            calculatrice.Acheter(4, 1);

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(25.60m));
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

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(30m));
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

            Assert.That(calculatrice.CalculerTotal(), Is.EqualTo(51.20m));
        }
    }

    public class Calculatrice
    {
        public Calculatrice()
        {
            Panier = new List<Item>();
        }

        public List<Item> Panier { get; set; }

        public void Acheter(int id, int quantite)
        {
            Panier.Add(new Item(id, quantite));
        }

        public decimal CalculerTotal()
        {
            List<Item> items = Panier;
            var reductions = new[]
            {
                new Reduction(2, 0.95m),
                new Reduction(3, 0.90m),
                new Reduction(4, 0.80m),
                new Reduction(5, 0.75m)
            };

            return CalculerTotal(items, reductions);
        }

        private static decimal CalculerTotal(List<Item> items, Reduction[] reductions)
        {
            items = items.Where(i => i.Quantite > 0).ToList();

            if (!items.Any()) return 0m;

            List<int> valeurs = items.Select(item => item.Quantite).ToList();
            int quantite = valeurs.Count(v => v >= 1);

            decimal alt = Decimal.MaxValue;

            if (reductions.Any())
            {
                alt = CalculerTotal(items, reductions.Skip(1).ToArray());
            }

            var reduction = reductions.FirstOrDefault(r => quantite >= r.Nombre) ?? new Reduction(1, 1);
            decimal reduc = reduction.Taux;

            var quantiteARetrancher = reduction.Nombre;

            List<Item> newitems = new List<Item>();
            foreach (var item in items)
            {
                newitems.Add(new Item(item.Id, quantiteARetrancher > 0 ? item.Quantite - 1 : item.Quantite ));
                quantiteARetrancher--;

            }
            
            return Math.Min(alt, reduc*reduction.Nombre*8 + CalculerTotal(newitems, reductions));
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