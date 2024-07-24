using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private Rigidbody _rb;

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
        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized,Time.deltaTime * rotationSpeed);
        }
    }
}
