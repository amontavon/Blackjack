
using Blackjack.biz.Cards;

namespace Blackjack.biz.Players
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public int HandTotal { get; set; }
        public int HiddenHandTotal { get; set; }

        public string DisplayHand()
        {
            string handToDisplay = "";

            foreach(Card card in Hand)
            {
                handToDisplay += card.DisplayCard() + " ";
            }

            return handToDisplay;
        }
    }
}
