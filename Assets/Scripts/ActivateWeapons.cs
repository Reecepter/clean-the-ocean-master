using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapons : MonoBehaviour
{
    public GameObject player;
    ShootScript shoot;

    private void Start()
    {
        shoot = player.gameObject.GetComponentInChildren<ShootScript>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collison");
        if (other.gameObject.tag.Equals("Player"))
        {
            //shoot = other.gameObject.GetComponentInChildren<ShootScript>();
            shoot.EnableShoot();
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
        shoot.DisableShoot();
    }
}
