using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlaform : Platform
{
    public Rigidbody platform;
    public float speed = 5;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        platform.MovePosition(platform.position + new Vector3(0, speed * Time.deltaTime, 0));
    }

    public override void Reverse()
    {
        speed *= -1;
    }

}
