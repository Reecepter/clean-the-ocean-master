using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public GameObject[] taskIcons = new GameObject[7];
    public GameObject[] taskTriggers = new GameObject[7];
    private int currentTask = 0;
    [SerializeField]
    private UnityEvent _NextLevel;
    public AudioSource audSource;
    public AudioClip nextTaskAudio;
    
    public void NextTask()
    {
        taskIcons[currentTask].SetActive(false);
        taskTriggers[currentTask].SetActive(false);
        currentTask++;
        if(currentTask >= taskIcons.Length)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (!currentScene.name.Equals("_mainScene_unhealthy"))
            {
                _NextLevel?.Invoke();
            }
        }
        else
        {
            taskIcons[currentTask].SetActive(true);
            taskTriggers[currentTask].SetActive(true);
            if(currentTask != 7)
            {
                PlayNextTaskAudio();
            }
        }
    }

    private void PlayNextTaskAudio()
    {
        if(audSource != null && !nextTaskAudio.Equals(null))
        {
            audSource.PlayOneShot(nextTaskAudio);
        }
    }
}
