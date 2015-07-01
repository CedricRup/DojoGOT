using System;
using System.Collections.Generic;
using System.Linq;

namespace GOT
{
    public class Caisse
    {
        private readonly Reduction[] _reductions;

        public Caisse()
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
    }
}