using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementDash : MonoBehaviour
{
    MovementController moveScript;

    public bool _isDashPressed;
    public float DashSpeed;
    public float dashTime;
    public float dashDelay = 1f;
    bool dashing;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDashPressed && !dashing)
        {
            dashing = true;
            StartCoroutine(Dash());
            _isDashPressed = false;
        }
    }
    public void OnDash(InputValue context)
    {
        _isDashPressed = context.isPressed;
    }

    IEnumerator Dash()
    {
        
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            moveScript.Dash(DashSpeed);

            yield return null;
        }

        moveScript.Dash(1f);
        yield return new WaitForSeconds(dashDelay);
        dashing = false;
    }


}
