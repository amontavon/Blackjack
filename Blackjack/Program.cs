using Blackjack.biz.Players;
using Blackjack.biz.Cards;
using Blackjack.biz.Game;
using static Blackjack.biz.Constants;

namespace Blackjack;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Blackjack!\n");

        IGameService gameService = new GameService();

        var dealer = new Player(DEALER_NAME);
        var player = new Player();

        List<Player> players = new List<Player>() { dealer, player };

        var game = new Game();
        game.Deck.Initalize();
        game.Deck.Shuffle();

        bool playGame = true;

        while (playGame)
        {
            gameService.DealStartingHands(players, game);

            if (player.Result != Result.Blackjack && dealer.Result != Result.Blackjack) //end game immediately if either the player or the dealer get Blackjack
            {

                gameService.DisplayPlayers(players);

                bool playerTurn = true;

                while (playerTurn)
                {
                    player.Result = gameService.TakeTurn(player, game);

                    if (player.Result != Result.InProgress)
                    {
                        playerTurn = false;
                    }

                    gameService.DisplayPlayers(players);
                }

                if (player.Result != Result.Bust)
                {
                    dealer.Result = gameService.TakeDealerTurn(dealer, game);
                }
            }

            gameService.DisplayFinalHand(players);

            gameService.ResolveGame(dealer, dealer.Result, player, player.Result);

            playGame = gameService.PlayAgain();

            if (playGame)
            {
                gameService.ResetGame(players, game);
                Console.Clear();
            }
        }
    }
}