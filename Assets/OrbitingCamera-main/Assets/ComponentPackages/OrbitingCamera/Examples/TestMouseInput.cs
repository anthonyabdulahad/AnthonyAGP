using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseInput : MonoBehaviour
{
    Orbit orbit;

    void Start()
    {
        orbit = GetComponent<Orbit>();
    }

    void Update()
    {
        orbit.Rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
