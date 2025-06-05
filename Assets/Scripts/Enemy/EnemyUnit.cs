using NF.Main.Core.PlayerStateMachine;
using NF.TD.BaseEnemy;
using NF.TD.EnemyPath;
using UnityEngine;

namespace NF.TD.Enemy.Core
{
    /// <summary>
    /// Universal behavior for any enemy
    /// </summary>
    public class EnemyUnit : MonoBehaviour
    {
        public EnemyScriptable enemyScriptable;

        private Transform target;
        private int wavepointIndex = 0;

        private void Start()
        {
            target = Waypoints.points[0];
        }

        private void Update()
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemyScriptable.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
        }

        void GetNextWaypoint()
        {
            if (wavepointIndex >= Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }

        void EndPath()
        {
            Destroy(gameObject);
        }
    }
}

