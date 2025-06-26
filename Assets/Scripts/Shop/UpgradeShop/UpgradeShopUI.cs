using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.Turret;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NF.TD.Upgrade
{
    public class UpgradeShopUI : MonoBehaviour
    {
        public Image turretIcon;
        public TMP_Text turretNameText;
        public TMP_Text turretLevelText;
        public TMP_Text turretAccuracy;
        public TMP_Text turretMinRange;
        public TMP_Text turretMaxRange;
        public TMP_Text damage;
        public TMP_Text maxAmmo;
        public TMP_Text reloadTime;

        [Header("UpgradeButton")]
        public Button upgradeButton;
        public TMP_Text upgradeButtonText;

        [Header("SellButton")]
        public Button sellButton;
        public TMP_Text sellButtonText;

        private Node target;

        public void SetTarget(Node _target)
        {
            target = _target;

            transform.position = target.GetBuildPosition();

            TurretTower turretComponent = target.turret.GetComponent<TurretTower>();
            if (turretComponent != null && turretComponent.turretData != null)
            {
                TurretScriptable data = turretComponent.turretData;

                turretIcon.sprite = data.turretIcon;
                turretNameText.text = data.TurretName;
                turretLevelText.text = $"Lvl: {data.turretLevel}";
                turretAccuracy.text = $"Accuracy: {data.AccuracyPercentage:F0}%";
                turretMinRange.text = $"Min Range: {data.minRange}";
                turretMaxRange.text = $"Max Range: {data.maxRange}";
                damage.text = $"Damage: {data.projectileDamage}";
                maxAmmo.text = $"Max Ammo: {data.maxBullets}";
                reloadTime.text = $"Reload Time: {data.reloadTime}s";

                // Calculate and update the button text
                //int upgradeCost = Mathf.RoundToInt(data.turretCost * 0.5f * data.turretLevel);
                //upgradeButtonText.text = $"Upgrade \"{upgradeCost}\"";

                // Check max level
                if (data.turretLevel >= data.maxTurretLevel)
                {
                    upgradeButtonText.text = "MAX";
                    upgradeButton.interactable = false;
                }
                else
                {
                    int upgradeCost = Mathf.RoundToInt(data.turretCost * 0.5f * data.turretLevel);
                    upgradeButtonText.text = $"Upgrade \"{upgradeCost}\"";
                    upgradeButton.interactable = true;
                }

                // Show sell price on the Button
                int sellPrice = turretComponent.GetSellPrice();
                sellButtonText.text = $"Sell \"{sellPrice}\"";
            }
            else
            {
                turretNameText.text = "No Turret";
                turretLevelText.text = "0";
                turretMinRange.text = "???";
                turretMaxRange.text = "???";
                damage.text = "???";
                maxAmmo.text = "???";
                reloadTime.text = "???";
                upgradeButtonText.text = "Upgrade \"???\"";
            }

            Upgrading();
            Selling();
        }

        void Upgrading()
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() =>
            {
                TurretUpgradeManager.Instance.UpgradeTurret(target);
            });
        }

        void Selling()
        {
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(() =>
            {
                TurretUpgradeManager.Instance.SellTurret(target);
            });
        }
    }
}