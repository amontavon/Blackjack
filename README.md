# Blackjack

Welcome to Blackjack!

The rules for this game were adapted from https://bicyclecards.com/how-to-play/blackjack/

This game is a C# Windows Console Application. To play, clone the repo and run the Blackjack.Console project.

At start, the game will ask for the number of players. Enter a number 1-4 to continue.

For each player, the game will ask for their name as it creates each player, and how many chips they would like. At this time, this is the only place to set a player's chip amount. Other than winning, there is no other way to add chips to a player's total.

The dealer will deal a card to every player face-up. Then the dealer will deal a second card to everyone, with the dealer's second card being placed face-down.

If a dealer has a Blackjack, the game ends. Any player who also has a Blackjack will tie, and any player who doesn't will lose.

Otherwise, each player takes their turn, choosing to STAND or HIT. Players can hit until they BUST.

After all players take a turn, the dealer will take their turn. If the value of the dealer's hand is less than 17, they will hit, and continue to hit, until they either bust or have a hand value between 17 and 21.

At the end of the game, each player's score will be compared to the dealer's to see if they won.

After, they can decide whether or not they want to play again.

At any point during a player's turn, they can type HELP to see the rules.

Players can also choose to DOUBLE DOWN on their turn, if they have not yet hit. This will have the dealer deal them one card and end their turn. Doubling down will result in doubling their bet.

If a player runs out of chips, they are removed from the game. The game will continue until the players decide not to play again, or all players run out of chips.

**NOTE: There are no unit tests at this time, so the game is tested by hand. While the game is playable, there may be bugs that have not yet been discovered. They will be fixed as soon as they're found.**

Future plans include:

  -Using five decks instead of one
  
  -Split
  
  -Insurance
  
  -Ability to add chips during play
  
  -Unit Tests
