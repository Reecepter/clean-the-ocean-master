using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootParticle : MonoBehaviour
{
    public ParticleSystem shootParticle;

    private void Start()
    {
        shootParticle = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Barrel") == true)
        {
            other.SetActive(false);
            
        }
    }

}
