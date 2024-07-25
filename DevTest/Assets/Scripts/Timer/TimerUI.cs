using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]TMP_Text text;
    float elapsedTime;
    [SerializeField] float looseConditionTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        looseConditionTime -= Time.deltaTime;
        int minutes=Mathf.FloorToInt(looseConditionTime/60);
        int seconds = Mathf.FloorToInt(looseConditionTime % 60);
        text.text="Time: "+string.Format("{0:00}:{1:00}", minutes, seconds);
        if(looseConditionTime<=0)
        {
            GameState.Instance.TransitionToLoose();
        }
    }
}
