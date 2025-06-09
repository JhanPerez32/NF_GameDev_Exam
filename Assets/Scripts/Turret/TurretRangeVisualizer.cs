using NF.TD.Turret;
using UnityEngine;

namespace NF.TD.TurretVisualRange
{
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

        public void SetVisualizerVisible(bool visible)
        {
            if (rangeParent != null)
            {
                rangeParent.SetActive(visible);
            }
        }
    }

}

