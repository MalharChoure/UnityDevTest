using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityManip : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] Transform _orientation;
    Vector3 _gravityAxis;
    [SerializeField] float _gravityScale;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            setGravityAxis(1,0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            setGravityAxis(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            setGravityAxis(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            setGravityAxis(0, -1);
        }

    }

    private void setGravityAxis(int f, int r)
    {
        Vector3 temp = _orientation.forward.normalized;
        Vector3 temp2 = _orientation.up.normalized;
        Vector3 temp3 = _orientation.right.normalized;
        Vector3 top = Vector3.zero;
        Vector3 forward = Vector3.zero;
        Vector3 right = Vector3.zero;


        top = ReturnWorldSpaceAxis(temp2);
        forward = ReturnWorldSpaceAxis(temp);
        right = ReturnWorldSpaceAxis(temp3);

        Debug.DrawRay(_orientation.position,top,Color.green,100f);
        Debug.DrawRay(_orientation.position, forward, Color.red, 100f);
        Debug.DrawRay(_orientation.position, right, Color.blue, 100f);
        if (f == 0 && r == 1)
        {
            Physics.gravity = right * _gravityScale;
        }
        if(f==0 && r==-1)
        {
            Physics.gravity = -right * _gravityScale;
        }
        if(f==1 && r==0)
        {
            Physics.gravity = forward * _gravityScale;
        }
        if (f == -1 && r == 0)
        {
            Physics.gravity = -forward * _gravityScale;
        }

    }

    public Vector3 ReturnWorldSpaceAxis(Vector3 dir)
    {
        Vector3 temp = Vector3.zero;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            temp = new Vector3(dir.x / Mathf.Abs(dir.x), 0, 0);
        }
        else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x) && Mathf.Abs(dir.y) > Mathf.Abs(dir.z))
        {
            temp = new Vector3(0, dir.y / Mathf.Abs(dir.y), 0);
        }
        else
        {
            temp = new Vector3(0, 0, dir.z / Mathf.Abs(dir.z));
        }
        return temp;
    }
}
