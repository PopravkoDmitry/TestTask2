using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolableProjectile
{
    [SerializeField] private float _speed;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private Color _projectileColor;
    [SerializeField] private float _deathTime;
    [SerializeField] protected ParticleSystem _explosionEffect;
    [SerializeField] protected TrailRenderer _trailRenderer;
    [SerializeField] protected MeshRenderer _meshRenderer;
    [SerializeField] protected SphereCollider _sphereCollider;
    private bool _isCollided;
    [SerializeField] protected bool _isAvailable;
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] protected SoundManager _soundManager;
    [SerializeField] private string _soundKey;

    private void Awake()
    {
        _soundManager = SoundManager.Instance;
    }

    void Start()
    {
        ChangeColor();
    }

    protected void ApplyForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _speed, ForceMode.Acceleration);
    }

    protected void ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = _projectileColor;
    }


    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!_isCollided)
        {
            StartCoroutine(DestroyDelay());
            _isCollided = true;
        }
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_deathTime);
        Instantiate(_explosionEffect, transform.position, transform.rotation);
        //Destroy(gameObject);
        StartCoroutine(DisableProjectile());
    }
    
    protected IEnumerator DisableProjectile()
    {
        PlaySound();
        _meshRenderer.enabled = false;
        _rigidbody.isKinematic = true;
        _sphereCollider.enabled = false;
        _isCollided = false;
        yield return new WaitForSeconds(_trailRenderer.time);
        gameObject.SetActive(false);
        _isAvailable = true;
    }

    public virtual void LaunchProjectile(Transform launchingPoint)
    {
        transform.position = launchingPoint.position;
        transform.rotation = launchingPoint.rotation;
        gameObject.SetActive(true);
        _isAvailable = false;
        _meshRenderer.enabled = true;
        _rigidbody.isKinematic = false;
        _sphereCollider.enabled = true;
        ApplyForce(transform.forward);
    }


    bool IPoolableProjectile.IsAvailable()
    {
        return _isAvailable;
    }

    public ProjectileType GetProjectileType()
    {
        return _projectileType;
    }

    protected virtual void PlaySound()
    {
        _soundManager.PlaySoundAtPoint(_soundKey, transform);
    }
}
