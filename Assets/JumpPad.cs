using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private const float JumpForce = 28f;
    public Vector3 _movementInput;
    public bool candestroy;
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("Onsquish");
            other.SendMessage("DoJump");
            if (candestroy)
            {
                Destroy(gameObject);
            }

        }
    }
}
