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

        var game = gameService.InitializeGame();

        var dealer = new Player(DEALER_NAME);
        List<Player> players = playerService.InitalizePlayers(game.NumberOfPlayers);
        List<Player> playersAndDealer = new List<Player> { dealer };
        playersAndDealer.AddRange(players);

        game.Deck.Initalize();
        game.Deck.Shuffle();

        bool playGame = true;

        while (playGame)
        {
            foreach(Player player in players) //get bets from every player
            {
                playerService.GetPlayerBet(player);
            }

            Console.Clear();

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

            if (players.Where(p => p.Result != Result.Bust).Count() > 0 && dealer.Result != Result.Blackjack
                && players.Where(p => p.Result != Result.Blackjack).Count() < game.NumberOfPlayers) //if any player didn't bust, the dealer didn't get Blackjack, or all players didn't get Blackjack
            {
                Console.WriteLine("It's the dealer's turn");
                dealer.Result = gameService.TakeDealerTurn(dealer, game);
            }

            gameService.DisplayFinalHand(playersAndDealer);

            foreach(Player player in players)
            {
                gameService.ResolveGame(dealer, player);
            }

            var canContinue = gameService.DisplayFinalChipAmounts(players);

            playGame = canContinue ? gameService.PlayAgain() : false; //if you can continue, do you want to play again?

            if (playGame)
            {
                gameService.ResetGame(players, game);
                Console.Clear();
            }
        }
    }
}