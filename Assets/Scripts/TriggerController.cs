using System;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private LayerMask layerToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if ((layerToTrigger.value & 1 << other.gameObject.layer) <= 0)
        {
            return;
        }
        
        var robotController = other.gameObject.GetComponent<RobotController>();
        robotController.projectileType = _projectileType;
        robotController.CanShoot = true;
        Debug.Log("Can Shoot");
    }

    private void OnTriggerExit(Collider other)
    {
        if ((layerToTrigger.value & 1 << other.gameObject.layer) <= 0)
        {
            return;
        }

        other.gameObject.GetComponent<RobotController>().CanShoot = false;
        
        Debug.Log("Cant Shoot");
    }
}
