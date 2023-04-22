using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : Singleton<ProjectilePool>
{
    [SerializeField] private Projectile _simpleProjectilePrefab;
    [SerializeField] private GrenadeProjectile _grenadeProjectilePrefab;
    [SerializeField] private TennisBallProjectile _tennisBallProjectilePrefab;
    private Dictionary<ProjectileType, IPoolableProjectile> _prefabs;

    private List<IPoolableProjectile> _projectiles;


    private void Start()
    {
        _prefabs = new Dictionary<ProjectileType, IPoolableProjectile>()
        {
            {ProjectileType.Simple, _simpleProjectilePrefab},
            {ProjectileType.Grenade, _grenadeProjectilePrefab},
            {ProjectileType.TennisBall, _tennisBallProjectilePrefab}
        };
        InitializePool();
    }

    public void LaunchProjectile(ProjectileType type, Transform startPoint)
    {
        IPoolableProjectile projectile;
        switch (type)
        {
            case ProjectileType.Simple:
                if (LaunchAvailableProjectile(type, startPoint))
                {
                    projectile = Instantiate(_simpleProjectilePrefab);
                    projectile.LaunchProjectile(startPoint);
                    _projectiles.Add(projectile);
                }
                break;
            case ProjectileType.Grenade:
                if (LaunchAvailableProjectile(type, startPoint))
                {
                    projectile = Instantiate(_grenadeProjectilePrefab);
                    projectile.LaunchProjectile(startPoint);
                    _projectiles.Add(projectile);
                }
                break;
            case ProjectileType.TennisBall:
                if (LaunchAvailableProjectile(type, startPoint))
                {
                    projectile = Instantiate(_tennisBallProjectilePrefab);
                    projectile.LaunchProjectile(startPoint);
                    _projectiles.Add(projectile);
                }
                break;
        }
    }

    private bool LaunchAvailableProjectile(ProjectileType type, Transform startPoint)
    {
        foreach (var projectile in _projectiles)
        {
            if (type == projectile.GetProjectileType() && projectile.IsAvailable())
            {
                projectile.LaunchProjectile(startPoint);
                return false;
            }
        }
        return true;
    }

    private void InitializePool()
    {
        _projectiles = new List<IPoolableProjectile>();
        _projectiles.Add(Instantiate(_simpleProjectilePrefab));
        _projectiles.Add(Instantiate(_grenadeProjectilePrefab));
        _projectiles.Add(Instantiate(_tennisBallProjectilePrefab));
    }
    
    
}

public enum ProjectileType
{
    Simple,
    Grenade,
    TennisBall
}


