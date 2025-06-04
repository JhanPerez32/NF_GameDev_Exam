using UnityEditor;
using UnityEngine;
using NF.TD.Gun;
using NF.TD.Joints;
using NF.TD.TurretCore;

namespace NF.TD.Turret
{
    public class TurretTower : TurretUnit
    {
        IAiming[] aimModules;
        IShooting shooter;

        void Awake()
        {
            aimModules = GetComponentsInChildren<IAiming>();
            shooter = gun.GetComponent<IShooting>();
        }

        void Update()
        {
            // do nothing when no target
            if (!target) return;

            // aim target
            var allAimed = true;
            foreach (var mountPoint in turretJoints)
            {
                if (!mountPoint.Aim(target.position))
                {
                    allAimed = false;
                }
            }

            // shoot when aimed
            if (allAimed)
            {
                gun.Fire();
            }
        }




        /*TODO:
        - Implement Min and Max Range
        - Done: Auto Aim on the Enemy*/
        
            
    }

}

