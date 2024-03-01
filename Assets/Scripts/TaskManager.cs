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
    public AudioClip welcomeAudio;
    public AudioClip startTasksAudio;
    public AudioClip nextTaskAudio;
    public AudioClip nextLevelAudio = null;

    private void Start()
    {
        PlayWelcomeAudio();
    }
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
            else
            {
                PlayNextLevelAudio();
            }
        }
    }

    public void NextLevel()
    {
        if(currentTask >= taskIcons.Length)
        {
            NextTask();
        }
        else
        {
            _NextLevel?.Invoke();
        }
    }
    private void PlayNextTaskAudio()
    {
        if(audSource != null && !nextTaskAudio.Equals(null))
        {
            audSource.PlayOneShot(nextTaskAudio);
        }
    }

    private void PlayWelcomeAudio()
    {
        if(audSource != null && !welcomeAudio.Equals(null))
        {
            audSource.PlayOneShot(welcomeAudio);
        }
    }

    public void PlayStartTasksAudio()
    {
        if (audSource != null && !startTasksAudio.Equals(null))
        {
            if (audSource.isPlaying)
            {
                audSource.Stop();
            }
            audSource.PlayOneShot(startTasksAudio);

        }
    }
    
    public void PlayNextLevelAudio()
    {
        if (audSource != null && !nextLevelAudio.Equals(null))
        {
            audSource.PlayOneShot(nextLevelAudio);
        }
    }
}
