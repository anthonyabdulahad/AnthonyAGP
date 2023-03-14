using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _animator.SetBool("Playeropen", true);
    }

}
