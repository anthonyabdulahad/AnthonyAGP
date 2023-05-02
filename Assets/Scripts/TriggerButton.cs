using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    public Behaviour[] behaviours;
    public Animator anim;
    bool state = true;

    private void Start()
    {
        anim.SetBool("Active", state);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter {other.name}", other.gameObject);
        if (other.gameObject.tag == "Player")
        {
            state = !state;
            anim.SetBool("Active", state);
            foreach(var behaviour in behaviours)
            {
                behaviour.enabled = state;
            }
        }
    }


}
