using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz.Bets
{
    // Class that handles the amount of chips a player has, as well as their bet
    public class Chips
    {
        public Chips()
        {
            BetAmount = 0;
            TotalChipAmount = 0;
        }

        public Chips(int totalChipAmount)
        {
            BetAmount = 0;
            TotalChipAmount = totalChipAmount;
        }

        public int BetAmount { get; set; }
        public int TotalChipAmount { get; set; }
    }
}
