using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public Slider slider;
    public Patrol patrol;
    public AIShooting shooting;
    public Vision vision;
    public Chase chase;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }

    internal void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {

            patrol.enabled = false;
            vision.enabled = false;
            chase.enabled = false;
            shooting.enabled = false;

            Debug.Log("Stop");


        }
    }
    /* private void OnCollisionEnter(Collision hit)
{
    if (hit.collider.gameObject.tag == "Enemy")
    {
        health = health - 20f;
        Debug.Log("Hit Enemy");

        if (health <= 0.0f)
        {
            patrol.enabled = false;
            shooting.enabled = false;
            vision.enabled = false;
            chase.enabled = false;


        }
    }
}*/

}
