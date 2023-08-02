using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_TouchLayerTrigger : PlayerFSMBaseTrigger
{
    public bool isDetectTrigger = true;
    public bool isDetectCollison = true;
    public bool isStayDetect = true;
    public LayerMask layer;

    public override bool IsTriggerReachOnCollisionEnter(PlayerFSM playerFSM, Collision2D collision)
    {
        if (isDetectCollison == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
        {
            return true;
        }
        return false;
    }
    public override bool IsTriggerReachOnTriggerEnter(PlayerFSM playerFSM, Collider2D collision)
    {
        if (isDetectTrigger == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }
    public override bool IsTriggerReachOnCollisionStay(PlayerFSM playerFSM, Collision2D collision)
    {
        if (isStayDetect == false || isDetectCollison == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }

    public override bool IsTriggerReachOnTriggerStay(PlayerFSM playerFSM, Collider2D collision)
    {
        if (isStayDetect == false || isDetectTrigger == false)
            return false;
        if (((0x01 << collision.gameObject.layer) & layer) != 0)
            return true;
        return false;
    }
}

public class Player_TouchGroundTrigger : Player_TouchLayerTrigger
{
    public float rayThreshold = 1f;

    public override bool IsTriggerReachOnCollisionEnter(PlayerFSM playerFSM, Collision2D collision)
    {
        if (base.IsTriggerReachOnCollisionEnter(playerFSM, collision))
        {
            var ray = Physics2D.Raycast(playerFSM.transform.position, Vector2.down, 1000000, layer);
            if (ray.distance < rayThreshold && playerFSM.rigidbody.velocity.y <= 0)
                return true;
        }
        return false;
    }
    public override bool IsTriggerReachOnCollisionStay(PlayerFSM playerFSM, Collision2D collision)
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

