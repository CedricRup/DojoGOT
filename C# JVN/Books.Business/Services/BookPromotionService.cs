// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookPromotionService.cs" company="AXA France Service">
//   Copyright (c) AXA France Service. All rights reserved.
// </copyright>
//  <actor>s614599 (VANDENBUSSCHE Julien)</actor>
//  <created>25/06/2015 09:20</created>
//  <modified>25/06/2015 10:08</modified>
// <summary>
//   The book promotion.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Books.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Books.Business.Models;

    /// <summary>
    /// The book promotion.
    /// </summary>
    public class BookPromotionService
    {
        /// <summary>
        /// The reduction.
        /// </summary>
        private readonly decimal[] reductions = { 1m, 0.95m, 0.9m, 0.80m, 0.75m };

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="books">
        /// The books.
        /// </param>
        /// <param name="price">
        /// The price.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public decimal Compute(List<Book> books, decimal price = 0)
        {
            if (!books.Any())
            {
                return price;
            }

            var groupBook = books.GroupBy(b => b.Tome).ToList();
            var nbGroup = groupBook.Count();
            if (nbGroup == 5 && groupBook.Count(g => g.Count() > 1) == 3)
            {
                nbGroup = 4;
                groupBook = groupBook.Take(nbGroup).ToList();
            }

            decimal reduction = this.reductions[nbGroup - 1];
            decimal totalPack = 0;
            groupBook.ForEach(
                group =>
                    {
                        var book = group.First();
                        books.Remove(book);
                        totalPack += book.Price;
                    });

            price += totalPack * reduction + this.Compute(books, price);
            return price;
        }
    }
}