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
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<PlayerInput>().
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelect()
    {
        GameObject shot;
        shot = Instantiate(Projectile, ProjectileSpawnPoint.transform.position, ProjectileSpawnPoint.transform.rotation);
        Destroy(shot, 2.0f);
    }
}
