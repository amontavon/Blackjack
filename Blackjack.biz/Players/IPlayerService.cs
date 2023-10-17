using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.biz.Players
{
    public interface IPlayerService
    {
        /// <summary>
        /// Creates an amount of players based on the numberofPlayers passed, and asks users for input.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        /// <returns>List of players</returns>
        public List<Player> InitalizePlayers(int numberOfPlayers);
        public void GetPlayerBet(Player player);
    }
}
