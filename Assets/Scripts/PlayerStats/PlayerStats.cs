using NF.TD.UICore;
using UnityEngine;

namespace NF.TD.PlayerCore
{
    public class PlayerStats : MonoBehaviour
    {
        private static int money;
        public static int Money
        {
            get => money;
            set
            {
                money = value;
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.UpdateMoneyUI(money);
                }
            }
        }

        public static int Rounds;

        public int startingMoney = 500;

        private void Start()
        {
            Money = startingMoney; // Triggers UI update
            Rounds = 0;
        }
    }
}
