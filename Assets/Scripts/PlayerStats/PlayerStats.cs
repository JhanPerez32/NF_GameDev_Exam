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

        private static int lives;
        public static int Lives
        {
            get => lives;
            set
            {
                lives = value;
                //Debug.Log($"Lives SET to: {lives}");
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.UpdateLivesUI(lives);
                }
            }
        }

        public static int Rounds;

        public int startingMoney = 500;
        public int startingLives = 5;

        private void Start()
        {
            Money = startingMoney; // Triggers UI update
            Lives = startingLives; // Triggers UI update
            Rounds = 0;
        }
    }
}
