using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    Animator anim;
    //public AnimationClip unhealthyAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        anim.SetBool("StartAnimation", true);
    }
    public void StopAnimation()
    {
        anim.SetBool("StartAnimation", false);
    }
}
