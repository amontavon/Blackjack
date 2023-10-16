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
                var displayValue = GetCardDisplayValue(this.Value);
                var displaySuit = GetSuitDisplayValue(this.CardSuit);
                return displayValue.ToString() + displaySuit.ToString();
            }
        }

        public int GetCardPointValue()
        {
            switch (this.Value)
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

        private string GetSuitDisplayValue(Suit s)
        {
            switch (s)
            {
                case Suit.Spade:
                    return "♠";
                case Suit.Club:
                    return "♣";
                case Suit.Diamond:
                    return "♦";
                case Suit.Heart:
                    return "♥";
                default:
                    return "*";
            }
        }

        private string GetCardDisplayValue(CardValue v)
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
    }
}