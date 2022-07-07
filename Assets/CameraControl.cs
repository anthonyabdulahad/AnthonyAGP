using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Orbit orbit;
    float idle;
    public AnimationAndMovementController Player;
    public float centertime = 2f;

    void Start()
    {
        orbit = GetComponent<Orbit>();
    }

    void Update()
    {
        float pan = Input.GetAxis("Mouse X");
        float tilt = Input.GetAxis("Mouse Y");

        if (pan ==0 && tilt ==0 && !Player.IsMovementPressed())
        {
            idle += Time.deltaTime;
        }
        else
        {
            idle = 0;

        }
        
        if (idle > centertime)
        {
            orbit.Center();
        }
        else
        {
            orbit.Rotate(pan, tilt);
        }
    }
    
}
