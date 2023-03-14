using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitYellow;
    [SerializeField] private Transform vfxHitRed;


    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 10f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            Instantiate(vfxHitYellow, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, 1f);
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) ;
        {

        }
    } 

}
