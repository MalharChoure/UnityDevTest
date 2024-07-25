using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _movementSpeed;
    [SerializeField] Transform _orientation;
    [SerializeField] float _groundDrag;
    [SerializeField] float _jumpForce;
    [SerializeField] float _jumpCoolDown;
    [SerializeField] float _airMultiplier;
    bool _readyJump;

    [Header("Ground Check")]
    [SerializeField] float _playerHeight;
    [SerializeField] LayerMask _ground;
    public bool grounded;

    [SerializeField] float _maxHoverTimeBeforeLose;
    float _elapsedTime;

    [Header("Animation")]
    [SerializeField] Animator _animator;

    float _horizonatalInput;
    float _verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _elapsedTime = _maxHoverTimeBeforeLose;
        _readyJump = true;
    }

    private void _MyInput()
    {
        _horizonatalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && grounded && _readyJump)
        {
            _readyJump = false;
            _Jump();
            Invoke(nameof(_ResetJump), _jumpCoolDown);
        }
    }

    private void _MovePlayer()
    {
        moveDirection = _orientation.forward* _verticalInput + _orientation.right*_horizonatalInput;
        _animator.SetFloat("WalkingSpeed", moveDirection.magnitude);
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * _movementSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * _movementSpeed * 10f * _airMultiplier, ForceMode.Force);
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _ground);
        _animator.SetBool("Grounded", grounded);
        _MyInput();
        if (grounded)
        {
            _elapsedTime = _maxHoverTimeBeforeLose;
            rb.drag = _groundDrag;
        }
        else
        {
            _elapsedTime -= Time.deltaTime;
            //Debug.Log(_elapsedTime);
            if(_elapsedTime<=0)
            {
                _elapsedTime = 0;
                GameState.Instance.TransitionToLoose();
                //Debug.Log("Here");
            }
            rb.drag = 0;
        }
        _SpeedControl();
    }

    private void FixedUpdate()
    {
        _MovePlayer();
    }

    private void _SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > _movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _movementSpeed;
            rb.velocity = new Vector3( limitedVel.x,rb.velocity.y, limitedVel.z);
        }
    }

    private void _Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void _ResetJump()
    {
        _readyJump = true;
    }
}
