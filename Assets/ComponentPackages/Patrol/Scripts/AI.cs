using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<Behaviour> states = new List<Behaviour>();

    public void ChangeState(Behaviour newState)
    {
        states.ForEach((state) => state.enabled = (state == newState));
    }
}
