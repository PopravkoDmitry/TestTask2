using UnityEngine;

public class TennisBallProjectile : Projectile
{
    [SerializeField] private int _maxNumberOfJumps;
    private int _numberOfJumps;


    private void Start()
    {
        ChangeColor();
    }


    protected override void OnCollisionEnter(Collision collision)
    {
        PlaySound();
        _numberOfJumps++;
        Instantiate(_explosionEffect, collision.contacts[0].point + collision.contacts[0].normal/2, Quaternion.LookRotation(collision.contacts[0].normal));
        if (_numberOfJumps ==_maxNumberOfJumps)
        {
            _numberOfJumps = 0;
            StartCoroutine(DisableProjectile());
        }
    }

    public override void LaunchProjectile(Transform launchingPoint)
    {
        transform.position = launchingPoint.position;
        transform.rotation = launchingPoint.rotation;
        gameObject.SetActive(true);
        _isAvailable = false;
        _meshRenderer.enabled = true;
        _rigidbody.isKinematic = false;
        _sphereCollider.enabled = true;
        ApplyForce(Vector3.up + transform.forward);
    }
    
}
