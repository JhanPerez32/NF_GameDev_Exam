using NF.TD.BaseTurret;
using NF.TD.Turret;

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
                spreadScale = turretData.spreadScale,
                reloadTime = turretData.reloadTime,
                projectileDamage = turretData.projectileDamage
            };

            TurretAttributes upgraded = TurretUpgradeUtils.UpgradeAttributes(current);

            // Apply upgraded values back to the ScriptableObject instance
            turretData.minRange = upgraded.minRange;
            turretData.maxRange = upgraded.maxRange;
            turretData.maxBullets = upgraded.maxBullets;
            turretData.fireRate = upgraded.fireRate;
            turretData.spreadScale = upgraded.spreadScale;
            turretData.reloadTime = upgraded.reloadTime;
            turretData.projectileDamage = upgraded.projectileDamage;

            turretData.turretLevel++;
        }
    }
}

