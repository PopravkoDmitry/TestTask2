using System;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _roationSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _spawnPoint;
    public ProjectileType projectileType;
    [SerializeField] private bool _canShoot;
    private Vector2 _movementDirection;
    private ProjectilePool _projectilePool;
    public bool CanShoot
    {
        set => _canShoot = value;
    }

    private void Awake()
    {
        _projectilePool = ProjectilePool.Instance;
    }


    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            _rigidbody.velocity = transform.right * (_movementSpeed * Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            _rigidbody.angularVelocity = new Vector3(0f, Input.GetAxis("Horizontal") * _roationSpeed, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            _projectilePool.LaunchProjectile(projectileType, _spawnPoint);
        }
    }
}
