using NF.TD.BaseTurret;
using NF.TD.Turret;
using UnityEngine;

namespace NF.TD.Extensions 
{
    /// <summary>
    /// A static class that extends the `TurretScriptable` class with upgrade functionality.
    /// 
    /// The `UpgradeTurretAttributes` method retrieves current stats from the turret,
    /// sends them to `TurretUpgradeUtils.UpgradeAttributes`, and applies the upgraded values
    /// back to the ScriptableObject.
    /// 
    /// It also increments the turret's level after an upgrade.
    /// 
    /// This keeps the `TurretScriptable` class clean while still allowing easy stat upgrading logic,
    /// and provides better separation of concerns.
    /// </summary>
    public static class TurretScriptableExtensions
    {
        public static void UpgradeTurretAttributes(this TurretScriptable turretData, TurretTower turretComponent)
        {
            var current = new TurretAttributes
            {
                minRange = turretData.minRange,
                maxRange = turretData.maxRange,
                maxBullets = turretData.maxBullets,
                fireRate = turretData.fireRate,
                spreadScale = turretComponent.gun.spreadScale,
                reloadTime = turretData.reloadTime,
                projectileDamage = turretData.projectileDamage
            };

            TurretAttributes upgraded = TurretUpgradeUtils.UpgradeAttributes(current);

            // Apply upgraded values back to the ScriptableObject instance
            turretData.minRange = upgraded.minRange; //Updating
            turretData.maxRange = upgraded.maxRange; //Updating
            turretData.maxBullets = upgraded.maxBullets; //Updating
            turretData.fireRate = upgraded.fireRate; //Updating
            turretData.spreadScale = upgraded.spreadScale; //Temporary fix for the moment until I find whats wrong when using the TurretData like the rest
            turretData.reloadTime = upgraded.reloadTime; //Updating
            turretData.projectileDamage = upgraded.projectileDamage; //Updating

            turretData.turretLevel++;

            Debug.Log("Spread Scale: " + turretData.spreadScale);
        }
    }
}

