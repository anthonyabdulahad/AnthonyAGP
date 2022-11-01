using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
 
public class ExamplePlayerController : MonoBehaviour
{

    [SerializeField]
    private GameObject MainCamera;

    Animator _animator;
    int _isRunningHash;
    int _isJumpingHash;

    private CharacterController _controller;
    private const float MovementSpeed = 10f;
    private const float RotationSpeed = 0.75f;
    public Vector3 _movementInput;
    private Vector3 _rotationInput;

    private const float JumpForce = 35f;
    private const float Gravity = 15f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _animator.SetBool(_isJumpingHash, false);
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
        if (_controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            _movementInput.y = JumpForce;
           // _animator.SetBool(_isJumpingHash, true);
        }

        if (!_controller.isGrounded || _movementInput.y > 0)
        {
            _movementInput.y += Physics.gravity.y * Gravity * Time.deltaTime;
        }
    }

    void Move()
    {
        if (_movementInput != Vector3.zero)
        {
            Vector3 moveDirection = Vector3.zero;
            if (_movementInput.x != 0 || _movementInput.z != 0)
            {
                moveDirection = transform.TransformDirection(Vector3.forward) * MovementSpeed;
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
            _controller.Move(moveDirection * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(_rotationInput * Time.deltaTime);
           
        }

    }
}