using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map_TouchLayerTrigger : MapObjectFSMBaseTrigger
{
    public bool isDetectTrigger = true;
    public bool isDetectCollison = true;
    public bool isStayDetect = true;
    public LayerMask layer;

    public override bool IsTriggerReachOnCollisionEnter(MapObjectFSM playerFSM, Collision2D collision)
    {
        if (isDetectCollison == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
        {
            return true;
        }
        return false;
    }
    public override bool IsTriggerReachOnTriggerEnter(MapObjectFSM playerFSM, Collider2D collision)
    {
        if (isDetectTrigger == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }
    public override bool IsTriggerReachOnCollisionStay(MapObjectFSM playerFSM, Collision2D collision)
    {
        if (isStayDetect == false || isDetectCollison == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }

    public override bool IsTriggerReachOnTriggerStay(MapObjectFSM playerFSM, Collider2D collision)
    {
        if (isStayDetect == false || isDetectTrigger == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }
}

public class Map_TouchGroundTrigger : Map_TouchLayerTrigger
{
    public float rayThreshold = 1f;

    public override bool IsTriggerReachOnCollisionEnter(MapObjectFSM playerFSM, Collision2D collision)
    {
        if (base.IsTriggerReachOnCollisionEnter(playerFSM, collision))
        {
            var ray = Physics2D.Raycast(playerFSM.transform.position, Vector2.down, 1000000, layer);
            if (ray.distance < rayThreshold && playerFSM.rigidbody.velocity.y <= 0)
                return true;
        }
        return false;
    }
    public override bool IsTriggerReachOnCollisionStay(MapObjectFSM playerFSM, Collision2D collision)
    {
        if (base.IsTriggerReachOnCollisionStay(playerFSM, collision))
        {
            var ray = Physics2D.Raycast(playerFSM.transform.position, Vector2.down, 1000000, layer);
            if (ray.distance < rayThreshold && playerFSM.rigidbody.velocity.y <= 0)
                return true;
        }
        return false;
    }
}

public class Map_NearWallTrigger : MapObjectFSMBaseTrigger
{
    public float rayThreshold = 0.2f;
    public override bool IsTriggerReachInUpdate(MapObjectFSM mapObjectFSM)
    {
        var temp = Physics2D.Raycast(mapObjectFSM.gameObject.transform.position, new Vector2(Mathf.Sign(mapObjectFSM.rigidbody.velocity.x), 0), 100000, LayerMask.GetMask("Ground"));
        if (temp.distance < rayThreshold)
            return true;
        else
            return false;
    }
}

