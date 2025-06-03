using UnityEngine;

namespace NF.TD.Gun
{
    public class TurretGun : MonoBehaviour
    {
        public GameObject shotPrefab;

        //Multiple Spawn Bullet Points, can be also be a single gun
        public Transform[] bulletSpawnPoint;
        public float fireRate;

        bool firing;
        float fireTimer;

        int gunPointIndex;

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
            Instantiate(shotPrefab, gunPoint.position, gunPoint.rotation);

            // Loop back to the first spawn point if at the end
            gunPointIndex %= bulletSpawnPoint.Length;
        }

        // Public method to trigger firing, can be called externally
        public void Fire()
        {
            firing = true;
        }

        /*TODO:
        - Implement Max Bullet and Reload Time*/
    }

}

