
using Blackjack.biz.Cards;

namespace Blackjack.biz.Players
{
    public class Player
    {
        public Player()
        {
            Name = "Player";
            Hand = new List<Card>();
            HandTotal = 0;
            HiddenHandTotal = 0;
        }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            HandTotal = 0;
            HiddenHandTotal = 0;
        }

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

        public void ResetPlayer()
        {
            Hand.Clear();
            HandTotal = 0;
            HiddenHandTotal = 0;
        }
    }
}
