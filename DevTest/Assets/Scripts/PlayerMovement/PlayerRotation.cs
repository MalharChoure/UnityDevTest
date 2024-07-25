using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Grabs the orientation component inside the player main object. This object points in the direction of camera to player. this gives
    // us an accurate forward when we subtract y axes or when gravity is shifted the gravity axes. Super important
    [SerializeField] private Transform _orientation; 
    //Grabs the main player parent object. This holds your rigid body and collider. required to form endpoint of line connecting camera and player
    [SerializeField] private Transform _player;
    // PlayerObj is the actual body of the player. The skinned and animated mesh to be exact. This is required for the smooth turning and seemless animations
    [SerializeField] private Transform _playerObj;
    // Defines the speed at which the player will rotate around gravitational axes
    [SerializeField] private float rotationSpeed;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // The local axes are going to keep shifting in 3d space when gravity shits as object forward vector direction is unpredictable it is easier to deal with the virtual axes 

        // So here we use gravitational axes (stored in unity physics). Since we change the gravity often the only two things we rely on mainly are
        // Vector3 forward and gravitational axes. we can derive the right axes from the same. It is easier to rely on the virtual axes (forwad,gravity and right)
        // First we get the gravitational absolute normalized vector. 
        // we then get a counter vector eg g={0,1,0} ng={1,0,1}
        // this is so we can subtract gravity factor from the forward direction to a vector in the real plane (This is required ahead)
        Vector3 gravAbs = new Vector3(Mathf.Abs(Physics.gravity.normalized.x),Mathf.Abs( Physics.gravity.normalized.y),Mathf.Abs( Physics.gravity.normalized.z));
        Vector3 nonGravdir= Vector3.one-(gravAbs);
        // view dir stores the direction from cam to player
        Vector3 viewDir = _player.position - transform.position;
        // Subtract the graviational axes 
        viewDir = new Vector3(viewDir.x * nonGravdir.x, viewDir.y * nonGravdir.y, viewDir.z * nonGravdir.z);

        // set the forward direction to the orientation component
        _orientation.forward = viewDir;
        //Add a slight bit of gravity direction because the plane flips over sometimes when rotated in Euler
        _orientation.forward =new Vector3(_orientation.forward.x*nonGravdir.x, _orientation.forward.y * nonGravdir.y, _orientation.forward.z * nonGravdir.z)+gravAbs*0.01f;

        //Grab player inputs of WASD
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // since local coordinates need to be contantly translated back and forth just use the above mention virtual axes to compute vector right
        Vector3 actualRight = Vector3.Cross(_orientation.forward, Physics.gravity.normalized);
        // Use the above derived vector to create the desired rotation relative to _player object
        Vector3 inputDir = _orientation.forward * verticalInput + actualRight * horizontalInput;

        if (inputDir != Vector3.zero)// when the camera is not alined with the _player obj and motion is being done
        {
/*            Debug.DrawRay(_playerObj.transform.position,_playerObj.forward,Color.red,1f);
            Debug.DrawRay(_playerObj.transform.position, inputDir, Color.blue, 1f);*/
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir, Time.deltaTime * rotationSpeed);// Slerp to smootly rotate the player.
            _playerObj.localRotation = Quaternion.Euler(0.0f,_playerObj.localEulerAngles.y, 0.0f);// Makes sure the rotation is only along gravitational axes

        }
    }
}
