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
    [SerializeField] GameObject _hologram;
    [SerializeField] Transform _anchorPoint;
    Vector3 top,forward,right = Vector3.zero;
    bool _holoactive;
    int FrontHolo;
    int RightHolo;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            setLocalToGlobalAxes();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FrontHolo = -1;
            RightHolo = 0;

            resetHologram();
            //setGravityAxis(FrontHolo, RightHolo);
            showHoloDirection(FrontHolo, RightHolo);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FrontHolo = 1;
            RightHolo = 0;
            resetHologram();
            //setGravityAxis(FrontHolo, RightHolo);
            showHoloDirection(FrontHolo, RightHolo);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontHolo = 0;
            RightHolo = 1;
            resetHologram();
            //setGravityAxis(FrontHolo, RightHolo);
            showHoloDirection(FrontHolo,RightHolo);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FrontHolo = 0;
            RightHolo = -1;
            resetHologram();
            //setGravityAxis(FrontHolo, RightHolo);
            showHoloDirection(FrontHolo, RightHolo);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            resetHologram();
        }
        if(Input.GetKeyDown(KeyCode.Space) && _holoactive)
        {
            setGravityAxis(FrontHolo,RightHolo);
            rotatePlayer(FrontHolo, RightHolo);
        }

    }

    private void setGravityAxis(int f, int r)
    {
        

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

    public void showHoloDirection(int front, int rightSide)
    {
        Debug.Log("Front" +front+"Rear" + right);
        _hologram.SetActive(true);
        if (rightSide == 1)
        {
            _hologram.transform.RotateAround(_anchorPoint.position, forward, -90);
        }
        else if(rightSide == -1)
        {
            _hologram.transform.RotateAround(_anchorPoint.position, forward, 90);
        }
        else if (front==1)
        {
            _hologram.transform.RotateAround(_anchorPoint.position, right, 90);
        }
        else if (front == -1)
        {
            _hologram.transform.RotateAround(_anchorPoint.position, right, -90);
        }
        _holoactive = true;
    }

    public void resetHologram()
    {
        _hologram.transform.localPosition = new Vector3(0,0,0);
        _hologram.transform.localRotation = new Quaternion(0.0f,0.0f,0.0f,0f);
        _hologram.SetActive(false);
    }

    public void setLocalToGlobalAxes()
    {
        Vector3 temp = _orientation.forward.normalized;
        Vector3 temp2 = _orientation.up.normalized;
        Vector3 temp3 = _orientation.right.normalized;


        top = ReturnWorldSpaceAxis(temp2);
        forward = ReturnWorldSpaceAxis(temp);
        right = ReturnWorldSpaceAxis(temp3);
    }
     
    private void rotatePlayer(int x, int y)
    {
        if(x==1)
        _rb.transform.RotateAround(_anchorPoint.position, right, 90);
        else if(x==-1)
        _rb.transform.RotateAround(_anchorPoint.position, right, -90);
        if (y == 1)
            _rb.transform.RotateAround(_anchorPoint.position, forward, -90);
        else if (y == -1)
            _rb.transform.RotateAround(_anchorPoint.position, forward, 90);
    }

}
