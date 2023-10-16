using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public interface IGameService
    {
        /// <summary>
        /// Method that deals the starting hands to each player and the dealer. The dealer's second card is dealt face down.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public void DealStartingHands(List<Player> players, Game game);
        /// <summary>
        /// Displays the hand of a given list of players. Dealers hand is always shown with appropriate cards face down.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public void DisplayPlayers(List<Player> players);
        /// <summary>
        /// Displays the hand of the given list of players. If any cards are face down, it flips them to face up.
        /// </summary>
        /// <param name="players"></param>
        /// <returms></returms>
        public void DisplayFinalHand(List<Player> players);
        /// <summary>
        /// Determines what the player does on their turn.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <returns>Result of the turn.<returns>
        public Result TakeTurn(Player player, Game game);
        /// <summary>
        /// Determines what the dealer does on their turn.
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="game"></param>
        /// <returns>Result of the turn.<returns>
        public Result TakeDealerTurn(Player dealer, Game game);
        /// <summary>
        /// Determines the result of the player's hand after they take a turn.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Result of player's hand</returns>
        public Result ResolvePlayerHand(Player player);
        /// <summary>
        /// Determines the result of the dealer's hand after they take a turn.
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="game"></param>
        /// <returns>Result of the dealer's hand.</returns>
        public Result ResolveDealerHand(Player dealer, Game game);
        /// <summary>
        /// Determines the result of a game.
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public void ResolveGame(Player dealer, Player player);
        /// <summary>
        /// Resets the game so it can be played again.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        public void ResetGame(List<Player> players, Game game);
        /// <summary>
        /// Determines whether or not the player wants to play again.
        /// </summary>
        /// <returns>Boolean. True if the player want to play again, false if they don't.</returns>
        public bool PlayAgain();
    }
}
