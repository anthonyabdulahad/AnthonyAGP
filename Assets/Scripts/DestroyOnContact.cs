using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    public ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        //Collider col = collision.gameObject.GetComponent<Collider>();
        //Physics.IgnoreCollision(col, GetComponent<Collider>());
        Destroy(gameObject);
    }
}
