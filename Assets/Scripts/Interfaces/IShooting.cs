namespace NF.TD.TurretCore
{
    public interface IShooting
    {
        void Fire();
        bool IsTargetWithinSpread();
    }
}
