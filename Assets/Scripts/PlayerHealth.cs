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
    public Animator animatorfade;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MovementController>();
    }

    // Update is called once per frame
    public void Respawn()
    {
        player._controller.enabled = false;
        animatorfade.SetTrigger("FadeIn");
        Invoke("respawnplayer", 0.8f);
        Debug.Log($"Respawn player to {respawn.name}", gameObject);
    }

    private void respawnplayer()
    {
     player.RespawnTo(respawn.position);
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.tag == "Enemy")
        {
            Debug.Log($"Hit bullet id {hit.collider.GetInstanceID()} at position {hit.collider.transform.position}", hit.collider.gameObject);
            lifeManager.LoseLife();
        }
    }
}
