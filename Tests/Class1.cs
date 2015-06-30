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
        public List<Item> Panier { get; set; }

        public Calculatrice()
        {
            Panier = new List<Item>();
        }

        public void Acheter(int id, int quantite)
        {
            Panier.Add(new Item(id, quantite));
        }

        public decimal CalculerTotal()
        {
            var reductions = new[]
            {
                 new {Nombre = 0, Taux = 0m},
                new {Nombre = 1, Taux = 1m},
                new {Nombre = 2, Taux = 0.95m},
                new {Nombre = 3, Taux = 0.90m},

            };

            var total = 0m;
            while (Panier.Any())
            {
                var valeurs = Panier.Select(item => item.Quantite).ToList();
                var quantite = valeurs.Count(v => v >= 1);

                var reduc = reductions.First(r => r.Nombre == quantite).Taux;

                total = total + reduc*quantite*8;

                Panier =
                    Panier.Select(item => new Item(item.Id, item.Quantite - 1))
                        .Where(item => item.Quantite > 0).ToList();
            }
            return total;
        }

    }

    public class Item {
        public int Id { get; set; }
        public int Quantite { get; set; }

        public Item(int id, int quantite)
        {
            Id = id;
            Quantite = quantite;
        }
    }
}
