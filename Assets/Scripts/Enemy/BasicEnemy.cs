using NF.Main.Core.PlayerStateMachine;
using NF.TD.Enemy.Core;
using NF.TD.SpawnEnemy;
using UnityEngine;

namespace NF.TD.Enemy.BasicEnemy 
{
    public class BasicEnemy : EnemyUnit
    {
        /*If we want to add a mechanic in which the 
         * Enemy can attack the Player Turret, then
         * we can mostly put it here */

        int increasedHealthPerWave;

        protected override void Start()
        {
            base.Start();

            increasedHealthPerWave = enemyScriptable.increasedHealthPerWave;

            // Apply health bonus only to BasicEnemy
            int bonusHealth = increasedHealthPerWave * (EnemySpawn.CurrentWave - 1); // Wave 1 = +0, Wave 2 = +20, etc.
            health += bonusHealth;

            if (healthBar != null)
            {
                healthBar.SetMaxHealth(health);
                healthBar.SetHealth(health);
            }
        }
    }
}
