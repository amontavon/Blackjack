using Blackjack.biz.Cards;
using Blackjack.biz.Players;
using System.ComponentModel.Design;
using System.Numerics;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Game
{
    public class GameService : IGameService
    {
        public Game InitializeGame()
        {
            bool validNumberOfPlayers = false;
            string numberOfPlayersInput = "";
            int numberOfPlayers = 0;

            while (!validNumberOfPlayers) //get the number of players
            {
                Console.WriteLine("Enter the number of players (minimum 1, maximum 4):");
                numberOfPlayersInput = Console.ReadLine();
                if (Int32.TryParse(numberOfPlayersInput, out numberOfPlayers)) //make sure value is an int
                {
                    if (numberOfPlayers > 0 && numberOfPlayers <= 4) //make sure int is a valid number
                    {
                        validNumberOfPlayers = true;
                    }
                }
            }

           return new Game(numberOfPlayers);
        }
        public void DealStartingHands(List<Player> players, Game game)
        {
            var deck = game.Deck;
            var cardsInPlay = game.Deck.CardsInPlay;

            var card = new Card();

            foreach (var player in players) //deal first card to all players, including dealer
            {
                card = deck.DrawCard();
                cardsInPlay.Add(card);
                player.Hand.Add(card);
            }

            var dealer = players.Where(p => p.IsDealer).Single();
            players.Remove(dealer);

            foreach (var player in players) //deal second card to all players, minus dealer
            {
                card = deck.DrawCard();
                cardsInPlay.Add(card);
                player.Hand.Add(card);

                if(BlackJackScorer.GetScore(player.Hand, true) == 21) //check for Blackjack
                {
                    player.Result = Result.Blackjack;
                }
            }

            //deal second card for dealer, face down
            card = deck.DrawCard();
            card.IsHidden = true;
            cardsInPlay.Add(card);
            dealer.Hand.Add(card);

            if(BlackJackScorer.GetScore(dealer.Hand, true) == 21)
            {
                dealer.Result = Result.Blackjack;
            }

            players.Insert(0, dealer); //Add dealer to the start of the list

            return;
        }

        public void DisplayPlayers(List<Player> players)
        {
            Console.WriteLine("\n******\n"); //help with readability

            foreach (var player in players)
            {
                Console.WriteLine(player.Name + ": " + player.GetDisplayHandValue());
                if(player.IsDealer)
                {
                    Console.WriteLine("Dealer has: " + BlackJackScorer.GetScore(player.Hand, false));
                }
                else
                {
                    Console.WriteLine(player.Name + " has: " + BlackJackScorer.GetScore(player.Hand, false));
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
            if(player.Hand.Count == 2) //if it's the players first turn, they have the option to double down.
            {
                Console.WriteLine(player.Name + ", it's your turn. Would you like to STAND, HIT, or DOUBLE DOWN? Or type HELP for help.");
            }
            else
            {
                Console.WriteLine(player.Name + ", it's your turn. Would you like to STAND or HIT? Or type HELP for help.");
            }
            var input = Console.ReadLine();

            return TakeTurn(player, game, input);
        }

        public Result PlayerHit(Player player, Game game)
        {
            var card = game.Deck.DrawCard();
            game.Deck.CardsInPlay.Add(card);
            player.Hand.Add(card);

            if(player.IsDealer)
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

        public void ResolveGame(Player dealer, Player player)
        {
            var dealerScore = BlackJackScorer.GetScore(dealer.Hand, true);
            var playerScore = BlackJackScorer.GetScore(player.Hand, false);

            if(player.Result == Result.Bust) //if the player busted, it doesn't matter what the dealer did
            {
                Console.WriteLine(player.Name + " busted!\n");
            }
            else if(dealer.Result == Result.Bust) //if the player didn't bust, but the dealer did, player wins
            {
                Console.WriteLine("Dealer busted! " + player.Name + " wins!\n");
                ResolveBet(player, true);
            }
            else if(dealer.Result == Result.Blackjack && player.Result == Result.Blackjack) //if they both get blackjack, it's a tie
            {
                Console.WriteLine(player.Name + " and the dealer both got Blackjack! You tie.\n");
                ResolveBet(player, false);
            }
            else if(dealer.Result == Result.Blackjack) //don't need to check both: above checked above to see if they are both blackjack
            {
                Console.WriteLine("Oh no! The dealer got Blackjack. " + player.Name + " loses.\n");
            }
            else if (player.Result == Result.Blackjack) //if the player gets blackjack
            {
                Console.WriteLine(player.Name + " got Blackjack!\n");
                ResolveBet(player, true);
            }
            else if(dealerScore == playerScore) //if neither the dealer or player has blackjack and they have the same score.
            {
                Console.WriteLine(player.Name + " tied the dealer.\n");
                ResolveBet(player, false);
            }
            else if(dealerScore > playerScore) //if the dealer outscores the player but doesn't bust
            {
                Console.WriteLine("Dealer has a better score than " + player.Name + "! Better luck next time.\n");
            }
            else //else, player wins
            {
                Console.WriteLine("Congrats, " + player.Name + "! You have a better score than the dealer!\n");
                ResolveBet(player, true);
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

            game.Deck.Discard.AddRange(game.Deck.CardsInPlay); //add cards from this game to the discard pile
            game.Deck.CardsInPlay.Clear(); //clear cards in play list
        }

        public void DisplayFinalHand(List<Player> players)
        {
            var dealer = players.Where(p => p.IsDealer).Single();
            var card = dealer.Hand.Where(c => c.IsHidden).Single();

            card.IsHidden = false;

            DisplayPlayers(players);
        }

        public bool DisplayFinalChipAmounts(List<Player> players)
        {
            foreach(var player in players)
            {
                if (!player.IsDealer) //do not check the dealer
                {
                    if (player.PlayerChips.TotalChipAmount == 0) //if a player is out of chips
                    {
                        Console.WriteLine($"{player.Name} is out of chips. They can no longer play.");
                    }
                    else
                    {
                        Console.WriteLine($"{player.Name} has {player.PlayerChips.TotalChipAmount} chips remaining.");
                    }
                }
            }

            players.RemoveAll(p => p.PlayerChips.TotalChipAmount == 0 && !p.IsDealer); //remove all players with 0 chips left from the game

            if (players.Count == 1) //if all players are out, end the game.
            {
                Console.WriteLine("All players are out of chips. Thanks for playing.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines the result of the bet. If the player won, isVictory is true. If the player tied, isVictory is false. This method is not called if a player loses, as their bet is subtracted from the total amount of chips upon making the bet.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isVictory"></param>
        /// <returns></returns>
        private void ResolveBet(Player player, bool isVictory)
        {
            if(player.Result == Result.Blackjack && isVictory) //if the player got Blackjack, and the dealer didn't, 
            {
                player.PlayerChips.TotalChipAmount += (int) Math.Ceiling(player.PlayerChips.BetAmount * 1.5) * 2;
            }
            else if(isVictory) //if the player won and didn't get blackjack, update 
            {
                player.PlayerChips.TotalChipAmount += player.PlayerChips.BetAmount * 2;
            }
            else //Player tied: they get their bet back
            {
                player.PlayerChips.TotalChipAmount += player.PlayerChips.BetAmount;
            }
        }

        /// <summary>
        /// Based on player input, determines which action to take
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private Result TakeTurn(Player player, Game game, string input)
        {
            switch (input)
            {
                case "HIT":
                    Console.Clear();
                    return PlayerHit(player, game);
                case "STAND":
                    Console.Clear();
                    return Result.Done;
                case "DOUBLE DOWN":
                    if(player.Hand.Count != 2)
                    {
                        Console.WriteLine("Not a valid input.");
                        return Result.InProgress;
                    }
                    return HandleDoubleDown(player, game);
                case "HELP":
                    DisplayHelp();
                    return Result.InProgress;
                default:
                    Console.WriteLine("Not a valid input.");
                    return Result.InProgress;
            }
        }

        /// <summary>
        /// Handles bet and result of a player doubling down
        /// </summary>
        /// <param name="player"></param>
        /// <param name="game"></param>
        /// <returns></returns>
        private Result HandleDoubleDown(Player player, Game game)
        {
            if (player.PlayerChips.TotalChipAmount < player.PlayerChips.BetAmount) //check to make sure the player have enough chips to double down
            {
                Console.WriteLine("Unable to double down. Not enough chips.");
                return Result.InProgress;
            }

            player.PlayerChips.TotalChipAmount -= player.PlayerChips.BetAmount;
            player.PlayerChips.BetAmount += player.PlayerChips.BetAmount;

            var result = PlayerHit(player, game);
            result = result == Result.Bust ? result : Result.Done; //if the result of doubling down was a bust, return Result.Bust. otherwise, the turn is over, return Result.Done
            return result;
        }

        /// <summary>
        /// Determines whether or not to play another game.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool PlayAgain(string input)
        {
            switch (input)
            {
                case "Y":
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

        /// <summary>
        /// Displays instructions on how to play the game.
        /// </summary>
        /// <returns></returns>
        private void DisplayHelp()
        {
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("The object of the game is to attempt to beat the dealer by getting a count as close to 21 as possible, without going over 21\n");
            Console.WriteLine("Before the deal begins, each player places a bet. In this game, there is a minimum bet of 1 chip, and a maximum amount of 99,999.\n");
            Console.WriteLine("An ace is worth an 11 unless it would cause a hand to bust. Otherwise, it is worth 1 point. Face cards are 10 and any other card is its pip value.\n");
            Console.WriteLine("If a player's first two cards are an ace and a ten-card (a picture card or 10), giving a count of 21 in two cards, this is a natural or \"blackjack.\" If any player has a natural and the dealer does not, the dealer immediately pays that player one and a half times the amount of their bet. If the dealer has a natural, they immediately collect the bets of all players who do not have naturals, (but no additional amount). If the dealer and another player both have naturals, the bet of that player is a stand-off (a tie), and the player takes back his chips.");
            Console.WriteLine("When all the players have placed their bets, the dealer gives one card face up to each player, and then one card face up to themselves. Another round of cards is then dealt face up to each player, but the dealer takes the second card face down. Thus, each player except the dealer receives two cards face up, and the dealer receives one card face up and one card face down. ");
            Console.WriteLine("On a player's turn, they must decide whether to \"stand\" (not ask for another card) or \"hit\" (ask for another card in an attempt to get closer to a count of 21, or even hit 21 exactly). Thus, a player may stand on the two cards originally dealt to them, or they may ask the dealer for additional cards, one at a time, until deciding to stand on the total (if it is 21 or under), or goes \"bust\" (if it is over 21). In the latter case, the player loses and the dealer collects the bet wagered. The dealer then turns to the next player and serves them in the same manner.\n");
            Console.WriteLine("Another option open to the player is doubling their bet. After being dealt the inital two cards, they can place a bet equal to the original bet, and the dealer gives the player just one card. Note that the dealer does not have the option of doubling down.\n");

            Console.WriteLine("Rules adapted from https://bicyclecards.com/how-to-play/blackjack/");
        }
    }
}
