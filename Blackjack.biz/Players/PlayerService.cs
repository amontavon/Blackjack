using Blackjack.biz.Bets;
using static Blackjack.biz.Constants;
namespace Blackjack.biz.Players
{
    public class PlayerService : IPlayerService
    {
        public List<Player> InitalizePlayers(int numberOfPlayers)
        {
            var playerList = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine("Enter Player name:"); //Get player name
                bool validName = false;
                var name = "";
                while (!validName) //loop until a valid answer is given
                {
                    name = Console.ReadLine();

                    if (name != DEALER_NAME && name != "")
                    {
                        validName = true;
                    }
                }

                Console.WriteLine("Enter inital chip amount:"); //Get initial amount of chips
                bool validChipAmount = false;
                var chipAmount = 0;
                while (!validChipAmount) //loop until a valid value is given
                {
                    var chipAmountInput = Console.ReadLine();
                    if (Int32.TryParse(chipAmountInput, out chipAmount)) //attempts to convert answer to int. if it's not an int, it's not a valid answer, and will keep looping.
                    {
                        if (chipAmount > 0 && chipAmount < 99999)
                        {
                            validChipAmount = true;
                        }
                    }
                }

                playerList.Add(new Player(name, new Bet(chipAmount)));
            }

            Console.Clear();

            return playerList;
        }

        public void GetPlayerBet(Player player)
        {
            Console.WriteLine($"{player.Name}, you have {player.PlayerBet.TotalChipAmount} chips to bet. How many chips would you want to bet?");
            bool validBetAmount = false;
            var betAmount = 0;
            while (!validBetAmount) //loop until a valid value is given
            {
                var betAmountInput = Console.ReadLine();
                if (Int32.TryParse(betAmountInput, out betAmount)) //attempts to convert answer to int. if it's not an int, it's not a valid answer, and will keep looping.
                {
                    if (betAmount > 0 && betAmount <= player.PlayerBet.TotalChipAmount)
                    {
                        validBetAmount = true;
                    }
                }
            }
            
            player.PlayerBet.BetAmount = betAmount;
            player.PlayerBet.TotalChipAmount -= betAmount;
        }
    }
}
