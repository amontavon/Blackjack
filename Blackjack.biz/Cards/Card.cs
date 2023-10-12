using static Blackjack.biz.Constants;

namespace Blackjack.biz.Cards
{
    public class Card
    {
        public CardValue Value { get; set; }
        public Suite CardSuite { get; set; }
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
                var displaySuite = GetDisplaySuite(this.CardSuite);
                return displayValue.ToString() + displaySuite.ToString();
            }
        }

        private char GetDisplaySuite(Suite s)
        {
            switch (s)
            {
                case Suite.Spade:
                    return 'S';
                case Suite.Club:
                    return 'C';
                case Suite.Diamond:
                    return 'D';
                case Suite.Heart:
                    return 'H';
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