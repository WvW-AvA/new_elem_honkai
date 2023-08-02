using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerDistanceTrigger : EnemyFSMBaseTrigger
{
    public float checkNearRadius;
    public float checkFarRadius;
    public bool isReturnTrueBetweenNearAndFar = true;
    public bool isRayDetect = false;
    [ShowIf("isRayDetect")]
    public Vector2 startPointOffset;
    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
    }
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        Vector3 v = enemyFSM.GetPlayerDirection();
        if (v.sqrMagnitude > checkNearRadius * checkNearRadius && v.sqrMagnitude < checkFarRadius * checkFarRadius)
        {
            if (isRayDetect)
            {
                var ray = Physics2D.Raycast((Vector2)enemyFSM.transform.position + startPointOffset, v.normalized, 10000, LayerMask.GetMask("Ground", "Wall"));
                Debug.DrawRay((Vector2)enemyFSM.transform.position + startPointOffset, v.normalized * ray.distance, Color.red);
                if (ray.distance * ray.distance >= v.sqrMagnitude)
                    return isReturnTrueBetweenNearAndFar;
                else
                    return !isReturnTrueBetweenNearAndFar;
            }
            else
                return isReturnTrueBetweenNearAndFar;
        }
        else
        {
            return !isReturnTrueBetweenNearAndFar;
        }
    }
}
public class PlayerDirectionTrigger : EnemyFSMBaseTrigger
{
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        var dir = Mathf.Sign(Player.instance.transform.position.x - enemyFSM.transform.position.x);
        if (enemyFSM.curr_x_scale * dir < 0)
            return true;
        else
            return false;
    }
}
