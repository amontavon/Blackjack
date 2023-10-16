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
                    chipAmount = Convert.ToInt32(Console.ReadLine());

                    if (chipAmount > 0 && chipAmount < 99999)
                    {
                        validChipAmount = true;
                    }
                }

                playerList.Add(new Player(name, chipAmount));
            }

            Console.Clear();

            return playerList;
        }
    }
}
