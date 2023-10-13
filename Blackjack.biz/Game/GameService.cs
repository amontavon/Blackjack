using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using System.Numerics;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public class GameService : IGameService
    {
        public void DealStartingHands(List<Player> players, Game game)
        {
            var deck = game.Deck;
            var cardsInPlay = game.CardsInPlay;

            var card = new Card();

            foreach (var player in players) //deal first card to all players, including dealer
            {
                if(deck.Cards.Count == 0)
                {
                    ResetDeck(game);
                }

                card = deck.Draw();
                cardsInPlay.Cards.Add(card);
                player.Hand.Add(card);
            }

            var dealer = players.Where(c => c.Name == DEALER_NAME).Single();
            players.Remove(dealer);

            foreach (var player in players) //deal second card to all players, minus dealer
            {
                if (deck.Cards.Count == 0)
                {
                    ResetDeck(game);
                }

                card = deck.Draw();
                cardsInPlay.Cards.Add(card);
                player.Hand.Add(card);

                if(BlackJackScorer.GetScore(player.Hand, true) == 21) //check for Blackjack
                {
                    player.Result = Result.Blackjack;
                }
            }

            //deal second card for dealer, face down
            card = deck.Draw();
            card.IsHidden = true;
            cardsInPlay.Cards.Add(card);
            dealer.Hand.Add(card);

            if(BlackJackScorer.GetScore(dealer.Hand, false) == 21)
            {
                dealer.Result = Result.Blackjack;
            }

            players.Insert(0, dealer); //Add dealer to the start of the list

            return;
        }

        private void ResetDeck(Game game)
        {
            game.Discard.Shuffle();
            game.Deck = game.Discard;
            game.Discard.Cards.Clear();
        }

        public void DisplayPlayers(List<Player> players)
        {
            Console.WriteLine("\n******\n"); //help with readability

            foreach (var player in players)
            {
                Console.WriteLine(player.Name + ": " + player.GetDisplayHandValue());
                if(player.Name == Constants.DEALER_NAME)
                {
                    Console.WriteLine("Dealer has: " + BlackJackScorer.GetScore(player.Hand, false));
                }
                else
                {
                    Console.WriteLine("You have: " + BlackJackScorer.GetScore(player.Hand, false));
                }
            }

            Console.WriteLine("\n******\n"); 
        }

        public Result TakeDealerTurn(Player dealer, Game game)
        {
            return ResolveDealerHand(dealer, game);
        }

        public Result TakeTurn(Player player, Game game)
        {
            Console.WriteLine("It's your turn. Would you like to STAND or HIT?");
            var input = Console.ReadLine();

            return TakeTurn(player, game, input);
        }

        public Result PlayerHit(Player player, Game game)
        {
            if(game.Deck.Cards.Count == 0)
            {
                ResetDeck(game);
            }

            var card = game.Deck.Draw();
            game.CardsInPlay.Cards.Add(card);
            player.Hand.Add(card);

            if(player.Name == DEALER_NAME)
            {
                return ResolveDealerHand(player, game);
            }

            return ResolvePlayerHand(player);
        }

        public Result ResolvePlayerHand(Player player)
        {
            var score = BlackJackScorer.GetScore(player.Hand, false);

            if(score == 21) //if the player has 21, determine if it's blackjack or not (needed for betting)
            {
                if (player.Hand.Count == 2)
                {
                    return Result.Blackjack;
                }

                return Result.TwentyOne;
            }
            else if(score < 21)
            {
                return Result.InProgress;
            }

            return Result.Bust;
        }

        public Result ResolveDealerHand(Player dealer, Game game) 
        {
            var score = BlackJackScorer.GetScore(dealer.Hand, true);

            if (score < 17) //if less than 17, dealer will hit again
            {
                PlayerHit(dealer, game);
                return ResolveDealerHand(dealer, game);
            }
            else if(score == 21) //if the dealer has 21, determine if it's blackjack or not (needed for betting)
            {
                if (dealer.Hand.Count == 2)
                {
                    return Result.Blackjack;
                }

                return Result.TwentyOne;
            }
            else if (score >= 17 && score < 21) //if between 17 and 21, dealer stands
            {
                return Result.Done;
            }
            else //dealer busted
            {
                return Result.Bust;
            }
        }

        public void ResolveGame(Player dealer, Result dealerResult, Player player, Result playerResult)
        {
            var dealerScore = BlackJackScorer.GetScore(dealer.Hand, true);
            var playerScore = BlackJackScorer.GetScore(player.Hand, false);

            if(playerResult == Result.Bust) //if the player busted, it doesn't matter what the dealer did
            {
                Console.WriteLine("You busted! Game over.\n");
            }
            else if(dealerResult == Result.Bust) //if the player didn't bust, but the dealer did, player wins
            {
                Console.WriteLine("Dealer busted! You win!\n");
            }
            else if(dealerResult == Result.Blackjack && playerResult == Result.Blackjack) //if they both get blackjack, it's a tie
            {
                Console.WriteLine("You and the dealer both got Blackjack! You tie.\n");
            }
            else if(dealerResult == Result.Blackjack) //don't need to check both: above checked above to see if they are both blackjack
            {
                Console.WriteLine("Oh no! The dealer got Blackjack. You lose.\n");
            }
            else if (playerResult == Result.Blackjack)
            {
                Console.WriteLine("You got Blackjack! You win!\n");
            }
            else if(dealerScore == playerScore)
            {
                Console.WriteLine("It's a draw. No one wins.\n");
            }
            else if(dealerScore > playerScore)
            {
                Console.WriteLine("Dealer wins! Better luck next time.\n");
            }
            else
            {
                Console.WriteLine("Congrats! You win!\n");
            }
        }

        public bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("Enter Y for yes, N for no, or HELP for help.");
            var input = Console.ReadLine();

            return PlayAgain(input);
        }

        public void ResetGame(List<Player> players, Game game)
        {
            foreach(var player in players)
            {
                player.ResetPlayer();
            }

            game.Discard.Cards.AddRange(game.CardsInPlay.Cards);
            game.CardsInPlay.Cards.Clear();
        }

        public void DisplayFinalHand(List<Player> players)
        {
            var dealer = players.Where(c => c.Name == DEALER_NAME).Single();
            var card = dealer.Hand.Where(c => c.IsHidden).Single();

            card.IsHidden = false;

            DisplayPlayers(players);
        }

        private Result TakeTurn(Player player, Game game, string input)
        {
            switch (input)
            {
                case "HIT":
                    Console.WriteLine("Player chose to hit");
                    return PlayerHit(player, game);
                case "STAND":
                    Console.WriteLine("Player chose to stand");
                    return Result.Done;
                case "HELP":
                    DisplayHelp();
                    return Result.InProgress;
                default:
                    Console.WriteLine("Not a valid input.");
                    return Result.InProgress;
            }
        }

        private bool PlayAgain(string input)
        {
            switch (input)
            {
                case "Y":
                    Console.WriteLine("a game will be played");
                    return true;
                case "N":
                    Console.WriteLine("Goodbye.");
                    return false;
                case "HELP":
                    DisplayHelp();
                    return PlayAgain();
                default:
                    Console.WriteLine("Not a valid input.");
                    return PlayAgain();
            }
        }

        private void DisplayHelp()
        {
            Console.WriteLine("help will be displayed");
        }
    }
}
