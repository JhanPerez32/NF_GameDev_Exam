using UnityEngine;

namespace NF.TD.BaseEnemy 
{
    /// <summary>
    /// General Setting for the Enemy, for easy accesss expansions
    /// </summary>

    [CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyScriptable/BaseEnemy")]
    public class EnemyScriptable : ScriptableObject
    {
        [Header("Enemy Model")]
        public GameObject enemyPrefab;

        [Header("Hit/Destroy FX")]
        public GameObject hitFX;

        [Header("Movement Speed")]
        public float speed = 10f;

        [Header("Max Health")]
        public int health = 100;

        [Header("Reward")]
        public int rewardValue = 25;

        [Header("Damage to Base")]
        public int DMGToBase = 1;


    }
}
