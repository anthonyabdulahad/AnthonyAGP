using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : MonoBehaviour
{
    public LifeManager lifeManager;
    public new Renderer renderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            //renderer.enabled = false;
            lifeManager.ExtraLife();
        }
    }
}
