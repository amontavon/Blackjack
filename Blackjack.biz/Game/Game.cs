using Blackjack.biz.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz.Game
{
    public class Game
    {
        public Game()
        {
            Deck = new Deck();
            NumberOfPlayers = 1;
        }

        public Game(int numberOfPlayers)
        {
            Deck = new Deck();
            NumberOfPlayers = numberOfPlayers;
        }

        public int NumberOfPlayers { get; set; }
        public Deck Deck { get; set; }
    }
}
