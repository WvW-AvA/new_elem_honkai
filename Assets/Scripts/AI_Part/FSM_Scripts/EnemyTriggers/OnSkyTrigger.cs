using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSkyTrigger : EnemyFSMBaseTrigger
{
    public float hightLimit;
    public override bool IsTriggerReachInFixUpdate(EnemyFSM EnemyFSM)
    {
        var ray = Physics2D.Raycast(EnemyFSM.transform.position, Vector2.down, 200, LayerMask.GetMask("Ground"));
        if (ray.distance >= hightLimit)
            return true;
        else
            return false;
    }
}
public class VerticalVelocityTrigger : EnemyFSMBaseTrigger
{
    public override bool IsTriggerReachInFixUpdate(EnemyFSM EnemyFSM)
    {
        if (Mathf.Abs(EnemyFSM.rigidbody.velocity.y) > 0)
            return true;
        else
            return false;
    }


}
