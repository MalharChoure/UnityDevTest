
using UnityEngine;
// Class used to manipulate gravity and change axis.
[RequireComponent(typeof(Rigidbody))]
public class GravityManip : MonoBehaviour
{
    Rigidbody _rb;
    // orientation to derive local axes parallel to true axes 
    [SerializeField] Transform _orientation;
    // Magnitude of gravity
    [SerializeField] float _gravityScale;
    // handle for the hologram used in game to denote direction
    [SerializeField] GameObject _hologram;
    // Anchorpoint above the player to rotate hologram and player
    [SerializeField] Transform _anchorPoint;
    // Virtual axes parallel to Worlds space axes.
    Vector3 top, forward, right = Vector3.zero;
    // Bool to check if hologram is active
    bool _holoactive;
    // Intergers to hold 2 bit orientation 
    int FrontHolo, RightHolo;
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
        //  f  r (f- forward axis r-right axis)
        // -1  0 Front axes
        //  1  0 back axes
        //  0  1 left axes
        //  0 -1 right axes

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FrontHolo = -1;
            RightHolo = 0;

            resetHologram();

            showHoloDirection(FrontHolo, RightHolo);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FrontHolo = 1;
            RightHolo = 0;
            resetHologram();

            showHoloDirection(FrontHolo, RightHolo);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontHolo = 0;
            RightHolo = 1;
            resetHologram();

            showHoloDirection(FrontHolo, RightHolo);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FrontHolo = 0;
            RightHolo = -1;
            resetHologram();

            showHoloDirection(FrontHolo, RightHolo);
        }
        // Resets Hologram if key is not held pressed
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            resetHologram();
        }
        // Execute the gravity shift
        if(Input.GetKeyDown(KeyCode.Space) && _holoactive)
        {
            setGravityAxis(FrontHolo,RightHolo);
            rotatePlayer(FrontHolo, RightHolo);
        }

    }

    // Here we actually change the gravity.
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

    //Here we are able to get virtual axis parallel to world space axes this is where the calculation is done to get the largest axes in a vector and consider it the dominant axis
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

    // This sets the direction in which you will travel if you choose to execute gravity shift.
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

    //Resets hologram when no key is pressed.
    public void resetHologram()
    {
        _hologram.transform.localPosition = new Vector3(0,0,0);
        _hologram.transform.localRotation = new Quaternion(0.0f,0.0f,0.0f,0f);
        _hologram.SetActive(false);
    }

    //get virtual axis parallel to world space axes
    public void setLocalToGlobalAxes()
    {
        Vector3 temp = _orientation.forward.normalized;
        Vector3 temp2 = _orientation.up.normalized;
        Vector3 temp3 = _orientation.right.normalized;


        top = ReturnWorldSpaceAxis(temp2);
        forward = ReturnWorldSpaceAxis(temp);
        right = ReturnWorldSpaceAxis(temp3);
    }
     
    //It rotates the player on the virtual axes
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
