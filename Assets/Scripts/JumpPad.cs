using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private const float JumpForce = 28f;
    public Vector3 _movementInput;
    public bool candestroy;
    public bool checkpoint = false;
    Animator _animator;

    public GameObject itemToDrop;

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

            if (checkpoint)
            {
                GameObject.FindWithTag("Respawn").transform.position = transform.position;
            }

            if (candestroy)
            {
                GameObject inst = Instantiate(itemToDrop, transform.position, Quaternion.identity);
                Debug.Log("INSTANTIATE: " + inst.name);
                Destroy(gameObject);
            }
        }
    }
}
