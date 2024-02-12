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
    [SerializeField]
    ParticleSystem shootParticle;
    public AudioSource particleSound;

    public GameObject reticle;

    public GameObject ship;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    public InputActionReference triggerInputActionRefence;
    bool canShoot = false;


    // Start is called before the first frame update
    void OnEnable()
    {
        triggerInputActionRefence.action.performed += OnFire;
        reticle.SetActive(false);
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (canShoot == true)
        {
            StartCoroutine(StartRecoil());
            shootParticle.Play();
            particleSound.Play();
        }
    }
    public void EnableShoot()
    {
        canShoot = true;
        reticle.SetActive(true);
    }
    public void DisableShoot()
    {
        canShoot = false;
        reticle.SetActive(false);
    }
    private void OnDestroy()
    {
        triggerInputActionRefence.action.performed -= OnFire;
    }

    IEnumerator StartRecoil()
    {
        ship.GetComponent<Animator>().Play("Recoil");
        yield return new WaitForSeconds(0.20f);
        ship.GetComponent<Animator>().Play("New State");
    }

}
