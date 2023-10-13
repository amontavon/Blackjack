using Blackjack.biz.Players;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class CardService : ICardService
    {
        public List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();

            foreach (int Suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (int value in Enum.GetValues(typeof(CardValue)))
                {
                    deck.Add(new Card { Value = (CardValue)value, CardSuit = (Suit)Suit });
                }
            }

            //foreach (var c in deck)
            //{
            //    Console.WriteLine(c.DisplayCard());
            //}

            return deck;
        }

        

        public List<Card> DiscardCards(List<Player> players) { 
            List<Card> discard = new List<Card>();

            foreach(var player in players)
            {
                foreach(var card in player.Hand)
                {
                    if (card.IsHidden)
                    {
                        card.IsHidden = false;
                    }

                    discard.Add(card);
                    player.Hand.Remove(card); 
                }
            }

            return discard;
        }
    }
}
