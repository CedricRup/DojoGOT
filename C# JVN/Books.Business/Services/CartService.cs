// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartService.cs" company="AXA France Service">
//   Copyright (c) AXA France Service. All rights reserved.
// </copyright>
//  <actor>s614599 (VANDENBUSSCHE Julien)</actor>
//  <created>25/06/2015 09:20</created>
//  <modified>25/06/2015 09:25</modified>
// <summary>
//   The cart.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Books.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Books.Business.Models;

    /// <summary>
    /// The cart.
    /// </summary>
    public class CartService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class.
        /// </summary>
        public CartService()
        {
            this.Books = new List<Book>();
        }

        /// <summary>
        /// Gets the books.
        /// </summary>
        public List<Book> Books { get; private set; }

        /// <summary>
        /// The add book.
        /// </summary>
        /// <param name="gameOfTrone">
        /// The game of trone.
        /// </param>
        public void AddBook(GameOfTrone gameOfTrone)
        {
            this.Books.Add(new Book
                               {
                                   Tome = gameOfTrone
                               });
        }

        /// <summary>
        /// The get total.
        /// </summary>
        /// <returns>
        /// The <see cref="CartPrice"/>.
        /// </returns>
        public CartPrice GetTotal()
        {
            BookPromotionService bookPromotionService = new BookPromotionService();

            // Permet de casser la reference avec la liste originelle.
            List<Book> books = this.Books.ToList();
            CartPrice cartPrice = new CartPrice
                                      {
                                          WithoutReduction = this.Books.Sum(book => book.Price), 
                                          WithReduction = bookPromotionService.Compute(books)
                                      };
            return cartPrice;
        }
    }
}