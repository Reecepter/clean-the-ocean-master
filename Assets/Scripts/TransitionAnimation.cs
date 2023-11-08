using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionAnimation : MonoBehaviour
{
    Animator anim;
    //public AnimationClip unhealthyAnim;
    // Start is called before the first frame update
    //void Start()
    //{
    //    anim = GetComponent<Animator>();

    //}
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        Scene sceneCurrent = SceneManager.GetActiveScene();
        if (sceneCurrent.name.Equals("_mainScene_unhealthy"))
        {
            anim.SetBool("BeginUnhealthy", true);
        }
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
