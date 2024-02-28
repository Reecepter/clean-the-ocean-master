using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapons : MonoBehaviour
{
    public GameObject player;
    ShootScript shoot;
    public AudioSource audSource;
    public AudioClip weaponsOnline;

    private void OnEnable()
    {
        shoot = player.GetComponentInChildren<ShootScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collison");
        if (other.gameObject.tag.Equals("Player"))
        {
            //shoot = other.gameObject.GetComponentInChildren<ShootScript>();
            shoot.EnableShoot();
            PlayWeaponsOnline();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            shoot.DisableShoot();
        }
    }
    private void OnDisable()
    {
        //shoot.DisableShoot();
    }

    private void PlayWeaponsOnline()
    {
        if(audSource != null && !weaponsOnline.Equals(null))
        {
            if (audSource.isPlaying)
            {
                audSource.Stop();
            }
            audSource.PlayOneShot(weaponsOnline);
        }
    }
}
