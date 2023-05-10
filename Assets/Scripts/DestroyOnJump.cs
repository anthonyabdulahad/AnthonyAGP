using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnJump : MonoBehaviour
{
    public GameObject mainRobotParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainRobotParent.GetComponent<Explode>().DoExplosionNoDamage();
        }
    }
}
