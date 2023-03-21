using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float explodeTime = 5f;
    float timer = 0f;

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
        Debug.Log("explode!");
    }

    public void StartCountdown()
    {
        timer = explodeTime;
    }

    public void StopCountdown()
    {
        timer = 0f;
    }
}
