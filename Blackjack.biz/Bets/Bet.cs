using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz.Bets
{
    public class Bet
    {
        public Bet()
        {
            BetAmount = 0;
            TotalChipAmount = 0;
        }

        public Bet(int totalChipAmount)
        {
            BetAmount = 0;
            TotalChipAmount = totalChipAmount;
        }

        public int BetAmount { get; set; }
        public int TotalChipAmount { get; set; }
    }
}
