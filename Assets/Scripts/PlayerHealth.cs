using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float health;
    public Slider slider;
    public Transform respawn;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
        if (health <= 0)
        {
            transform.position = respawn.position;
            health = 100f;
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
