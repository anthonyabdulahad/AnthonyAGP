using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float explodeTime = 5f;
    public float explodeRadius = 2f;
    float timer = 0f;
    public GameObject Enemyparts;
    public GameObject Kamienemy;

    public ParticleSystem ps;

    private bool startedCountdown = false;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                DoExplosion();
            }
        }
    }

    void DoExplosion()
    {
        ps.Play();
        Debug.Log("EXPLODE");

        // Check how far the player is
        MovementController mController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        Enemyparts.SetActive(true);
        Kamienemy.SetActive(false);

        if (Vector3.Distance(this.transform.position, mController.transform.position) < explodeRadius)
        {

            mController.RespawnTo(GameObject.FindWithTag("Respawn").transform.position);
            FindObjectOfType<LifeManager>().LoseLife();
        }
    }

    public void StartCountdown()
    {
        if (!startedCountdown)
        {
            timer = explodeTime;
            startedCountdown = true;
        }
    }

    public void StopCountdown()
    {
        timer = 0f;
    }
}
