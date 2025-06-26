using NF.TD.Interfaces;
using NF.TD.BaseEnemy;
using NF.TD.EnemyPath;
using NF.TD.Enemy.UI;
using NF.TD.Extensions;
using UnityEngine;

namespace NF.TD.Enemy.Core
{
    /// <summary>
    /// Universal behavior for any enemy
    /// </summary>
    public class EnemyUnit : MonoBehaviour, IDamageable
    {
        public EnemyScriptable enemyScriptable;

        protected int health;
        protected EnemyHealthBar healthBar;
        private int rewardValue;
        private int DMGToBase;
        private Transform target;
        private int wavepointIndex = 0;

        protected virtual void Start()
        {
            //Will start at the very first Waypoint gameobject
            target = Waypoints.points[0];

            health = enemyScriptable.health;
            rewardValue = enemyScriptable.rewardValue;
            DMGToBase = enemyScriptable.DMGToBase;

            healthBar = GetComponentInChildren<EnemyHealthBar>();
            if (healthBar != null)
            {
                healthBar.followTarget = transform;
                healthBar.SetMaxHealth(health);
            }
        }

        //Moves the enemy toward the current target waypoint.
        //Once the enemy is within 0.4 units of the target, it proceeds to the next waypoint.
        private void Update()
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyScriptable.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
        }

        //The Arrangements of the Waypoints or Pathway of the enemy is in-order
        void GetNextWaypoint()
        {
            if (wavepointIndex >= Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }

            //How many the waypoints is until it reaches the last one
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }

        void EndPath()
        {
            PlayerStatsExtension.UpdatePlayerLives(DMGToBase);
            Destroy(gameObject);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;

            if (healthBar != null)
            {
                healthBar.SetHealth(health);
            }

            if (enemyScriptable.hitFX != null)
            {
                GameObject fx = Instantiate(enemyScriptable.hitFX, transform.position, Quaternion.identity);
                Destroy(fx, 2f);
            }

            if (health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            PlayerStatsExtension.AddMoney(rewardValue);
            Destroy(gameObject);
        }
    }
}

