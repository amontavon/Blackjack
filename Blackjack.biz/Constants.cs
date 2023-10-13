using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz
{
    public static class Constants
    {
        public static string DEALER_NAME = "Dealer";

        public enum Suit
        {
            Spade,
            Club,
            Diamond,
            Heart
        }

        public enum CardValue
        {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
        }

        public enum Result
        {
            Bust,
            Valid,
            TwentyOne,
            Blackjack,
            Done
        }
    }
}
