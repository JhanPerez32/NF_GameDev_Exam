using NF.TD.BaseTurret;
using NF.TD.Bullet;
using NF.TD.Turret;
using NF.TD.TurretCore;
using NF.TD.Interfaces;
using UnityEngine;

namespace NF.TD.Gun
{
    public class TurretGun : MonoBehaviour, IShooting, ITargeting
    {
        [HideInInspector]
        public Transform target;
        [HideInInspector]
        public float maxSpreadDistance; // Will be taking the value of maxRange in Turret.cs
        [HideInInspector]
        public float fireRate;
        
        public float spreadScale; //Will take in the Value on TurretScriptable for the Default value

        public TurretTower turret;

        [Space(5)]
        [Header("Bullet Settings")]
        public GameObject shotPrefab;
        //Multiple Spawn Bullet Points, can be also be a single gun
        public Transform[] bulletSpawnPoint;

        [Header("Muzzle Flash")]
        public GameObject[] muzzleFlash;

        private TurretScriptable turretData;
        private float lastSpreadScale;
        private float lastFireRate;

        bool firing;
        float fireTimer;
        int gunPointIndex;

        void Awake()
        {
            turretData = turret.turretData;
        }

        void Update()
        {
            //Continuously updates and for the Upgrade
            if (turretData.spreadScale != lastSpreadScale)
            {
                spreadScale = turretData.spreadScale;
                lastSpreadScale = spreadScale;
            }

            if (turretData.fireRate != lastFireRate)
            {
                fireRate = turretData.fireRate;
                lastFireRate = fireRate;
            }

            if (firing)
            {
                while (fireTimer >= 1 / fireRate)
                {
                    SpawnShot();
                    fireTimer -= 1 / fireRate; // Reduce the timer by the fire interval
                }

                fireTimer += Time.deltaTime;
                firing = false;
            }
            else
            {
                if (fireTimer < 1 / fireRate)
                {
                    fireTimer += Time.deltaTime;
                }
                else
                {
                    fireTimer = 1 / fireRate;
                }
            }
        }

        // Instantiate a projectile at the current bullet spawn point
        void SpawnShot()
        {
            var gunPoint = bulletSpawnPoint[gunPointIndex++];
            Vector3 forward = gunPoint.forward;

            float range = maxSpreadDistance;
            float spreadRadius = Mathf.Tan(spreadScale * Mathf.Deg2Rad) * range;
            Vector2 randomOffset = Random.insideUnitCircle * spreadRadius;
            Vector3 spreadPoint = gunPoint.position + forward * range + gunPoint.up * randomOffset.y + gunPoint.right * randomOffset.x;

            Vector3 direction = (spreadPoint - gunPoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject shotGo = Instantiate(shotPrefab, gunPoint.position, rotation);

            if (shotGo.TryGetComponent(out Projectile shot))
            {
                shot.Initialize(turretData.projectileSpeed, turretData.projectileDamage);
            }

            // Randomized muzzle flash
            if (muzzleFlash != null && muzzleFlash.Length > 0)
            {
                int flashIndex = Random.Range(0, muzzleFlash.Length);
                GameObject flash = Instantiate(muzzleFlash[flashIndex], gunPoint.position, gunPoint.rotation);

                Destroy(flash, 2f);
            }

            gunPointIndex %= bulletSpawnPoint.Length;
        }

        // Public method to trigger firing, can be called externally
        public void Fire()
        {
            if (target == null) return;

            foreach (var gunPoint in bulletSpawnPoint)
            {
                Vector3 dirToTarget = (target.position - gunPoint.position).normalized;
                gunPoint.rotation = Quaternion.LookRotation(dirToTarget);
            }

            firing = true;
        }

        /*This is to check if the Enemy is also inside the
         firing spread of the turret, it is like the
         spray and pray mechanic*/
        public bool IsTargetWithinSpread()
        {
            if (target == null) return false;

            foreach (Transform gunPoint in bulletSpawnPoint)
            {
                Vector3 directionToTarget = (target.position - gunPoint.position).normalized;
                float angle = Vector3.Angle(gunPoint.forward, directionToTarget);

                // Ensure it's within the spread angle
                if (angle <= spreadScale)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }

}

