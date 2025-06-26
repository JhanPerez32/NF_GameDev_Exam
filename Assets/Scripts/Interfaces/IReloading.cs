namespace NF.TD.Interfaces
{
    /// <summary>
    /// Interface for any turret or weapon that can reload.
    /// </summary>
    public interface IReloading
    {
        bool IsReloading { get; }
        bool CanShoot { get; }
        void UseBullet();
        void StartReload();
    }
}