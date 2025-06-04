using NF.TD.BaseTurret;
using NF.TD.Bullet;
using NF.TD.Turret;
using NF.TD.TurretCore;
using UnityEngine;

namespace NF.TD.Gun
{
    public class TurretGun : MonoBehaviour, IShooting, ITargeting
    {
        [HideInInspector]
        public Transform target;
        [HideInInspector]
        public float maxSpreadDistance; // Will be taking the value of maxRange in Turret.cs

        public TurretTower turret;

        [Space(5)]
        [Header("Bullet Settings")]
        public GameObject shotPrefab;
        //Multiple Spawn Bullet Points, can be also be a single gun
        public Transform[] bulletSpawnPoint;
        public float fireRate;

        [Space(5)]
        [Header("Spread")]
        [Range(1f, 90f)]
        public float maxSpreadAngle = 5f; // Maximum angle in degrees

        private TurretScriptable turretData;

        bool firing;
        float fireTimer;
        int gunPointIndex;

        void Awake()
        {
            turretData = turret.turretData;
        }

        void Update()
        {
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
            float spreadRadius = Mathf.Tan(maxSpreadAngle * Mathf.Deg2Rad) * range;
            Vector2 randomOffset = Random.insideUnitCircle * spreadRadius;
            Vector3 spreadPoint = gunPoint.position + forward * range + gunPoint.up * randomOffset.y + gunPoint.right * randomOffset.x;

            Vector3 direction = (spreadPoint - gunPoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject shotGo = Instantiate(shotPrefab, gunPoint.position, rotation);

            if (shotGo.TryGetComponent(out Projectile shot))
            {
                shot.Initialize(turretData.projectileSpeed, turretData.projectileDamage);
            }

            gunPointIndex %= bulletSpawnPoint.Length;
        }

        // Public method to trigger firing, can be called externally
        public void Fire()
        {
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
                if (angle <= maxSpreadAngle)
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

        /*TODO:
        - Implement Max Bullet and Reload Time*/
    }

}

