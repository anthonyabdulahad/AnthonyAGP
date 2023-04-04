using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private const float JumpForce = 28f;
    public Vector3 _movementInput;
    

   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("DoJump");
        }
    }
}
