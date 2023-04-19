using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathItem : MonoBehaviour
{
    public LifeManager lifeManager;
    public new Renderer renderer;
    public Transform respawn;
    public bool destroyonhit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            lifeManager.LoseLife();
            other.GetComponent<MovementController>().RespawnTo(respawn.position);

            if (destroyonhit)
            {
                Destroy(gameObject);
            }
            
        }


    }
}
