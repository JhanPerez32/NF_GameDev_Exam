using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.Turret;
using TMPro;
using UnityEngine;

public class UpgradeShopUI : MonoBehaviour
{
    public TMP_Text turretNameText;
    public TMP_Text turretLevelText;
    public TMP_Text turretMinRange;
    public TMP_Text turretMaxRange;
    public TMP_Text maxAmmo;
    public TMP_Text reloadTime;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        TurretTower turretComponent = target.turret?.GetComponent<TurretTower>();
        if (turretComponent != null && turretComponent.turretData != null)
        {
            TurretScriptable data = turretComponent.turretData;

            turretNameText.text = data.TurretName;
            turretLevelText.text = $"Lvl: {data.turretLevel}";
            turretMinRange.text = $"Min Range: {data.minRange}";
            turretMaxRange.text = $"Max Range: {data.maxRange}";
            maxAmmo.text = $"Ammo: {data.maxBullets}";
            reloadTime.text = $"Reload: {data.reloadTime}s";
        }
        else
        {
            turretNameText.text = "No Turret";
            turretLevelText.text = "0";
            turretMinRange.text = "???";
            turretMaxRange.text = "???";
            maxAmmo.text = "???";
            reloadTime.text = "???";
        }
    }
}
