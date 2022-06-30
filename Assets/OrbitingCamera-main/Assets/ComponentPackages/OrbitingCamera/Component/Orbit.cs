using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float distance = 10f;
    public Transform target;
    public float speed = 90.0f;
    public bool invertHorizontal = false;
    public bool invertVertical = false;
    public bool autoLevel = true;
    public float autoLevelLimit = 5.0f;
   

    float horizontal;
    float vertical;

    public void Rotate(float horizontal, float vertical)
    {
        this.horizontal = horizontal * (invertHorizontal? 1.0f : -1.0f);
        this.vertical = vertical * (invertVertical? -1.0f : 1.0f);
    }

    void Update()
    {
        //float distance = Vector3.Distance(transform.position, target.position);
        if (autoLevel)
        {
            float angle = Vector3.Angle(Vector3.up, transform.forward);
            float rotate = vertical * speed * Time.deltaTime;
            if (angle > 90.0f)
            {
                if (angle + rotate > 180.0f - autoLevelLimit)
                {
                    rotate = (180.0f - autoLevelLimit) - angle;
                }
            }
            else
            {
                if (angle + rotate < autoLevelLimit)
                {
                    rotate = autoLevelLimit - angle;
                }
            }
            transform.LookAt(target.position , Vector3.up);
            transform.Rotate(transform.up, horizontal * speed * Time.deltaTime, Space.World);
            transform.Rotate(transform.right, rotate, Space.World);
        }
        else
        {
            transform.Rotate(Vector3.up, horizontal * speed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.right, vertical * speed * Time.deltaTime, Space.Self);
        }
        transform.position = target.position + transform.forward * -distance;
    }
}
