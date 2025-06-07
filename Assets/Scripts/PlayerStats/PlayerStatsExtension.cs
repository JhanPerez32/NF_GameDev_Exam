using NF.TD.PlayerCore;

namespace NF.TD.Extensions 
{
    public class PlayerStatsExtension
    {
        /// <summary>
        /// Adds money to the player's current balance.
        /// </summary>
        /// <param name="amount">Amount of money to add.</param>
        public static void AddMoney(int amount)
        {
            PlayerStats.Money += amount;
        }

        /// <summary>
        /// Attempts to spend money. Returns true if the player had enough money and the cost was deducted.
        /// </summary>
        /// <param name="cost">Amount of money to spend.</param>
        /// <returns>True if the transaction succeeded, false otherwise.</returns>
        public static bool SpendMoney(int cost)
        {
            if (PlayerStats.Money >= cost)
            {
                PlayerStats.Money -= cost;
                return true;
            }

            return false;
        }
    }
}
