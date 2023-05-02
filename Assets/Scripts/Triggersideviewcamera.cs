using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggersideviewcamera : MonoBehaviour
{
    public ExamplePlayerCamera camara;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camara.camerasideview();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camara.camarabackview();
        }
    }
}
