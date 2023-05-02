using Cinemachine;
using UnityEngine;

public class ExampleCameraFollowTarget : MonoBehaviour
{

    [SerializeField]
    private Transform PlayerTransform;

    [SerializeField]
    private CinemachineSmoothPath CameraTrack;

    private Vector3 _playerPosition;
    private float _targetHeight;
    private Vector3 _targetPosition;

    private float _closestTrackPoint;
    private Vector3 _currentPathPoint;

    private Quaternion _currentPathRotation;
    private float _currentTrackYRotation;
    private Vector3 _horizontalRotation;

    private Vector3 _dampVelocity;

    private const float DampDuration = 0.15f;
    private const float RotationSpeed = 30f;

    private void Start()
    {
        _dampVelocity = Vector3.zero;
        _targetHeight = PlayerTransform.position.y;
        _currentTrackYRotation = 0f;
    }

    private void LateUpdate()
    {
        GetCameraParameters();
        MoveCameraTarget();
        RotateCameraTarget();
    }

    private void GetCameraParameters()
    {
        //Player Position
        _playerPosition = PlayerTransform.position;

        //Closest Track Point
        _closestTrackPoint = CameraTrack.FindClosestPoint(_playerPosition, 0, -1, 10);

        //Get Track Height
        _currentPathPoint =
            CameraTrack.EvaluatePositionAtUnit(_closestTrackPoint, CinemachinePathBase.PositionUnits.PathUnits);
        _targetHeight = _currentPathPoint.y;

        //Get Rotation
        _currentPathRotation =
            CameraTrack.EvaluateOrientationAtUnit(_closestTrackPoint, CinemachinePathBase.PositionUnits.PathUnits);
        _currentTrackYRotation = _currentPathRotation.eulerAngles.y;
    }

    private void MoveCameraTarget()
    {
        _targetPosition.Set(_currentPathPoint.x, _targetHeight, _currentPathPoint.z);
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _dampVelocity, DampDuration);
    }

    private void RotateCameraTarget()
    {
        _horizontalRotation.Set(0, _currentTrackYRotation, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_horizontalRotation),
            RotationSpeed * Time.deltaTime);
    }

}