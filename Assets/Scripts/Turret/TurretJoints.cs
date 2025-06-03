using UnityEngine;

namespace NF.TD.Joints
{
    public class TurretJoints : MonoBehaviour
    {
        //NOTE: Don't delete this 2
        //These are kept for compatibility or future reference (e.g., range checking), but hidden from the inspector
        [HideInInspector] public float minRange = 0f;
        [HideInInspector] public float maxRange = 0f;

        // How far the turret can rotate left and right (e.g., 90° means 45° left/right)
        [Range(0, 360f)]
        public float angleLimit = 90f;

        // Allowed rotation error when aiming — smaller = more accurate
        [Range(0, 360f)]
        public float aimTolerance = 1f;

        // Speed at which the turret rotates toward the target (degrees per second)
        public float turnSpeed = 90f;

        Transform turret;

        void Awake()
        {
            // Get the first child transform (usually the rotating turret part)
            turret = transform.GetChild(0);
        }

        /// <summary>
        /// Attempts to aim the turret at the target point.
        /// Returns true if aiming is successful (within tolerance), otherwise false.
        /// </summary>
        public bool Aim(Vector3 targetPoint)
        {
            return Aim(targetPoint, out _);
        }

        /// <summary>
        /// Aims the turret at a given target position.
        /// Returns true if turret is aligned (within aimTolerance).
        /// Outputs whether the turret tried to aim outside its angle limit.
        /// </summary>
        public bool Aim(Vector3 targetPoint, out bool reachAngleLimit)
        {
            reachAngleLimit = default;
            var hardpoint = transform;
            var los = targetPoint - hardpoint.position;
            var halfAngle = angleLimit / 2;
            var losOnPlane = Vector3.ProjectOnPlane(los, hardpoint.up);
            var deltaAngle = Vector3.SignedAngle(hardpoint.forward, losOnPlane, hardpoint.up);

            // If the angle to the target exceeds the allowed range
            if (Mathf.Abs(deltaAngle) > halfAngle)
            {
                reachAngleLimit = true;

                // Clamp the angle so turret doesn't rotate past the allowed limits
                losOnPlane = hardpoint.rotation * Quaternion.Euler(0, Mathf.Clamp(deltaAngle, -halfAngle, halfAngle), 0) * Vector3.forward;
            }

            // Calculate desired rotation to aim at the (possibly clamped) direction
            var targetRotation = Quaternion.LookRotation(losOnPlane, hardpoint.up);

            // Check if turret is already aimed (within tolerance)
            var aimed = !reachAngleLimit && Quaternion.Angle(turret.rotation, targetRotation) < aimTolerance;
            turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRotation, turnSpeed * Time.deltaTime);

            return aimed;
        }
    }
}

