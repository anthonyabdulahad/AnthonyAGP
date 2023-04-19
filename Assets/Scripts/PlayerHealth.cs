using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public LifeManager lifeManager;
    public Transform respawn;
    MovementController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Respawn()
    {
        lifeManager.LoseLife();
        player.RespawnTo(respawn.position);
        Debug.Log($"Respawn player to {respawn.position}", gameObject);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.tag == "Enemy")
        {
            Debug.Log($"Hit bullet id {hit.collider.GetInstanceID()} at position {hit.collider.transform.position}", hit.collider.gameObject);
            Respawn();
        }
    }
}
