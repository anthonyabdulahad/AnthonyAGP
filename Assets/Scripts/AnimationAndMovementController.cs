using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimationAndMovementController : MonoBehaviour
{
    new public Transform camera;
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    int _isWalkingHash;
    int _isRunningHash;

    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentRunMovement;
    Vector3 _appliedMovement;
    Vector3 _platformMovement;
    bool _isMovementPressed;
    bool _isRunPressed;

    float _rotationFactorPerFrame = 10.0f;
    float _runMultiplier = 5.0f;
    int _zero = 0;

    // gravity varibles
    float _gravity = -9.8f;
    float _groundedGravity = -.05f;

    bool Isdashing;
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 3.0f;
    float _maxJumpTime = 0.75f;
    bool _isJumping = false;
    int _isJumpingHash;
    int _jumpCountHash;
    bool _isJumpAnimating = false;
    int _jumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    private float coyoteTime = 0.3f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 2f;
    private float jumpBufferCounter;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();

        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Run.started += onRun;
        _playerInput.CharacterControls.Run.canceled += onRun;
        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;


        setupJumpVariables();
    }

    public bool IsMovementPressed()
    {
        return _isMovementPressed;
    }

    void setupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialvelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        initialJumpVelocities.Add(1, _initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialvelocity);

        jumpGravities.Add(0, _gravity);
        jumpGravities.Add(1, _gravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);
    }
    void handleJump()
    {

        if (!_isJumping && coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            jumpBufferCounter = 0f;

            if (_jumpCount < 3 && currentJumpResetRoutine != null)
            {
                StopCoroutine(currentJumpResetRoutine);
            }
            _animator.SetBool(_isJumpingHash, true);
            _isJumpAnimating = true;
            _isJumping = true;
            _jumpCount += 1;
            _animator.SetInteger(_jumpCountHash, _jumpCount);

            _currentMovement.y = initialJumpVelocities[_jumpCount];
            _appliedMovement.y = initialJumpVelocities[_jumpCount];

            coyoteTimeCounter = 0f;
        }
        else if (!_isJumpPressed && _isJumping && _characterController.isGrounded)
        {
            _isJumping = false;


        }
    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _jumpCount = 0;
    }



    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        Debug.Log($"{_isJumpPressed}");
    }

    void onRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        if (_isMovementPressed)
        {
            Vector3 direction = Vector3.Cross(camera.right, Vector3.up);
            Vector3 move = direction * _currentMovementInput.y + camera.right * _currentMovementInput.x;
            _currentMovement.x = move.x;
            _currentMovement.z = move.z;
            _currentRunMovement.x = move.x * _runMultiplier;
            _currentRunMovement.z = move.z * _runMultiplier;

            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            Quaternion currentRotation = transform.rotation;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }



    void onMovementInput(InputAction.CallbackContext context)
    {

        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentRunMovement.x = _currentMovementInput.x * _runMultiplier;
        _currentRunMovement.z = _currentMovementInput.y * _runMultiplier;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;


    }



    void handleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash)&& !Isdashing; 

        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }

        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        if ((_isMovementPressed && _isRunPressed) && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        else if ((!_isMovementPressed || !_isRunPressed) && isRunning)
        {

            _animator.SetBool(_isRunningHash, false);
        }
    }

    void handleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f || !_isJumpPressed;
        float fallMultiplier = 2.0f;

        if (_characterController.isGrounded)
        {
            Debug.Log("grounded");
            if (_isJumpAnimating)
            {

                _animator.SetBool("isJumping", false);
                _isJumpAnimating = false;
                currentJumpResetRoutine = StartCoroutine(jumpResetRoutine());
                if (_jumpCount == 3)
                {
                    _jumpCount = 0;
                    _animator.SetInteger(_jumpCountHash, _jumpCount);
                }
            }
            _currentMovement.y = _groundedGravity;
            _appliedMovement.y = _groundedGravity;

            
        }
        else if (isFalling)
        {
            if (!Isdashing)
            {
                float previousYVelocity = _currentMovement.y;
                _currentMovement.y = _currentMovement.y + (jumpGravities[_jumpCount] * fallMultiplier * Time.deltaTime);
                _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);
            }
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (jumpGravities[_jumpCount] * Time.deltaTime);
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;

        }
    }

    public void SetPlatformMovement(Vector3 platform)
    {
        _platformMovement = platform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
           jumpBufferCounter -= Time.deltaTime;
        }

        handleRotation();
        handleAnimation();

        if (!Isdashing)
        {
            if (_isRunPressed)
            {
                _appliedMovement.x = _currentRunMovement.x;
                _appliedMovement.z = _currentRunMovement.z;
            }
            else
            {
                _appliedMovement.x = _currentMovement.x;
                _appliedMovement.z = _currentMovement.z;
            }
        }

        _characterController.Move(_appliedMovement * Time.deltaTime + _platformMovement);

        handleGravity();
        handleJump();

    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    internal void Dash(float speed)
    {
        if (_isJumping)
        {
            Isdashing = speed > 0f;
            Vector3 direction = transform.forward;
            _currentMovement.y = 0;
            _appliedMovement.y = 0;
            _currentMovement.z = direction.z * speed;
            _appliedMovement.z = direction.z * speed;
            _currentMovement.x = direction.x * speed;
            _appliedMovement.x = direction.x * speed;
        }
        else
        {
            Isdashing = false;
        }
    }
}
