using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void PlayScene()
    {
        SceneManager.LoadScene("PlayScene");// navigates player to play scene
    }
}
