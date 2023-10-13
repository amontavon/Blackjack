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

        ICardService cardService = new CardService();
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
            gameService.DisplayPlayers(players);

            bool playerTurn = true;
            Result playerResult = Result.Valid;

            while (playerTurn)
            {
                playerResult = gameService.TakeTurn(player, game);

                if (playerResult != Result.Valid)
                {
                    playerTurn = false;
                }

                gameService.DisplayPlayers(players);
            }

            var dealerResult = Result.Valid;
            if (playerResult != Result.Bust)
            {
                dealerResult = gameService.TakeDealerTurn(dealer, game);
            }

            gameService.DisplayPlayers(players);

            playGame = gameService.ResolveGame(dealer, dealerResult, player, playerResult);

            if (playGame)
            {
                gameService.ResetGame(players, game);
            }
        }
    }
}