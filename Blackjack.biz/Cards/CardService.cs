using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class CardService : ICardService
    {
        public List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (int suite in Enum.GetValues(typeof(Suite)))
            {
                foreach (int value in Enum.GetValues(typeof(CardValue)))
                {
                    deck.Add(new Card { Value = (CardValue)value, CardSuite = (Suite)suite });
                }
            }

            //foreach (var c in deck)
            //{
            //    Console.WriteLine(c.DisplayCard());
            //}

            return deck;
        }

        public List<Card> ShuffleDeck(List<Card> deck)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var ShuffleDeck = new List<Card>();
            var card = new Card();

            for(int i = 0; i < Constants.DECK_SIZE; i++)
            {
                card = deck[rnd.Next(deck.Count)];
                deck.Remove(card);

                ShuffleDeck.Add(card);
            }

            return ShuffleDeck;
        }

        public void DiscardCards() { } //method called at the end of the game, takes cards from all players hands and creates a discarded deck. when the deck runs out, discard = deck, then deck is reshuffled
    }
}
