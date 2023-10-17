using Blackjack.biz.Players;
using Blackjack.biz.Game;
using static Blackjack.biz.Constants;

namespace Blackjack;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Blackjack!\n");

        IGameService gameService = new GameService();
        IPlayerService playerService = new PlayerService();

        var game = gameService.InitializeGame(); //create game

        List<Player> players = playerService.InitalizePlayers(game.NumberOfPlayers); //get the number of players and create them
        var dealer = players.Where(p => p.IsDealer).Single();

        game.Deck.Initalize(); //create the starting deck
        game.Deck.DrawPile = game.Deck.Shuffle(game.Deck.DrawPile); //shuffle the cards in the draw pile

        bool playGame = true;

        while (playGame)
        {
            foreach(Player player in players) //get bets from every player
            {
                if (!player.IsDealer) //do not get a bet from the dealer
                {
                    playerService.GetPlayerBet(player);
                }
            }

            Console.Clear();

            gameService.DealStartingHands(players, game);

            foreach (Player player in players)
            {
                if (!player.IsDealer) //if the player isn't the dealer, then take turn
                {
                    if (player.Result != Result.Blackjack && dealer.Result != Result.Blackjack) //only take player turn if the dealer doesn't have a blackjack, and if they don't have a blackjack
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
                    }
                }
            }

            // below bool determines whether or not the dealer needs to take their turn
            // if every player either busted or got a blackjack, the dealer doesn't need to take their turn
            // if the dealer already has blackjack, they don't need to take their turn
            // else, they need to take their turn
            var doesDealerTakeTurn = players.Where(p => p.Result != Result.Bust && p.Result != Result.Blackjack).Count() > 0 && dealer.Result != Result.Blackjack;
            if (doesDealerTakeTurn)
            {
                Console.WriteLine("It's the dealer's turn");
                dealer.Result = gameService.TakeDealerTurn(dealer, game);
            }

            gameService.DisplayFinalHand(players);

            foreach(Player player in players)
            {
                if (!player.IsDealer) //do not resolve the dealer's hand against themselves
                {
                    gameService.ResolveGame(dealer, player);
                }
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