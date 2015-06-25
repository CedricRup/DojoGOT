// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CartPrice.cs" company="AXA France Service">
//   Copyright (c) AXA France Service. All rights reserved.
// </copyright>
//  <actor>s614599 (VANDENBUSSCHE Julien)</actor>
//  <created>25/06/2015 09:19</created>
//  <modified>25/06/2015 09:20</modified>
// <summary>
//   The cart price.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Books.Business.Models
{
    /// <summary>
    /// The cart price.
    /// </summary>
    public class CartPrice
    {
        /// <summary>
        /// Gets or sets the with reduction.
        /// </summary>
        public decimal WithReduction { get; set; }

        /// <summary>
        /// Gets or sets the without reduction.
        /// </summary>
        public decimal WithoutReduction { get; set; }
    }
}