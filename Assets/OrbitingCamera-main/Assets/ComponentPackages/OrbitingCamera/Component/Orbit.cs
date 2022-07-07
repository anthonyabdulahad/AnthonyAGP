using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float distance = 10f;
    public Transform target;
    public float speed = 90.0f;
    public float smooth = 0.5f;
    public bool invertHorizontal = false;
    public bool invertVertical = false;
    public float minPitch = 0.0f;  // down to -90°
    public float maxPitch = 80.0f; // up to 90°
    public Vector3 offset = Vector3.up;
    float pitch = 0.0f;
    float yaw = 0.0f;
    float horizontal;
    float vertical;
    float pitchVelocity;
    float yawVelocity;

    public void Center()
    {
        pitch = Mathf.SmoothDampAngle(pitch, 0.0f, ref pitchVelocity, smooth);
        yaw = Mathf.SmoothDampAngle(yaw, Vector3.SignedAngle(Vector3.forward, target.forward, Vector3.up), ref yawVelocity, smooth); ;
    }

    public void Rotate(float horizontal, float vertical)
    {
        this.horizontal = horizontal * (invertHorizontal? 1.0f : -1.0f);
        this.vertical = vertical * (invertVertical? -1.0f : 1.0f);
    }

    void Update()
    {
        Vector3 viewpoint = target.position + offset;
        yaw += horizontal * speed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch + vertical * speed * Time.deltaTime, minPitch, maxPitch);
        transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
        transform.position = viewpoint + transform.forward * -distance;
    }
}
