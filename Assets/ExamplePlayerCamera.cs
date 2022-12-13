using System;
using Cinemachine;
using UnityEngine;

public class ExamplePlayerCamera : MonoBehaviour
{

    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private CinemachineVirtualCamera CameraForward;
    [SerializeField]
    private CinemachineVirtualCamera CameraBackwards;

    internal void camerasideview()
    {
        _orientation = 0;
        CameraForward.Priority = 1;
        CameraBackwards.Priority = 2;
        CameraSideview.Priority = 3;
        Debug.Log("Sideview camera");
    }

    internal void camarabackview()
    {
        _orientation = -1;
        CameraForward.Priority = 2;
        CameraBackwards.Priority = 3;
        CameraSideview.Priority = 1;
        Debug.Log("backward camera");
    }

    internal void camarafrontview()
    {
        _orientation = 1;
        CameraForward.Priority = 3;
        CameraBackwards.Priority = 2;
        CameraSideview.Priority = 1;
        Debug.Log("forward camera");
    }


    [SerializeField]
    private CinemachineVirtualCamera CameraSideview;

    private Vector3 _playerForward;
    private int _orientation; //-1 backwards, 0 unknown, 1 forward

    private void Start()
    {
        _orientation = 1;
    }

    private void Update()
    {
        //Update the current player forward (where the player is looking)
        _playerForward = transform.forward;
        _playerForward.Set(_playerForward.x, 0f, _playerForward.z);


        
        if (_orientation != 0)
        {
            if (_orientation != 1 && IsLookingForward())
            {
                camarafrontview();
               
            }
            else if (_orientation != -1 && IsLookingBackwards())
            {
                camarabackview();
                
            }
        }
    }

    private bool IsLookingForward()
    {
        return (Vector3.Dot(_playerForward, MainCamera.transform.forward) > 0.1f);
    }

    private bool IsLookingBackwards()
    {
        return (Vector3.Dot(_playerForward, MainCamera.transform.forward) <= -0.1f);
    }

    private bool IsLookingRight()
    {
        return (Vector3.Dot(_playerForward, MainCamera.transform.right) <= -0.1f);
    }

}