using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFallDownTrigger : EnemyFSMBaseTrigger
{
    public float detectVelocityLimit;
    public override bool IsTriggerReachInFixUpdate(EnemyFSM EnemyFSM)
    {
        if (EnemyFSM.rigidbody.gravityScale == 0)
            return false;
        if (EnemyFSM.rigidbody.velocity.y < -detectVelocityLimit)
            return true;
        else
            return false;
    }
}
