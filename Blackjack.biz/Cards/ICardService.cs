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
    }
}
