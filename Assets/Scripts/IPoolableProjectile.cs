using UnityEngine;

public interface IPoolableProjectile
{
    public void LaunchProjectile(Transform launchingPoint);
    public bool IsAvailable();
    public ProjectileType GetProjectileType();
}