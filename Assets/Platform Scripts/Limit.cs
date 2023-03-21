using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public Platform platform;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{name} collides with {other.name}", gameObject);
        if (other.tag =="Platform")
        {
           //Debug.Log("Reverse");
            platform.Reverse();
        }
    }
}
