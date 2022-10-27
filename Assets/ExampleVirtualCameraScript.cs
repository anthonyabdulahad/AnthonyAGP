using UnityEngine;

public class ExampleVirtualCameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform CameraFollowTargetTransform;

    private Vector3 _eulerAngles;

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        _eulerAngles = transform.rotation.eulerAngles;
        _eulerAngles.y = CameraFollowTargetTransform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(_eulerAngles);
    }
}