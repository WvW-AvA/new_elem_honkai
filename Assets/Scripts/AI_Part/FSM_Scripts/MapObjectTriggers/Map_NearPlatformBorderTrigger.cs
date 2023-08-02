using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_NearPlatformBorderTrigger : MapObjectFSMBaseTrigger
{
    public PlatformBorderCheckDir checkDir;
    public bool returnFalseWhenBorder;
    public override void InitTrigger(MapObjectFSM mapObjectFSM, MapObjectFSMBaseState state)
    {
        base.InitTrigger(mapObjectFSM, state);
    }


    public override bool IsTriggerReachInUpdate(MapObjectFSM mapObjectFSM)
    {
        Vector2 dir = Vector2.zero;
        switch (checkDir)
        {
            case PlatformBorderCheckDir.speed:
                {
                    dir = mapObjectFSM.rigidbody.velocity;
                    break;
                }
            case PlatformBorderCheckDir.target:
                {
                    dir = mapObjectFSM.GetPlayerDirection();
                    break;
                }
        }

        return nearPlatformBoundary(mapObjectFSM, dir) ? !returnFalseWhenBorder : returnFalseWhenBorder;
    }

    public static bool nearPlatformBoundary(MapObjectFSM mapObjectFSM, Vector2 checkDir)
    {
        //Collider2D collider = GetComponent<Collider2D>();
        float rayToGroundDistance = Mathf.Max(mapObjectFSM.collider.bounds.size.y - mapObjectFSM.collider.offset.y, 0);
        rayToGroundDistance += 1f;
        Vector3 frontPoint = mapObjectFSM.transform.position + new Vector3((checkDir.x > 0 ? 1 : -1), 0, 0) * (mapObjectFSM.GetComponent<Collider2D>().bounds.size.x * 0.5f);
        var rayHit = Physics2D.Raycast(frontPoint, Vector2.down, 100, 1 << LayerMask.NameToLayer("Ground"));
        //Debug.DrawRay(frontPoint,Vector3.down);
        if (rayHit.distance > rayToGroundDistance)//µ½´ï±ßÔµ
        {
            return true;
        }
        return false;

    }
}

