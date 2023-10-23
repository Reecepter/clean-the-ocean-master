using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject ProjectileSpawnPoint;
    //[SerializeField]
    //GameObject ProjectileSpawnPoint2;
    [SerializeField]
    GameObject Projectile;

    public InputActionReference triggerInputActionRefence;
    // Start is called before the first frame update
    void OnEnable()
    {
        triggerInputActionRefence.action.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        GameObject shot;
        shot = Instantiate(Projectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
        Destroy(shot, 2.0f);
        //GameObject shot2;
        //shot2 = Instantiate(Projectile, ProjectileSpawnPoint2.transform.position, ProjectileSpawnPoint.transform.rotation);
        //Destroy(shot2, 2.0f);
    }

    private void OnDestroy()
    {
        triggerInputActionRefence.action.performed -= OnFire;
    }

}
