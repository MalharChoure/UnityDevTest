using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _movementSpeed;// Sets player movement speed
    [SerializeField] Transform _orientation;// Gets player orientation and a handle to the Orientation gameobject
    [SerializeField] float _groundDrag;// Drag offered by the ground
    [SerializeField] float _jumpForce;// Magnitude of the jump force
    [SerializeField] float _jumpCoolDown;// Cooldown timer on the jump
    [SerializeField] float _airMultiplier;// Coyote time multiplier for movement
    bool _readyJump;// Readies the player jump after having jumped once

    [Header("Ground Check")]
    [SerializeField] float _playerHeight;// Height of the player for ground check
    [SerializeField] LayerMask _ground;//Layermask to seperate ground layers
    public bool grounded;// It sets the grounded flag when player is grounded

    [SerializeField] float _maxHoverTimeBeforeLose;// It dictates maximum time a player can hover before death.
    float _elapsedTime;// Time elapsed floating

    [Header("Animation")]
    [SerializeField] Animator _animator;// animator component of player
    [SerializeField] Animator _HologramAnimator;// animator component of hologram

    
    // Holds player horizontal axes input
    float _horizonatalInput;
    // Holds player vertical axes input
    float _verticalInput;
    // Direction in which the player needs to move
    public Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f,0);/// resets gravity in when scene resets
        rb = GetComponent<Rigidbody>();
        // We need to freeze physics based rotations
        rb.freezeRotation = true;
        _elapsedTime = _maxHoverTimeBeforeLose;
        _readyJump = true;
    }

    private void _MyInput()
    {
        _horizonatalInput = Input.GetAxisRaw("Horizontal");// Gathering player input for movement direction
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && grounded && _readyJump)// jump trigger
        {
            _readyJump = false;
            _Jump();
            Invoke(nameof(_ResetJump), _jumpCoolDown);
        }
    }

    private void _MovePlayer()
    {
        Vector3 temp= Vector3.Cross(_orientation.forward,Physics.gravity);// we take cross product to confirm vector3 . right as euler rotations are not super reliable
        moveDirection = _orientation.forward* _verticalInput + temp/temp.magnitude*_horizonatalInput;// Sets move direction irrespective of gravity direction
        _animator.SetFloat("WalkingSpeed", moveDirection.magnitude);// trigger animations
        _HologramAnimator.SetFloat("WalkingSpeed", moveDirection.magnitude);
        if (grounded)// Add drag so that player does not slide around
        {
            rb.AddForce(moveDirection.normalized * _movementSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)// coyote time muliplier
        {
            rb.AddForce(moveDirection.normalized * _movementSpeed * 10f * _airMultiplier, ForceMode.Force);
        }
    }

    private void Update()
    {
        // Checks if grounded
        grounded = Physics.Raycast(_orientation.position, Physics.gravity.normalized, _playerHeight * 0.5f + 0.2f, _ground);
        _animator.SetBool("Grounded", grounded);// sets animation parameter
        _HologramAnimator.SetBool("Grounded", grounded);
        _MyInput();
        if (grounded)// add gorund drag
        {
            _elapsedTime = _maxHoverTimeBeforeLose;
            rb.drag = _groundDrag;
        }
        else // checks elapsed time to kill on hovering too long AKA falling off
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
        _SpeedControl();// Caps the speed as acceleration of rigid body can surpass player set speeds
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
