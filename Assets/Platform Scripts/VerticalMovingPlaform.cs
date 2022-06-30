using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlaform : Platform
{
    public float speed = 5;
    public GameObject Platform;

    // Update is called once per frame
    void Update()
    {
        Platform.transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public override void Reverse()
    {
        speed *= -1;
    }

}
