using NF.TD.BaseEnemy;
using NF.TD.Interfaces;
using NF.TD.PlayerCore;
using NF.TD.UICore;
using System.Collections;
using TMPro;
using UnityEngine;

namespace NF.TD.SpawnEnemy
{
    public class EnemySpawn : MonoBehaviour
    {
        public static int CurrentWave { get; private set; } = 0;

        public EnemyScriptable enemyData;

        public Transform spawnPoint;

        public float timeBetweenWaves;
        private float countdown = 5f;

        private int waveIndex = 0;
        private ISpawner spawner;

        private bool isCountingDown = false;
        private bool waveInProgress = false;

        private void Awake()
        {
            spawner = new DefaultSpawner(); // Could be injected later
        }

        private void Update()
        {
            if (!waveInProgress && AreEnemiesAlive() == false && !isCountingDown)
            {
                // Start countdown after previous wave cleared
                isCountingDown = true;
                countdown = timeBetweenWaves;
            }

            if (isCountingDown)
            {
                countdown -= Time.deltaTime;
                countdown = Mathf.Max(0f, countdown);

                UIManager.Instance.UpdateWaveCountdown(countdown);

                if (countdown <= 0f)
                {
                    StartCoroutine(SpawnWave());
                    isCountingDown = false;
                    waveInProgress = true;
                }
            }
            else if (waveInProgress)
            {
                // During wave: Freeze countdown at 0
                UIManager.Instance.UpdateWaveCountdown(0f);

                if (!AreEnemiesAlive())
                {
                    waveInProgress = false; // Ready for next wave countdown
                }
            }
        }

        IEnumerator SpawnWave()
        {
            waveIndex++;
            CurrentWave = waveIndex;

            UIManager.Instance.UpdateWaveNumberUI(waveIndex);

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }

            PlayerStats.Rounds = waveIndex;
        }

        void SpawnEnemy()
        {
            spawner.Spawn(enemyData, spawnPoint);
        }

        bool AreEnemiesAlive()
        {
            return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
        }
    }
}
