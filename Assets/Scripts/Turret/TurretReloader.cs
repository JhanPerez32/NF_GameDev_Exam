using NF.TD.BaseTurret;
using NF.TD.Interfaces;
using NF.TD.Turret;
using NF.TD.TurretCore;
using UnityEngine;

namespace MF.TD.Gun.Modules 
{
    public class TurretReloader : MonoBehaviour, IReloading
    {
        public int CurrentBullets { get; private set; }
        public bool IsReloading { get; private set; }

        public bool CanShoot => !IsReloading && CurrentBullets > 0;

        private TurretScriptable turretData;
        private TurretUnit turretUnit;

        private void Awake()
        {
            // Auto-get the TurretUnit on the same GameObject
            turretUnit = GetComponent<TurretUnit>();
            if (turretUnit == null)
            {
                Debug.LogError("[TurretReloader] Missing TurretUnit on the same GameObject.");
                enabled = false;
                return;
            }

            turretData = turretUnit.turretData;

            if (turretData == null)
            {
                Debug.LogError("[TurretReloader] TurretData not assigned in TurretUnit.");
                enabled = false;
                return;
            }

            CurrentBullets = turretData.maxBullets;
        }

        public void UseBullet()
        {
            if (CurrentBullets > 0)
            {
                CurrentBullets--;
                if (CurrentBullets == 0)
                    StartReload();
            }
        }

        public void StartReload()
        {
            if (!IsReloading)
                StartCoroutine(Reload());
        }

        private System.Collections.IEnumerator Reload()
        {
            IsReloading = true;
            yield return new WaitForSeconds(turretData.reloadTime);
            CurrentBullets = turretData.maxBullets;
            IsReloading = false;
        }
    }
}
