using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class Card
    {
        public CardValue Value { get; set; }
        public Suit CardSuit { get; set; }
        public bool IsHidden { get; set; }

        public string DisplayCard()
        {
            if (IsHidden)
            {
                return ("*");
            }
            else
            {
                var displayValue = GetDisplayValue(this.Value);
                var displaySuit = GetDisplaySuit(this.CardSuit);
                return displayValue.ToString() + displaySuit.ToString();
            }
        }

        private char GetDisplaySuit(Suit s)
        {
            switch (s)
            {
                case Suit.Spade:
                    return '♠';
                case Suit.Club:
                    return '♣';
                case Suit.Diamond:
                    return '♦';
                case Suit.Heart:
                    return '♥';
                default:
                    return '*';
            }
        }

        private string GetDisplayValue(CardValue v)
        {
            switch (v)
            {
                case CardValue.Ace:
                    return "A";
                case CardValue.Jack:
                    return "J";
                case CardValue.Queen:
                    return "Q";
                case CardValue.King:
                    return "K";
                default:
                    var value = (int)v;
                    return value.ToString();

            }
        }

        public int GetCardValue()
        {
            switch(this.Value)
            {
                case CardValue.Ace:
                    return 1;
                case CardValue.Jack:
                    return 10;
                case CardValue.Queen:
                    return 10;
                case CardValue.King:
                    return 10;
                default:
                    return (int)this.Value;
            }
        }
    }
}