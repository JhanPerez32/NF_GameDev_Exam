using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.BuildCore;
using NF.TD.Extensions;
using NF.TD.PlayerCore;
using NF.TD.Turret;
using NF.TD.UICore;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace NF.TD.Upgrade 
{
    public class TurretUpgradeManager : MonoBehaviour
    {
        public static TurretUpgradeManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple TurretUpgradeManagers found!");
                return;
            }
            Instance = this;
        }

        public void UpgradeTurret(Node node)
        {
            if (node == null || node.turret == null) return;

            TurretTower turretComponent = node.turret.GetComponent<TurretTower>();
            if (turretComponent == null || turretComponent.turretData == null) return;

            TurretScriptable turretData = turretComponent.turretData;

            int upgradeCost = Mathf.RoundToInt(turretData.turretCost * 0.5f * turretData.turretLevel);

            if (!PlayerStatsExtension.SpendMoney(upgradeCost))
            {
                Debug.Log("Not Enough Money to Upgrade!");
                return;
            }

            // Track total spent cost
            turretComponent.totalSpentCost += upgradeCost;

            // Upgrade all other attributes
            turretData.UpgradeTurretAttributes(turretComponent);

            // Refreshes the UpgradeShopUI to show the new values
            UIManager.Instance.ShowUpgradeShop(node);
        }

        public void SellTurret(Node node)
        {
            if (node == null || node.turret == null) return;

            TurretTower turretComponent = node.turret.GetComponent<TurretTower>();
            if (turretComponent == null) return;

            // The selling price is calculated as 50% of the turret's TotalSpentCost,
            // which includes both the initial build cost and all upgrade costs.
            // This approach rewards strategic investments and fairly compensates the player.
            int sellPrice = turretComponent.GetSellPrice();
            PlayerStatsExtension.AddMoney(sellPrice);

            Destroy(node.turret);
            node.turret = null;

            BuildManager.instance.DeselectTurret();

            Debug.Log($"Turret sold for {sellPrice}");
        }
    }
}
