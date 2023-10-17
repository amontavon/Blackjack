using Blackjack.biz.Bets;
using Blackjack.biz.Cards;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Players
{
    public class Player
    {
        public Player()
        {
            Name = "Player";
            Hand = new List<Card>();
            Result = Result.InProgress;
            PlayerChips = new Chips();
            IsDealer = false;
        }

        public Player(string name, bool isDealer)
        {
            Name = name;
            Hand = new List<Card>();
            Result = Result.InProgress;
            PlayerChips = new Chips();
            IsDealer = isDealer;
        }

        public Player(string name, Chips bet)
        {
            Name = name;
            Hand = new List<Card>();
            Result = Result.InProgress;
            PlayerChips = bet;
            IsDealer = false;
        }

        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public Result Result { get; set; }
        public Chips PlayerChips { get; set; }
        public bool IsDealer { get; set; }

        public string GetDisplayHandValue()
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
            Result = Result.InProgress;
            PlayerChips.BetAmount = 0;
        }
    }
}
