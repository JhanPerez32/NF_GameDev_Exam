using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.Turret;
using NF.TD.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopUI : MonoBehaviour
{
    public Image turretIcon;
    public TMP_Text turretNameText;
    public TMP_Text turretLevelText;
    public TMP_Text turretMinRange;
    public TMP_Text turretMaxRange;
    public TMP_Text maxAmmo;
    public TMP_Text reloadTime;

    [Header("UpgradeButton")]
    public Button upgradeButton;
    public TMP_Text upgradeButtonText;

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
            turretMinRange.text = $"Min Range: {data.minRange}";
            turretMaxRange.text = $"Max Range: {data.maxRange}";
            maxAmmo.text = $"Ammo: {data.maxBullets}";
            reloadTime.text = $"Reload: {data.reloadTime}s";

            // Calculate and update the button text
            int upgradeCost = Mathf.RoundToInt(data.turretCost * 0.5f * data.turretLevel);
            upgradeButtonText.text = $"Upgrade \"{upgradeCost}\"";
        }
        else
        {
            turretNameText.text = "No Turret";
            turretLevelText.text = "0";
            turretMinRange.text = "???";
            turretMaxRange.text = "???";
            maxAmmo.text = "???";
            reloadTime.text = "???";
            upgradeButtonText.text = "Upgrade \"???\"";
        }

        upgradeButton.onClick.RemoveAllListeners(); // Clear previous listeners
        upgradeButton.onClick.AddListener(() =>
        {
            TurretUpgradeManager.Instance.UpgradeTurret(target);
        });
    }
}
