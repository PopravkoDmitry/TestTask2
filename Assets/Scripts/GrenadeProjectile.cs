using System.Collections;
using UnityEngine;

public class GrenadeProjectile : Projectile
{
    [SerializeField] private float _radius;
    [SerializeField] private Color _gizmosColor;
    private Collider[] _boxesCollidersList;
    [SerializeField] private LayerMask _layersToCheck;
    [SerializeField] private float _explosionForce;
    
    void Start()
    {
        ChangeColor();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        PlaySound();
        Instantiate(_explosionEffect, transform.position, transform.rotation);
        _boxesCollidersList = Physics.OverlapSphere(transform.position, _radius);
        foreach (var box in _boxesCollidersList)
        {
            if ((_layersToCheck.value & 1 << box.gameObject.layer) > 0)
            {
                box.gameObject.GetComponent<Rigidbody>()
                    .AddForce((box.transform.position - transform.position).normalized * _explosionForce);
            }
        }

        StartCoroutine(DisableProjectile());
        //Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawSphere(transform.position, _radius);
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
        ApplyForce(transform.forward + transform.up);
    }
}
