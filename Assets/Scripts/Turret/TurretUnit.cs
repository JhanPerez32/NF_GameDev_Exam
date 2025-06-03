using NF.TD.BaseTurret;
using NF.TD.Gun;
using NF.TD.Joints;
using UnityEngine;

namespace NF.TD.TurretCore 
{
    /// <summary>
    /// This Script Handles the Universal Behaviour of the Turret
    /// </summary>
    public class TurretUnit : MonoBehaviour
    {
        [HideInInspector]
        public Transform target;

        public TurretScriptable turretData;

        [Tooltip("Attach TurretGun")]
        public TurretGun gun;

        [Tooltip("Rotating part of the Turret " +
            "(Eg. Connector on Body into Head" +
            "and Body to Gun)")]
        public TurretJoints[] turretJoints;

        [Header("Target Tag")]
        public string enemyTag = "Enemy";

        private int currentBullets;
        private bool reloading = false;

        private float lastMinRange, lastMaxRange;

        void OnValidate()
        {
            if (turretData == null) return;

            foreach (var mountPoint in turretJoints)
            {
                if (mountPoint != null)
                {
                    mountPoint.minRange = turretData.minRange;
                    mountPoint.maxRange = turretData.maxRange;
                }
            }

            // Auto-update gun's spread distance
            if (gun != null)
            {
                gun.maxSpreadDistance = turretData.maxRange;
            }
        }

    }
}

