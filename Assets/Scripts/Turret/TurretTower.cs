using UnityEngine;
using NF.TD.TurretCore;
using NF.TD.Interfaces;

namespace NF.TD.Turret
{
    public class TurretTower : TurretUnit
    {
        IReloading reloader;

        void Awake()
        {
            reloader = GetComponent<IReloading>();
        }

        void Update()
        {
            // Sync turret joint ranges
            foreach (var mount in turretJoints)
            {
                if (mount != null)
                {
                    mount.minRange = turretData.minRange;
                    mount.maxRange = turretData.maxRange;
                }
            }

            // Sync gun spread range
            if (gun != null && gun.maxSpreadDistance != turretData.maxRange)
            {
                gun.maxSpreadDistance = turretData.maxRange;
            }

            // Remove target if out of range
            if (target != null)
            {
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist < turretData.minRange || dist > turretData.maxRange)
                {
                    target = null;
                    gun.target = null;
                    return;
                }
            }

            // Don't aim/fire if reloading or no target
            if (!target || reloader.IsReloading) return;

            // Aim target
            bool aimed = true;
            foreach (var mount in turretJoints)
            {
                if (!mount.Aim(target.position)) aimed = false;
            }

            gun.target = target;

            // Fire if aimed and within spread
            if (aimed && gun.IsTargetWithinSpread() && reloader.CanShoot)
            {
                gun.Fire();
                reloader.UseBullet();
            }

            // Cache update
            if (turretData.minRange != lastMinRange || turretData.maxRange != lastMaxRange)
            {
                lastMinRange = turretData.minRange;
                lastMaxRange = turretData.maxRange;
            }
        }

    }

}

