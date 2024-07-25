using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void TransitionToLoose()
    {
        SceneManager.LoadScene("Losescene");
    }

    public void TransitionToWin()
    {
        SceneManager.LoadScene("Victoryscene");
    }

    public void Reset()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
