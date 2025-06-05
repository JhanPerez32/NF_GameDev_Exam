using NF.Main.Core.PlayerStateMachine;
using NF.TD.BaseEnemy;
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

        public TextMeshProUGUI waveCountdownText;

        private int waveIndex = 0;

        private void Update()
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Max(0f, countdown);

            waveCountdownText.text = countdown.ToString("00.00");
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
            Instantiate(enemyData.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
