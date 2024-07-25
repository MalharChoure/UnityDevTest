using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int _boxCollected;
    [SerializeField] private int _totalNoOfBoxes;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_boxCollected>=_totalNoOfBoxes)
        {
            GameState.Instance.TransitionToWin();
        }
    }


}
