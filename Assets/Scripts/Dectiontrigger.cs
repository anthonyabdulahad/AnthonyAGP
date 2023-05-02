using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dectiontrigger : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position , other.transform.position + new Vector3(0f, 1f, 0f), speed * Time.deltaTime);
        }
    }

}
