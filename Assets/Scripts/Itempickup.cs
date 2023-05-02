using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : MonoBehaviour
{
    public new Renderer renderer;

    public enum Type
    {
        Life,
        Collectable
    }

    public Type type = Type.Life;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == Type.Life)
            {
                LifeManager lifeManager = FindObjectOfType<LifeManager>();
                lifeManager.ExtraLife();
            }

            if (type == Type.Collectable)
            {
                CollectableManager collectableManager = FindObjectOfType<CollectableManager>();
                collectableManager.Pickup();
            }

            Destroy(gameObject);
        }
    }
}
