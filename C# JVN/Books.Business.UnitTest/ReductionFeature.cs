// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReductionFeature.cs" company="AXA France Service">
//   Copyright (c) AXA France Service. All rights reserved.
// </copyright>
//  <actor>s614599 (VANDENBUSSCHE Julien)</actor>
//  <created>24/06/2015 15:40</created>
//  <modified>25/06/2015 10:14</modified>
// <summary>
//   The reduction feature.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Books.Business.UnitTest
{
    using Books.Business.Models;
    using Books.Business.Services;

    using NUnit.Framework;

    using TechTalk.SpecFlow;

    /// <summary>
    /// The reduction feature.
    /// </summary>
    [Binding]
    public class ReductionFeature
    {
        /// <summary>
        /// The given j ai achete livres identiques.
        /// </summary>
        /// <param name="nbBook">
        /// The nb Book.
        /// </param>
        [Given(@"j'ai acheté (.*) livres identiques")]
        public void GivenJAiAcheteLivresIdentiques(int nbBook)
        {
            CartService cartService = new CartService();
            for (int bookIndex = 0; bookIndex < nbBook; bookIndex++)
            {
                cartService.AddBook(GameOfTrone.Tome1);
            }

            ScenarioContext.Current.Set(cartService);
        }

        /// <summary>
        /// The when je passe a la caisse.
        /// </summary>
        [When(@"je passe à la caisse")]
        public void WhenJePasseALaCaisse()
        {
            CartService cartService = ScenarioContext.Current.Get<CartService>();
            CartPrice cartPrice = cartService.GetTotal();
            ScenarioContext.Current.Set(cartPrice);
        }

        /// <summary>
        /// The then le cout total est de sans reduction.
        /// </summary>
        /// <param name="totalWithoutReduction">
        /// The total Without Reduction.
        /// </param>
        [Then(@"le cout total est de (.*) sans réduction")]
        public void ThenLeCoutTotalEstDeSansReduction(decimal totalWithoutReduction)
        {
            CartPrice cartPrice = ScenarioContext.Current.Get<CartPrice>();
            Assert.AreEqual(totalWithoutReduction, cartPrice.WithoutReduction);
        }

        /// <summary>
        /// The given j ai achete livres differentes.
        /// </summary>
        /// <param name="nbBook">
        /// The nb book.
        /// </param>
        [Given(@"j'ai acheté (.*) livres différentes")]
        public void GivenJAiAcheteLivresDifferentes(int nbBook)
        {
            CartService cartService = new CartService();
            for (int bookIndex = 0; bookIndex < nbBook; bookIndex++)
            {
                cartService.AddBook((GameOfTrone)bookIndex);
            }

            ScenarioContext.Current.Set(cartService);
        }

        /// <summary>
        /// The then le cout est de avec reduction.
        /// </summary>
        /// <param name="totalWithReduction">
        /// The total With Reduction.
        /// </param>
        [Then(@"le cout est de (.*) avec réduction")]
        public void ThenLeCoutEstDeAvecReduction(decimal totalWithReduction)
        {
            CartPrice cartPrice = ScenarioContext.Current.Get<CartPrice>();
            Assert.AreEqual(totalWithReduction, cartPrice.WithReduction);
        }

        /// <summary>
        /// The given un dernier livre identique.
        /// </summary>
        [Given(@"un dernier livre identique à l'un des trois")]
        public void GivenUnDernierLivreIdentique()
        {
            CartService cartService = ScenarioContext.Current.Get<CartService>();
            cartService.AddBook(cartService.Books[0].Tome);
        }

        /// <summary>
        /// The given j ai achete exemplaires du livre numero.
        /// </summary>
        /// <param name="nbBook">
        /// The nb Book.
        /// </param>
        /// <param name="tome">
        /// The tome.
        /// </param>
        [Given(@"j'ai acheté (.*) exemplaires du livre numéro (.*)")]
        public void GivenJAiAcheteExemplairesDuLivreNumero(int nbBook, int tome)
        {
            CartService cartService;
            if(!ScenarioContext.Current.ContainsKey(typeof(CartService).FullName))
            {
                ScenarioContext.Current.Set(new CartService());
            }

            cartService = ScenarioContext.Current.Get<CartService>();
            for (int bookIndex = 0; bookIndex < nbBook; bookIndex++)
            {
                cartService.AddBook((GameOfTrone)tome);
            }
        }
    }
}
