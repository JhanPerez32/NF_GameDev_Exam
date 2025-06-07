using NF.TD.Interfaces;
using NF.TD.BaseEnemy;
using NF.TD.EnemyPath;
using UnityEngine;
using NF.TD.Extensions;

namespace NF.TD.Enemy.Core
{
    /// <summary>
    /// Universal behavior for any enemy
    /// </summary>
    public class EnemyUnit : MonoBehaviour, IDamageable
    {
        public EnemyScriptable enemyScriptable;

        [Tooltip("Keep it 0 since it will take in the Value on the" +
            "its Scriptable GameObject, kindly edit there")]
        private int health;
        private int rewardValue;
        private int DMGToBase;
        private Transform target;
        private int wavepointIndex = 0;

        private void Start()
        {
            //Will start at the very first Waypoint gameobject
            target = Waypoints.points[0];

            health = enemyScriptable.health;
            rewardValue = enemyScriptable.rewardValue;
            DMGToBase = enemyScriptable.DMGToBase;
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

        //TODO: Add here a Loss Lives
        void EndPath()
        {
            PlayerStatsExtension.UpdatePlayerLives(-DMGToBase);
            Destroy(gameObject);
        }

        public void TakeDamage(int amount)
        {
            health -= amount;

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

