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
        [HideInInspector]
        public float lastMinRange, lastMaxRange;

        public TurretScriptable turretData;
        public int totalSpentCost = 0;

        [Tooltip("Attach TurretGun")]
        public TurretGun gun;

        [Tooltip("Rotating part of the Turret " +
            "(Eg. Connector on Body into Head" +
            "and Body to Gun)")]
        public TurretJoints[] turretJoints;
        [Tooltip("Allowed rotation error when aiming — smaller = more accurate")]
        [Range(0f, 360f)]
        public float aimTolerance;

        [Header("Target Tag")]
        public string enemyTag = "Enemy";

        public int currentBullets;
        public bool reloading = false;

        void OnValidate()
        {
            if (turretData == null) return;

            foreach (var mountPoint in turretJoints)
            {
                if (mountPoint != null)
                {
                    mountPoint.minRange = turretData.minRange;
                    mountPoint.maxRange = turretData.maxRange;
                    mountPoint.aimTolerance = aimTolerance;
                }
            }

            // Auto-update gun's spread distance
            if (gun != null)
            {
                gun.maxSpreadDistance = turretData.maxRange;
            }
        }

        private void Start()
        {
            if (turretData != null)
            {
                totalSpentCost = turretData.turretCost;
            }

            currentBullets = turretData.maxBullets;
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            float closestValidDistance = Mathf.Infinity;
            GameObject selectedEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance >= turretData.minRange && distance <= turretData.maxRange)
                {
                    if (distance < closestValidDistance)
                    {
                        closestValidDistance = distance;
                        selectedEnemy = enemy;
                    }
                }
            }

            if (selectedEnemy != null)
            {
                target = selectedEnemy.transform;
            }
            else
            {
                target = null;
            }
        }

        /// <summary>
        /// Calculates the turret's selling price based on total spent cost.
        ///
        /// Formula:
        /// Sell Price = TotalSpentCost * 0.5
        ///
        /// TotalSpentCost includes:
        /// - The initial cost to build the turret
        /// - All costs accumulated from upgrades
        ///
        /// Example:
        /// If base cost = 250
        /// Upgrade 1 cost = 125 (250 * 0.5 * level 1)
        /// Upgrade 2 cost = 250 (250 * 0.5 * level 2)
        /// TotalSpentCost = 250 + 125 + 250 = 625
        ///
        /// Sell Price = 625 * 0.5 = 312 (rounded)
        ///
        /// This ensures players are partially refunded for both building and upgrading,
        /// encouraging smart investment and upgrades.
        /// </summary>
        public int GetSellPrice()
        {
            return Mathf.RoundToInt(totalSpentCost * 0.5f);
        }

    }
}

