using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    // Standard singleton pattern 
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


    public void TransitionToLoose()// lose state
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("LoseScene");
    }

    public void TransitionToWin()// win state
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("WinScene");
    }

    public void Reset() // Resets play state
    {
        SceneManager.LoadScene("PlayScene");
    }
}
