using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapons : MonoBehaviour
{
    ShootScript shoot;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collison");
        if (other.gameObject.tag.Equals("Player"))
        {
            shoot = other.gameObject.GetComponentInChildren<ShootScript>();
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
}
