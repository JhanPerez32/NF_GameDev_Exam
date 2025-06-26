using UnityEngine;
using UnityEngine.UI;

namespace NF.TD.Enemy.UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Slider healthSlider;
        public Transform followTarget;

        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main; // Cache for performance
        }

        private void LateUpdate()
        {
            if (followTarget == null || mainCam == null)
                return;

            // Make the health bar face the camera
            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        }

        public void SetMaxHealth(int max)
        {
            healthSlider.maxValue = max;
            healthSlider.value = max;
        }

        public void SetHealth(int current)
        {
            healthSlider.value = current;
        }
    }
}
