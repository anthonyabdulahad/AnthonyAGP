using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingPlatform : MonoBehaviour
{
    public float fallingDelayInSeconds;
    private Rigidbody rb;
    public float platformDestoryDelayInSeconds;
    public float fallSpeed;
    public UnityEvent whenFalling;
    public UnityEvent onPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Fall", fallingDelayInSeconds);
            onPlatform.Invoke();

        }
    }

    void Fall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(0, -1.0f, 0) *fallSpeed, ForceMode.Impulse);

        Invoke("Falling", platformDestoryDelayInSeconds);
    }

    void Falling()
    {
       
        whenFalling.Invoke();

    }

    public void DestroyPlatform()
    {
        Destroy(gameObject);
    }


}
