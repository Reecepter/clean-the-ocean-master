using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro trashCountScreen;
    public int _trashCollected;
    public PlayableDirector PD;
    public GameManager GM;
    public string targetText = "";
    // Start is called before the first frame update
    void Start()
    {
        _trashCollected = 0;
        trashCountScreen.text = _trashCollected + targetText;
    }

    public void AddTrashCollected()
    {
        _trashCollected++;
        trashCountScreen.text = _trashCollected + targetText;
        if ((GameManager.currentstate == 1 && _trashCollected == 5) || (GameManager.currentstate == 2 && _trashCollected == 10))
        {
            PD.Resume();
            GM.stopWaiting();
        }
    }

    public void setTargetText() {
        trashCountScreen.text = _trashCollected + targetText;
    }
}
