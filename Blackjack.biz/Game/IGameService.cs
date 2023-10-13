using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public interface IGameService
    {
        public void DealStartingHands(List<Player> players, Game game);
        public void DisplayPlayers(List<Player> players);
        public Result TakeTurn(Player player, Game game);
        public Result TakeDealerTurn(Player dealer, Game game);
        public Result ResolvePlayerHand(Player player);
        public Result ResolveDealerHand(Player dealer, Game game);
        public bool ResolveGame(Player dealer, Result dealerResult, Player player, Result playerResult);
        public void ResetGame(List<Player> players, Game game);
        public bool PlayAgain();
    }
}
