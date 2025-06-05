using NF.Main.Core.PlayerStateMachine;
using NF.TD.Enemy.Core;
using UnityEngine;

namespace NF.TD.Enemy.BasicEnemy 
{
    public class BasicEnemy : EnemyUnit
    {
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
            Destroy(gameObject);
        }
    }
}
