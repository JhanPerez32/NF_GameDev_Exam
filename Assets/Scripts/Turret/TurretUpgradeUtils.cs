using UnityEngine;

namespace NF.TD.BaseTurret 
{
    public class TurretUpgradeUtils
    {
        /// <summary>
        /// It provides the `UpgradeAttributes` method that calculates the upgraded values for turret stats
        /// based on predefined rules (e.g., increase fireRate by 0.1, decrease spreadScale by 0.1, etc.).
        /// 
        /// It also contains a helper method to round float values to two decimal places to keep data clean
        /// and avoid floating-point imprecision (e.g., 4.000001 becomes 4.01).
        /// 
        /// This class promotes separation of logic from data, making upgrades easy to manage and adjust.
        /// </summary>
        public static TurretAttributes UpgradeAttributes(TurretAttributes current)
        {
            TurretAttributes upgraded = new TurretAttributes
            {
                minRange = RoundToTwoDecimals(Mathf.Max(3f, current.minRange - 0.2f)), //Capped to 3f
                maxRange = RoundToTwoDecimals(current.maxRange + 0.2f),
                maxBullets = Mathf.CeilToInt(current.maxBullets + 50f),
                fireRate = RoundToTwoDecimals(current.fireRate + 0.5f),
                spreadScale = RoundToTwoDecimals(Mathf.Max(2f, current.spreadScale - 1f)), //Capped to 2f
                reloadTime = RoundToTwoDecimals(Mathf.Max(1f, current.reloadTime - 0.1f)), //Capped to 1f
                projectileDamage = Mathf.CeilToInt(current.projectileDamage + 20f)
            };

            return upgraded;
        }

        private static float RoundToTwoDecimals(float value)
        {
            return Mathf.Round(value * 100f) / 100f;
        }
    }

    // Helper struct to hold turret attributes cleanly
    public struct TurretAttributes
    {
        public int level;
        public float minRange;
        public float maxRange;
        public int maxBullets;
        public float fireRate;
        public float spreadScale;
        public float reloadTime;
        public int projectileDamage;
    }
}
