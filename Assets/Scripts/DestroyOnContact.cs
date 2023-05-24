using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    public ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
