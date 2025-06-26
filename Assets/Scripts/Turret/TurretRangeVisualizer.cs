using NF.TD.Gun;
using NF.TD.Turret;
using UnityEditor;
using UnityEngine;

namespace NF.TD.TurretVisualRange
{
    [ExecuteAlways]
    public class TurretRangeVisualizer : MonoBehaviour
    {
        public TurretTower turret;

        public GameObject rangeParent;
        public GameObject minRange;
        public GameObject maxRange;

        private float lastMin, lastMax;

        void Update()
        {
            if (turret == null || turret.turretData == null) return;

            float min = turret.turretData.minRange;
            float max = turret.turretData.maxRange;

            if (Mathf.Abs(lastMin - min) > 0.01f || Mathf.Abs(lastMax - max) > 0.01f)
            {
                UpdateRangeVisuals();
                lastMin = min;
                lastMax = max;
            }
        }

        void UpdateRangeVisuals()
        {
            if (turret == null || turret.turretData == null) return;

            if (minRange != null)
            {
                float diameter = turret.turretData.minRange;
                minRange.transform.localScale = new Vector3(diameter, minRange.transform.localScale.y, diameter);
                minRange.transform.position = turret.transform.position;
            }

            if (maxRange != null)
            {
                float diameter = turret.turretData.maxRange;
                maxRange.transform.localScale = new Vector3(diameter, maxRange.transform.localScale.y, diameter);
                maxRange.transform.position = turret.transform.position;
            }
        }

/*#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (turret == null)
                turret = GetComponent<TurretTower>();

            if (turret == null || turret.turretData == null || turret.turretJoints == null)
                return;

            var dashLineSize = 2f;

            foreach (var mountPoint in turret.turretJoints)
            {
                if (mountPoint == null) continue;

                var hardpoint = mountPoint.transform;
                var from = Quaternion.AngleAxis(-mountPoint.angleLimit / 2, hardpoint.up) * hardpoint.forward;

                // Draw angle-limited cone sector between minRange and maxRange
                if (turret.turretData.maxRange > turret.turretData.minRange)
                {
                    int segments = 60;
                    float step = mountPoint.angleLimit / segments;
                    Handles.color = new Color(0f, 1f, 0f, 0.2f);

                    for (int i = 0; i < segments; i++)
                    {
                        float angleStart = -mountPoint.angleLimit / 2 + step * i;
                        float angleEnd = angleStart + step;

                        Vector3 dir1 = Quaternion.AngleAxis(angleStart, hardpoint.up) * hardpoint.forward;
                        Vector3 dir2 = Quaternion.AngleAxis(angleEnd, hardpoint.up) * hardpoint.forward;

                        Handles.DrawAAConvexPolygon(
                            hardpoint.position + dir1 * turret.turretData.minRange,
                            hardpoint.position + dir1 * turret.turretData.maxRange,
                            hardpoint.position + dir2 * turret.turretData.maxRange,
                            hardpoint.position + dir2 * turret.turretData.minRange
                        );
                    }
                }

                Handles.color = Color.red;
                Handles.DrawWireArc(hardpoint.position, hardpoint.up, from, mountPoint.angleLimit, turret.turretData.minRange);
                Handles.color = Color.green;
                Handles.DrawWireArc(hardpoint.position, hardpoint.up, from, mountPoint.angleLimit, turret.turretData.maxRange);

                // Target direction feedback
                if (turret.target)
                {
                    Vector3 projection = Vector3.ProjectOnPlane(turret.target.position - hardpoint.position, hardpoint.up);
                    Handles.color = Color.white;
                    Handles.DrawDottedLine(turret.target.position, hardpoint.position + projection, dashLineSize);

                    if (Vector3.Angle(hardpoint.forward, projection) <= mountPoint.angleLimit / 2)
                    {
                        Handles.color = Color.red;
                        Handles.DrawLine(hardpoint.position, hardpoint.position + projection);
                        Handles.DrawSolidDisc(hardpoint.position + projection, hardpoint.up, 0.5f);
                    }
                }
            }

            TurretGun gun = turret.GetComponentInChildren<TurretGun>();
            if (gun != null && gun.bulletSpawnPoint != null)
            {
                Handles.color = Color.yellow;

                foreach (Transform gunPoint in gun.bulletSpawnPoint)
                {
                    if (gunPoint == null) continue;

                    Vector3 forward = gunPoint.forward;
                    float range = gun.maxSpreadDistance;
                    float angle = gun.spreadScale;

                    // Draw center line
                    Handles.DrawLine(gunPoint.position, gunPoint.position + forward * range);

                    // Draw cone boundary lines (up, down, left, right)
                    Vector3 upOffset = Quaternion.Euler(angle, 0, 0) * forward;
                    Vector3 downOffset = Quaternion.Euler(-angle, 0, 0) * forward;
                    Vector3 leftOffset = Quaternion.Euler(0, -angle, 0) * forward;
                    Vector3 rightOffset = Quaternion.Euler(0, angle, 0) * forward;

                    Handles.DrawDottedLine(gunPoint.position, gunPoint.position + upOffset * range, 2f);
                    Handles.DrawDottedLine(gunPoint.position, gunPoint.position + downOffset * range, 2f);
                    Handles.DrawDottedLine(gunPoint.position, gunPoint.position + leftOffset * range, 2f);
                    Handles.DrawDottedLine(gunPoint.position, gunPoint.position + rightOffset * range, 2f);

                    // Draw arc disc at the spread limit
                    float spreadRadius = Mathf.Tan(angle * Mathf.Deg2Rad) * range;
                    Handles.DrawWireDisc(gunPoint.position + forward * range, forward, spreadRadius);
                }
            }
        }
#endif*/

        public void SetVisualizerVisible(bool visible)
        {
            if (rangeParent != null)
            {
                rangeParent.SetActive(visible);
            }
        }
    }

}

