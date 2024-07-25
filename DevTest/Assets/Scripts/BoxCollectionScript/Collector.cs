using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

/// <summary>
/// Collector script to collect the various holographic boxes 
/// </summary>
public class Collector : MonoBehaviour
{
    // stores the number of boxes collected
    public int _boxCollected;
    //Max no of boxes there
    [SerializeField] private int _totalNoOfBoxes;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Win condition after collecting the required no of boxes.
        if(_boxCollected>=_totalNoOfBoxes)
        {
            GameState.Instance.TransitionToWin();
        }
    }


}
