using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    GameObject ProjectileSpawnPoint;
    [SerializeField]
    GameObject Projectile;

    public InputActionReference triggerInputActionRefence;
    // Start is called before the first frame update
    void Awake()
    {
        triggerInputActionRefence.action.performed += OnFire;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        GameObject shot;
        shot = Instantiate(Projectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
        Destroy(shot, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnFire()
    //{
    //    GameObject shot;
    //    shot = Instantiate(Projectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
    //    Destroy(shot, 2.0f);
    //}
   
}
