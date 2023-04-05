using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
 
public class ExamplePlayerController : MonoBehaviour, PlatformMovement
{

    [SerializeField]
    private GameObject MainCamera;

    Animator _animator;
    int _isRunningHash;
    int _isJumpingHash;

    private CharacterController _controller;
    private const float MovementSpeed = 10f;
    public float RotationSpeed = 0.75f;
    public Vector3 _movementInput;
    private Vector3 _rotationInput;

    private const float JumpForce = 28f;
    private const float Gravity = 9.8f;
    private float _groundedTime = 0f;
    public float Jumpcooldown;

    private float coyoteTime = 0.3f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 2f;
    private float jumpBufferCounter;
    private bool _isJumpPressed;

    private Vector3 platformMovement;
    private float currentSpeed = 10f;
    internal object Controller;
    public float Jump;

    private bool isBouncingBox;
   

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
    }
    public void OnJump(InputValue context)
    {
        _isJumpPressed = context.isPressed;
    }
    void Update()
    {
        if (_controller.isGrounded)
        {
            //_movementInput.y = -2f;

            _groundedTime += Time.deltaTime;
            coyoteTimeCounter = coyoteTime;
            _animator.SetBool(_isJumpingHash, false);
        }
        else
        {
            _groundedTime = 0f;
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (_isJumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        ReadMovementInputs();
        ReadJumpInputs();
        Move();
    }

    void ReadMovementInputs()
    {
        float xAxisMovement = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        float zAxisMovement = Input.GetAxisRaw("Vertical") * MovementSpeed;
        Vector3 cameraRight = MainCamera.transform.right * 1.1f;
        Vector3 cameraForward = MainCamera.transform.forward * 1.1f;
        Vector3 relativeMovement = cameraRight * xAxisMovement + cameraForward * zAxisMovement;
        _movementInput.Set(relativeMovement.x, _movementInput.y, relativeMovement.z);

        Vector3 desiredForward = new Vector3(relativeMovement.x, 0, relativeMovement.z);
        _rotationInput = Vector3.RotateTowards(transform.forward, desiredForward,
            RotationSpeed, 0f);
    }

    void ReadJumpInputs()
    {
        if (_controller.isGrounded && _isJumpPressed && _groundedTime > Jumpcooldown)
        {
            _movementInput.y = JumpForce;
            _animator.SetBool(_isJumpingHash, true);
            _isJumpPressed = false;
        }

        //if (!_controller.isGrounded || _movementInput.y > 0)
        //{
            _movementInput.y += Physics.gravity.y * Gravity * Time.deltaTime;
        //}
    }

    void Move()
    {
        if (_controller.isGrounded)
        {
            RotationSpeed = .15f;
        }
        else
        {
            RotationSpeed = .05f;
        }

        if (_movementInput != Vector3.zero)
            {
                Vector3 moveDirection = Vector3.zero;
                if (_movementInput.x != 0 || _movementInput.z != 0)
                {
                    moveDirection = transform.TransformDirection(Vector3.forward) * currentSpeed;
                    if (_controller.isGrounded)
                    {
                     _animator.SetBool(_isRunningHash, true);
                    }
                

                }
                else
                {
                    if (_controller.isGrounded)
                    {
                        _animator.SetBool(_isRunningHash, false);
                    }

                }

                moveDirection.y = _movementInput.y;
                _controller.Move(moveDirection * Time.deltaTime + platformMovement);
                transform.rotation = Quaternion.LookRotation(_rotationInput * Time.deltaTime);
            }
    }

    public void SetPlatformMovement(Vector3 platform)
    {
        platformMovement = platform;
    }

    public void RespawnTo(Vector3 position)
    {
        _controller.enabled = false;
        transform.position = position;
        _controller.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Player hits {other.name}", other.gameObject);
    }

    public void Dash(float dashSpeed)
    {
        currentSpeed = MovementSpeed * dashSpeed;
    }

    public void DoJump()
    {
        _movementInput.y = JumpForce;
        //_controller.Move(new Vector3(0f, 2f, 0f));
        _animator.SetBool(_isJumpingHash, true);
    }
}    