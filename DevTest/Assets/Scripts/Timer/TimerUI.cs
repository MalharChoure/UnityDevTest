using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]TMP_Text text;// Holds text handle for timer.
    float elapsedTime;
    [SerializeField] float looseConditionTime; // Timer maximum value.


    void Update()
    {
        looseConditionTime -= Time.deltaTime;
        int minutes=Mathf.FloorToInt(looseConditionTime/60);
        int seconds = Mathf.FloorToInt(looseConditionTime % 60);
        text.text="Time: "+string.Format("{0:00}:{1:00}", minutes, seconds);// Formating the UI to look like a clock
        if(looseConditionTime<=0)
        {
            GameState.Instance.TransitionToLoose();// calls gamestate to proceed to lose state
        }
    }
}
