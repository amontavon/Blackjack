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

        var dealer = new Player {
            Name = DEALER_NAME,
            Hand = new List<Card>()
        };

        var player = new Player
        {
            Name = "Player", //TODO: Let player input name
            Hand = new List<Card>()
        };

        List<Player> players = new List<Player>() { dealer, player };

        List<Card> deck = cardService.CreateDeck();

        deck = cardService.ShuffleDeck(deck);

        gameService.DealStartingHands(players, deck);

        gameService.DisplayPlayers(players);

        bool playGame = true;

        while (playGame)
        {
            bool playerTurn = true;
            Result playerResult = Result.Valid;

            while (playerTurn)
            {
                playerResult = gameService.TakeTurn(player, deck);

                if (playerResult != Result.Valid)
                {
                    playerTurn = false;
                }

                gameService.DisplayPlayers(players);
            }

            var dealerResult = gameService.TakeDealerTurn(dealer, deck);

            gameService.DisplayPlayers(players);

            playGame = gameService.ResolveGame(dealer, dealerResult, player, playerResult);
        }
    }
}