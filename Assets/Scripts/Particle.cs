using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", 0.3f);
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
