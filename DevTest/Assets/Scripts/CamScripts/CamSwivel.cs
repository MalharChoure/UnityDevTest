using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwivel : MonoBehaviour
{
    /*[SerializeField] float _turnSpeed;
    [SerializeField] float _orbitOffset;

    [SerializeField] float _verticalTurnSpeed;

    [SerializeField] Transform _objectToWatch;


    float _theta;
    float _phi;*/

    [SerializeField] private bool _CanMove = false;

    /// <summary>
    /// The Actual object that we want to swivel about.
    /// </summary>


    [SerializeField] private Transform _SwivelObject;

    /// <summary>
    /// Radius for the radial coordinate system
    /// </summary>
    [SerializeField] private float _Radius = 3f;
    /// <summary>
    /// Theta of the radial coordinate system
    /// </summary>
    [SerializeField] private float _Theta = 45f;
    /// <summary>
    /// Phi of the radial coordinate system
    /// </summary>
    [SerializeField] private float _Phi = 45f;
    /// <summary>
    /// Float to determin how fast we can move the camera
    /// </summary>
    [SerializeField] private float _SwivelSpeed = 0f;

    private Vector3 _PositionCastesian;
    // Start is called before the first frame update
    void Start()
    {
        //Calculations to convert radial to cartesian as the coordinate system that unity uses is cartesian
        float x = _Radius * Mathf.Cos(_Theta) * Mathf.Sin(_Phi);
        float y = _Radius * Mathf.Sin(_Theta) * Mathf.Sin(_Phi);
        float z = _Radius * Mathf.Cos(_Phi);
        _PositionCastesian = new Vector3(x, y, z);
        //initially setting the transform of the system.
        transform.position = _PositionCastesian;
    }

    // Update is called once per frame
    void Update()
    {
        _InputScript();
        float x = _Radius * Mathf.Cos(_Theta) * Mathf.Sin(_Phi);
        float z = _Radius * Mathf.Sin(_Theta) * Mathf.Sin(_Phi);
        float y = _Radius * Mathf.Cos(_Phi);
        _PositionCastesian = new Vector3(x, y, z);
        transform.position = _PositionCastesian;


        transform.LookAt(_SwivelObject);
    }

    /// <summary>
    /// This method is used to swivel around the object that we are editing
    /// </summary>
    private void _InputScript()
    {
        //if (Input.GetMouseButton(1))
        //{
            _Phi = Mathf.Clamp((_Phi + Input.GetAxis("Mouse Y") * Time.deltaTime * _SwivelSpeed), Mathf.PI / 6, 5 * Mathf.PI / 6);
            _Theta = (_Theta - Input.GetAxis("Mouse X") * Time.deltaTime * _SwivelSpeed);
        //}
        //_Radius = _Radius - Input.mouseScrollDelta.y;
    }
    /// <summary>
    /// Getter and setter method for changing the
    /// <see cref="_CanMove"/>
    /// bool 
    /// </summary>
    public bool CanMove { get { return _CanMove; } set { _CanMove = value; } }


    public Transform ChangeTransform { set { _SwivelObject = value; } }
}
