using UnityEngine;

namespace NF.TD.BaseTurret
{
    [CreateAssetMenu(fileName = "New Turret", menuName = "TurretScriptable/BaseTurret")]
    public class BaseTurret : ScriptableObject
    {
        [Header("Turret")]

        [Tooltip("Base Model of the Turret")]
        public GameObject turretModel;

        [Tooltip("Turret Icon for the Shop")]
        public Sprite turretIcon;

        [Tooltip("Purchase Cost")]
        public int turretCost;

        [Header("Turret Range")]
        public float minRange = 15f;
        public float maxRange = 30f;

        [Header("Gun Settings")]
        public int maxBullets = 10;
        public float fireRate = 20f;
        public float reloadTime = 2f;

        [Header("Projectile Settings")]
        public float projectileSpeed = 20f;
        public int projectileDamage = 10;
        public float explosionRadius = 5;

        //Dynamic Naming for the Scriptable Game Object.
        //It will take in the Name of the Scriptable GameObject it self.
        public string TurretName => name; //For Turret Shops, this line will be responsible for naming.
    }
}