using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz.Cards
{
    public interface ICardService
    {
        /// <summary>
        /// Creates a deck of 52 cards.
        /// </summary>
        /// <returns>List of cards (the deck)</returns>
        public List<Card> CreateDeck();

        /// <summary>
        /// Shuffles the cards in the deck
        /// </summary>
        /// <param name="deck"></param>
        /// <returns>The shuffled deck</returns>
        public List<Card> ShuffleDeck(List<Card> deck);
    }
}
