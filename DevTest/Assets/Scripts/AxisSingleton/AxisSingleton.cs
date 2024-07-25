using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxisSingleton : MonoBehaviour
{

    new Vector3[] orient= new Vector3[3];
    // Start is called before the first frame update
    void Start()
    {
        orient[(int)axis.up] = Vector3.up;
        orient[(int)axis.right] = Vector3.right;
        orient[(int)axis.front] = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shiftAxis(Vector3 gravity)
    {
        if(gravity.y==0 && gravity.z==0)
        {

            orient[(int)axis.right] = -orient[(int)axis.up];
            orient[(int)axis.up] = gravity;
        }
        if(gravity.x==0 && gravity.z==0)
        {
            orient[(int)axis.up] = -orient[(int)axis.up];
            orient[(int)axis.up] = gravity;
        }
    }

}

public enum axis
{
    up,
    right,
    front
}
