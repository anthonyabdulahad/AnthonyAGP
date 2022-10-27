using UnityEngine;
 
public class ExamplePlayerController : MonoBehaviour {
 
    [SerializeField] 
    private GameObject MainCamera; 
 
    private CharacterController _controller;
    private const float MovementSpeed = 10f;
    private const float RotationSpeed = 0.75f;
    private Vector3 _movementInput;
    private Vector3 _rotationInput;
 
    void Start() {
        _controller = GetComponent<CharacterController>();
    }
     
    void Update() {
        ReadInputs();
        Move();
    }
 
    void ReadInputs() {
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
 
    void Move() {
        if (_movementInput != Vector3.zero) {
            _controller.Move(transform.TransformDirection(Vector3.forward) * (MovementSpeed * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(_rotationInput * Time.deltaTime);
        }
    }
} 