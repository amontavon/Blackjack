using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public interface IGameService
    {
        public List<Player> DealStartingHands(List<Player> players, List<Card> deck);
        public void DisplayPlayers(List<Player> players);
        public Result TakeTurn(Player player, List<Card> deck);
        public Result TakeDealerTurn(Player dealer, List<Card> deck);
        public Result ResolvePlayerHand(Player player);
        public Result ResolveDealerHand(Player dealer, List<Card> deck);
        public bool ResolveGame(Player dealer, Result dealerResult, Player player, Result playerResult);
        public bool PlayAgain();
    }
}
