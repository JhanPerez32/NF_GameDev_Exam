using TMPro;
using UnityEngine;

namespace NF.TD.UICore
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI waveCountdownText;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void UpdateMoneyUI(int amount)
        {
            if (moneyText != null)
                moneyText.text = amount.ToString();
        }

        public void UpdateWaveCountdown(float countdown)
        {
            if (waveCountdownText != null)
                waveCountdownText.text = countdown.ToString("00.00");
        }
    }
}