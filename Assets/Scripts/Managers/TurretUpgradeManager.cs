using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.Extensions;
using NF.TD.PlayerCore;
using NF.TD.Turret;
using NF.TD.UICore;
using UnityEngine;

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

            turretData.turretLevel++;

            Debug.Log($"Turret upgraded to Level {turretData.turretLevel}. Money left: {PlayerStats.Money}");

            // Refreshes the UpgradeShopUI to show the new values
            UIManager.Instance.ShowUpgradeShop(node);
        }
    }
}
