using UnityEditor;
using UnityEngine;
using NF.TD.Gun;
using NF.TD.Joints;
using NF.TD.TurretCore;

namespace NF.TD.Turret
{
    public class TurretTower : TurretUnit
    {
        void Update()
        {
            // do nothing when no target
            if (!target) return;

            // aim target
            var aimed = true;
            foreach (var mountPoint in turretJoints)
            {
                if (!mountPoint.Aim(target.position))
                {
                    aimed = false;
                }
            }

            // shoot when aimed
            if (aimed)
            {
                gun.Fire();
            }
        }

        /*TODO:
        - Implement Min and Max Range
        - Auto Aim on the Enemy*/
        
            
    }

}

