using Blackjack.biz.Players;
using Blackjack.biz.Game;
using static Blackjack.biz.Constants;
using System.Numerics;

namespace Blackjack;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Blackjack!\n");

        IGameService gameService = new GameService();
        IPlayerService playerService = new PlayerService();

        bool validNumberOfPlayers = false;
        string numberOfPlayersInput = "";
        int numberOfPlayers = 0;

        while (!validNumberOfPlayers) //get the number of players
        {
            Console.WriteLine("Enter the number of players (minimum 1, maximum 4):");
            numberOfPlayersInput = Console.ReadLine();
            if(Int32.TryParse(numberOfPlayersInput, out numberOfPlayers)) {
                if(numberOfPlayers > 0 && numberOfPlayers <= 4)
                {
                    validNumberOfPlayers = true;
                }
            }
        }

        var game = new Game(numberOfPlayers);
        var dealer = new Player(DEALER_NAME);
        List<Player> players = playerService.InitalizePlayers(numberOfPlayers);
        players.Count();
        List<Player> playersAndDealer = new List<Player> { dealer };
        playersAndDealer.AddRange(players);

        game.Deck.Initalize();
        game.Deck.Shuffle();

        bool playGame = true;

        while (playGame)
        {
            gameService.DealStartingHands(playersAndDealer, game);

            foreach (Player player in players)
            {
                if (player.Result != Result.Blackjack && dealer.Result != Result.Blackjack) //end game immediately if either the player or the dealer get Blackjack
                {
                    gameService.DisplayPlayers(playersAndDealer);

                    bool playerTurn = true;

                    while (playerTurn)
                    {
                        player.Result = gameService.TakeTurn(player, game);

                        if (player.Result != Result.InProgress)
                        {
                            playerTurn = false;
                        }

                        gameService.DisplayPlayers(playersAndDealer);
                    }
                }
            }

            if (players.Where(p => p.Result != Result.Bust).Count() > 0) //if any player didn't bust
            {
                Console.WriteLine("It's the dealer's turn");
                dealer.Result = gameService.TakeDealerTurn(dealer, game);
            }

            gameService.DisplayFinalHand(playersAndDealer);

            foreach(Player player in players)
            {
                gameService.ResolveGame(dealer, player);
            }

            playGame = gameService.PlayAgain();

            if (playGame)
            {
                gameService.ResetGame(players, game);
                Console.Clear();
            }
        }
    }
}