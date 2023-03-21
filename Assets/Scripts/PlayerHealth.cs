using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float health;
    public Slider slider;
    public Transform respawn;
    ExamplePlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<ExamplePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
        if (health <= 0)
        {
            player.RespawnTo(respawn.position);
            health = 100f;
            Debug.Log($"Respawn player to {respawn.position}", gameObject);
        }
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.gameObject.tag == "Enemy")
        {
            health = health - 20f;
            Debug.Log("Hit Player");
        }
    }
}
