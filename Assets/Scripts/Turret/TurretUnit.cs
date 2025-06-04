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

        [Tooltip("Attach TurretGun")]
        public TurretGun gun;

        [Tooltip("Rotating part of the Turret " +
            "(Eg. Connector on Body into Head" +
            "and Body to Gun)")]
        public TurretJoints[] turretJoints;

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
    }
}

