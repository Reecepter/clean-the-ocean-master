using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seawormAttack : MonoBehaviour
{
    public Animator amin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMonsterAttack() {
        amin.SetBool("isAttacking", true);
    }
}
