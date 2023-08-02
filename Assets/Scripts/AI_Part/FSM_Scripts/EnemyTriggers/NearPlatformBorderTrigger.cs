using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearPlatformBorderTrigger : EnemyFSMBaseTrigger
{
    public PlatformBorderCheckDir checkDir;
    public bool returnFalseWhenBorder;
    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
    }


    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        Vector2 dir = Vector2.zero;
        switch (checkDir)
        {
            case PlatformBorderCheckDir.speed:
                {
                    dir = enemyFSM.rigidbody.velocity;
                    break;
                }
            case PlatformBorderCheckDir.target:
                {
                    dir = enemyFSM.GetPlayerDirection();
                    break;
                }
        }

        return nearPlatformBoundary(enemyFSM, dir) ? !returnFalseWhenBorder : returnFalseWhenBorder;
    }

    public static bool nearPlatformBoundary(EnemyFSM enemyFSM, Vector2 checkDir)
    {
        //Collider2D collider = GetComponent<Collider2D>();
        float rayToGroundDistance = Mathf.Max(enemyFSM.collider.bounds.size.y - enemyFSM.collider.offset.y, 0);
        rayToGroundDistance += 1f;
        Vector3 frontPoint = enemyFSM.transform.position + new Vector3((checkDir.x > 0 ? 1 : -1), 0, 0) * (enemyFSM.GetComponent<Collider2D>().bounds.size.x * 0.5f);
        var rayHit = Physics2D.Raycast(frontPoint, Vector2.down, 100, 1 << LayerMask.NameToLayer("Ground"));
        //Debug.DrawRay(frontPoint,Vector3.down);
        if (rayHit.distance > rayToGroundDistance)//µ½´ï±ßÔµ
        {
            return true;
        }
        return false;

    }
}

public enum PlatformBorderCheckDir
{
    speed,
    target,

}

