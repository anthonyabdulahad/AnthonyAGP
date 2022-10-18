using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    AnimationAndMovementController player;
    Vector3 previous;

    void Start()
    {
        previous = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            player.SetPlatformMovement(transform.position - previous);
        }

        previous = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player on platform");
            player = other.GetComponent<AnimationAndMovementController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player off platform");
            if (player != null)
            {
                player.SetPlatformMovement(Vector3.zero);
            }
            player = null;
        }
    }


}
