using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chasestate;
    public bool canSeeThePlayer;

  public override State RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chasestate;
        }
        else
        {
            return this;
        }
       
    }

}
