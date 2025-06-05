using NF.TD.BaseEnemy;
using NF.TD.Interfaces;
using UnityEngine;

namespace NF.TD.SpawnEnemy
{
    public class DefaultSpawner : ISpawner
    {
        public void Spawn(EnemyScriptable enemyData, Transform spawnPoint)
        {
            if (enemyData != null && enemyData.enemyPrefab != null)
            {
                Object.Instantiate(enemyData.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
