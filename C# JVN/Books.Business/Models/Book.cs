// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Book.cs" company="AXA France Service">
//   Copyright (c) AXA France Service. All rights reserved.
// </copyright>
//  <actor>s614599 (VANDENBUSSCHE Julien)</actor>
//  <created>25/06/2015 09:19</created>
//  <modified>25/06/2015 09:20</modified>
// <summary>
//   The book.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Books.Business.Models
{
    /// <summary>
    /// The book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book()
        {
            this.Price = 8;
        }

        /// <summary>
        /// Gets or sets the tome.
        /// </summary>
        public GameOfTrone Tome { get; set; }

        /// <summary>
        /// Gets the price.
        /// </summary>
        public decimal Price { get; private set; }
    }
}