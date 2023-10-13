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
            CardsInPlay = new Deck();
            Discard = new Deck();
        }
        public Deck Deck { get; set; }
        public Deck CardsInPlay { get; set; }
        public Deck Discard { get; set; }
    }
}
