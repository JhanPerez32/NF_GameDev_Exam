using UnityEditor;
using UnityEngine;
using NF.TD.Gun;
using NF.TD.Joints;

namespace NF.TD.Turret
{
    public class TurretTower : MonoBehaviour
    {
        public TurretGun turretGun;
        public TurretJoints[] turretJoints;
        public Transform target;

        void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (!target) return;

            var dashLineSize = 2f;

            foreach (var mountPoint in turretJoints)
            {
                var hardpoint = mountPoint.transform;
                var from = Quaternion.AngleAxis(-mountPoint.angleLimit / 2, hardpoint.up) * hardpoint.forward;
                var projection = Vector3.ProjectOnPlane(target.position - hardpoint.position, hardpoint.up);

                // projection line
                Handles.color = Color.white;
                Handles.DrawDottedLine(target.position, hardpoint.position + projection, dashLineSize);

                // do not draw target indicator when out of angle
                if (Vector3.Angle(hardpoint.forward, projection) > mountPoint.angleLimit / 2) return;

                // target line
                Handles.color = Color.red;
                Handles.DrawLine(hardpoint.position, hardpoint.position + projection);

                // range line
                Handles.color = Color.green;
                Handles.DrawWireArc(hardpoint.position, hardpoint.up, from, mountPoint.angleLimit, projection.magnitude);
                Handles.DrawSolidDisc(hardpoint.position + projection, hardpoint.up, .5f);
#endif
            }
        }

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
                turretGun.Fire();
            }
        }

        /*TODO:
        - Implement Min and Max Range
        - Auto Aim on the Enemy*/
        
            
    }

}

