using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerObj;
    //[SerializeField] private Rigidbody _rb;
    //[SerializeField] private Transform _target;
    //[SerializeField] private PlayerMovement _movement;

    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 gravAbs = new Vector3(Mathf.Abs(Physics.gravity.normalized.x),Mathf.Abs( Physics.gravity.normalized.y),Mathf.Abs( Physics.gravity.normalized.z));
        Vector3 nonGravdir= Vector3.one-(gravAbs);
        //Vector3 otherDir = new Vector3(nonGravdir.x * transform.position.x, nonGravdir.y * transform.position.y, nonGravdir.z * transform.position.z);
        //Vector3 gravDir = new Vector3 (gravAbs.x* _player.position.x, gravAbs.y * _player.position.y, gravAbs.z * _player.position.z);
        Vector3 viewDir = _player.position - transform.position;//(otherDir+gravDir);
        viewDir = new Vector3(viewDir.x * nonGravdir.x, viewDir.y * nonGravdir.y, viewDir.z * nonGravdir.z);
        //Debug.Log(viewDir);
        
        //_orientation.up = -Physics.gravity;
        _orientation.forward = viewDir;
        _orientation.forward =new Vector3(_orientation.forward.x*nonGravdir.x, _orientation.forward.y * nonGravdir.y, _orientation.forward.z * nonGravdir.z)+gravAbs*0.01f;
        //Debug.Log(nonGravdir);
        //_orientation.eulerAngles = _player.eulerAngles;
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 actualRight = Vector3.Cross(_orientation.forward, Physics.gravity.normalized);

        Vector3 inputDir = _orientation.forward * verticalInput + actualRight * horizontalInput;//_playerObj.forward - (new Vector3 (gravAbs.x* _playerObj.forward.x, gravAbs.x * _playerObj.forward.y, gravAbs.z * _playerObj.forward.z)) * verticalInput+  _playerObj.right * horizontalInput;
        //inputDir = new Vector3(inputDir.x * nonGravdir.x, inputDir.y * nonGravdir.y, inputDir.z * nonGravdir.z) + Physics.gravity;

        if (inputDir != Vector3.zero)
        {
            //Debug.Log(_playerObj.forward+""+inputDir);
            Debug.DrawRay(_playerObj.transform.position,_playerObj.forward,Color.red,1f);
            Debug.DrawRay(_playerObj.transform.position, inputDir, Color.blue, 1f);
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir, Time.deltaTime * rotationSpeed);
            _playerObj.localRotation = Quaternion.Euler(0.0f,_playerObj.localEulerAngles.y, 0.0f);
            //_playerObj.localRotation = Quaternion.Euler(0.0f, Vector3.Angle(_playerObj.forward, inputDir)* Time.deltaTime,0.0f);
            //_playerObj.RotateAround(_playerObj.position, Physics.gravity, Vector3.Angle(_playerObj.forward,inputDir)*Time.deltaTime* -horizontalInput);
            //_playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized,Time.deltaTime * rotationSpeed);
        }
    }
}
