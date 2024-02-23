using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    private AudioSource audSource;
    public AudioClip NextDropZone;
    // Start is called before the first frame update
    void Start()
    {
        audSource = GetComponent<AudioSource>();
    }

    public void PlayNextDropZone()
    {
        audSource.PlayOneShot(NextDropZone);
    }
}
