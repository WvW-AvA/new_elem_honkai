using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWallTrigger : EnemyFSMBaseTrigger
{

    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);

    }
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        return hitWall(enemyFSM);
    }

    public static bool hitWall(EnemyFSM enemyFSM)
    {

        Vector3 frontPoint = enemyFSM.transform.position + new Vector3((enemyFSM.rigidbody.velocity.x > 0 ? 1 : -1), 0, 0) * (enemyFSM.GetComponent<Collider2D>().bounds.size.x * 0.5f);
        Vector3 upPoint = enemyFSM.transform.position + new Vector3(0, 0.1f, 0);
        if (Physics2D.OverlapArea(upPoint, frontPoint, LayerMask.GetMask("Ground")) != null)
        {
            return true;
        }
        return false;
    }

}

public class WallDistanceTrigger : EnemyFSMBaseTrigger
{
    public float distanceThreshold = 10;
    public bool isReturnTrueIfInsideThrehold = true;
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        var ray = Physics2D.Raycast(enemyFSM.transform.position, new Vector2(Mathf.Sign(enemyFSM.curr_x_scale), 0), 10000, LayerMask.GetMask("Ground", "Wall"));
        if (ray.distance < distanceThreshold)
            return isReturnTrueIfInsideThrehold;
        else
            return !isReturnTrueIfInsideThrehold;
    }
}
