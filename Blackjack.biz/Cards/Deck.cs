using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class Deck
    {
        public Deck() {
            Cards = new List<Card>();
            DeckSize = 52;
        }
        public List<Card> Cards { get; set; }

        public int DeckSize { get; set; }

        public void Initalize()
        {
            foreach (int Suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (int value in Enum.GetValues(typeof(CardValue)))
                {
                    Cards.Add(new Card { Value = (CardValue)value, CardSuit = (Suit)Suit });
                }
            }

            //foreach (var c in deck)
            //{
            //    Console.WriteLine(c.DisplayCard());
            //}
        }

        public void Shuffle()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var ShuffleDeck = new List<Card>();

            for (int i = 0; i < DeckSize; i++)
            {
                var card = Cards[rnd.Next(Cards.Count)];
                Cards.Remove(card);

                ShuffleDeck.Add(card);
            }

            Cards = ShuffleDeck;
        }

        public Card Draw()
        {
            var card = Cards.First();
            Cards.Remove(card);
            return card;
        }
    }
}
