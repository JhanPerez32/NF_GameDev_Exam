using NF.TD.BaseEnemy;
using UnityEngine;

namespace NF.TD.Interfaces
{
    public interface ISpawner
    {
        void Spawn(EnemyScriptable enemyData, Transform spawnPoint);
    }
}
