using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using System.Numerics;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public class GameService : IGameService
    {
        public List<Player> DealStartingHands(List<Player> players, List<Card> deck) //TODO: are lists reference parameters? if I make changes to the list, do I need to return it?
        {
            var dealer = players.Where(c => c.Name == Constants.DEALER_NAME).Single();
            players.Remove(dealer);
            var card = new Card();

            foreach (var player in players) //deal first card to all players
            {
                card = deck.First();
                player.Hand.Add(card);
                player.HandTotal = card.GetCardValue();
                deck.Remove(card);
            }

            //get first card for dealer
            card = deck.First();
            dealer.Hand.Add(card);
            dealer.HandTotal = card.GetCardValue();
            deck.Remove(card);

            //deal second card
            foreach (var player in players) //deal first card to all players
            {
                card = deck.First();
                player.Hand.Add(card);
                player.HandTotal += card.GetCardValue();
                deck.Remove(card);
            }

            //deal second card for dealer, face down
            card = deck.First();
            deck.Remove(card);
            card.IsHidden = true;
            dealer.Hand.Add(card);
            dealer.HiddenHandTotal = dealer.HandTotal + card.GetCardValue();

            players.Add(dealer);

            return players;
        }

        public void DisplayPlayers(List<Player> players)
        {
            foreach(var player in players)
            {
                Console.WriteLine(player.Name + ": " + player.DisplayHand());
                if(player.Name == Constants.DEALER_NAME)
                {
                    Console.WriteLine("Dealer has: " + player.HandTotal);
                }
                else
                {
                    Console.WriteLine("You have: " + player.HandTotal);
                }
            }
            Console.WriteLine("\n******\n"); //help with readability
        }

        public Result TakeDealerTurn(Player dealer, List<Card> deck)
        {
            return ResolveDealerHand(dealer, deck);
        }

        public Result TakeTurn(Player player, List<Card> deck)
        {
            Console.WriteLine("It's your turn. Would you like to STAND or HIT?");
            var input = Console.ReadLine();

            return TakeTurn(player, deck, input);
        }

        private Result TakeTurn(Player player, List<Card> deck, string input)
        {
            switch (input)
            {
                case "HIT":
                    Console.WriteLine("Player chose to hit");
                    return PlayerHit(player, deck);
                case "STAND":
                    Console.WriteLine("Player chose to stand");
                    return Result.Done;
                case "HELP":
                    DisplayHelp();
                    return Result.Valid;
                default:
                    Console.WriteLine("Not a valid input.");
                    return Result.Valid;
            }
        }

        public Result PlayerHit(Player player, List<Card> deck)
        {
            var card = deck.First();
            player.Hand.Add(card);
            player.HandTotal += card.GetCardValue();
            deck.Remove(card);

            if(player.Name == Constants.DEALER_NAME)
            {
                player.HiddenHandTotal += card.GetCardValue();
                return ResolveDealerHand(player, deck);
            }

            return ResolvePlayerHand(player);
        }

        public Result ResolvePlayerHand(Player player)
        {
            if(player.HandTotal == 21)
            {
                if (player.Hand.Count == 2)
                {
                    return Result.Blackjack;
                }

                return Result.TwentyOne;
            }
            else if(player.HandTotal < 21)
            {
                return Result.Valid;
            }

            return Result.Bust;
        }

        public Result ResolveDealerHand(Player dealer, List<Card> deck) 
        {
            if (dealer.HiddenHandTotal < 17) //if less than 17, dealer will hit again
            {
                PlayerHit(dealer, deck);
                return ResolveDealerHand(dealer, deck);
            }
            else if(dealer.HandTotal == 21)
            {
                if (dealer.Hand.Count == 2)
                {
                    return Result.Blackjack;
                }

                return Result.TwentyOne;
            }
            else if (dealer.HandTotal >= 17 && dealer.HandTotal < 21) //if between 17 and 21, dealer stands
            {
                return Result.Done;
            }
            else //dealer busted
            {
                return Result.Bust;
            }
        }

        public bool ResolveGame(Player dealer, Result dealerResult, Player player, Result playerResult)
        {
            if(playerResult == Result.Bust)
            {
                Console.WriteLine("You busted! Game over.\n");
            }
            else if(dealerResult == Result.Bust)
            {
                Console.WriteLine("Dealer busted! You win!\n");
            }
            else if(dealerResult == Result.Blackjack && playerResult == Result.Blackjack)
            {
                Console.WriteLine("You and the dealer both got Blackjack! You tie.\n");

                return PlayAgain();
            }
            else if(dealerResult == Result.Blackjack && playerResult != Result.Blackjack)
            {
                Console.WriteLine("Oh no! The dealer got Blackjack. You lose.\n");
            }
            else if (dealerResult != Result.Blackjack && playerResult == Result.Blackjack)
            {
                Console.WriteLine("You got Blackjack! You win!\n");
            }
            else if(dealer.HiddenHandTotal == player.HandTotal)
            {
                Console.WriteLine("It's a draw. No one wins.\n");
            }
            else if(dealer.HiddenHandTotal > player.HandTotal && dealerResult != Result.Bust)
            {
                Console.WriteLine("Dealer wins! Better luck next time.\n");
            }
            else
            {
                Console.WriteLine("Congrats! You win!\n");
            }

            return PlayAgain();
        }

        public bool PlayAgain()
        {
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("Enter Y for yes, N for no, or HELP for help.");
            var input = Console.ReadLine();

            return PlayAgain(input);
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

        private void DisplayFinalHand()
        {
            //method called at the start of resolve game to reveal dealers hand
        }

        private void DisplayHelp()
        {
            Console.WriteLine("help will be displayed");
        }
    }
}
