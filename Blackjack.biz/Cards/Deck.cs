using Blackjack.biz.Game;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class Deck
    {
        public Deck() {
            DrawPile = new List<Card>();
            CardsInPlay = new List<Card>();
            Discard = new List<Card>();
            DeckSize = 52;
        }
        public List<Card> DrawPile { get; set; } //Deck of cards face down available to go into play

        public List<Card> CardsInPlay { get; set; } //List of cards currently in play
        public List<Card> Discard { get; set; } //Discarded cards

        public int DeckSize { get; set; }

        public void Initalize()
        {
            foreach (int Suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (int value in Enum.GetValues(typeof(CardValue)))
                {
                    DrawPile.Add(new Card { Value = (CardValue)value, CardSuit = (Suit)Suit });
                }
            }
        }

        public List<Card> Shuffle(List<Card> deck)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var ShuffleDeck = new List<Card>();

            for (int i = 0; i < DeckSize; i++)
            {
                var card = deck[rnd.Next(deck.Count)];
                deck.Remove(card);

                ShuffleDeck.Add(card);
            }
;
            return ShuffleDeck;
        }

        public Card DrawCard()
        {
            if(DrawPile.Count == 0) //if there are no cards left, reshuffle the discard pile
            {
                ResetDeck();
            }

            var card = DrawPile.First();
            DrawPile.Remove(card);
            return card;
        }

        private void ResetDeck()
        {
            DrawPile = Shuffle(Discard);
            Discard.Clear();
        }
    }
}
