using NF.Main.Core.PlayerStateMachine;
using NF.TD.BaseEnemy;
using NF.TD.Interfaces;
using NF.TD.UICore;
using System.Collections;
using TMPro;
using UnityEngine;

namespace NF.TD.SpawnEnemy
{
    public class EnemySpawn : MonoBehaviour
    {
        public EnemyScriptable enemyData;

        public Transform spawnPoint;

        public float timeBetweenWaves;
        private float countdown = 5f;

        private int waveIndex = 0;
        private ISpawner spawner;

        private void Awake()
        {
            spawner = new DefaultSpawner(); // Could be injected later
        }

        private void Update()
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Max(0f, countdown);

            UIManager.Instance?.UpdateWaveCountdown(countdown);
        }

        IEnumerator SpawnWave()
        {
            waveIndex++;

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        }

        void SpawnEnemy()
        {
            spawner.Spawn(enemyData, spawnPoint);
        }
    }
}
