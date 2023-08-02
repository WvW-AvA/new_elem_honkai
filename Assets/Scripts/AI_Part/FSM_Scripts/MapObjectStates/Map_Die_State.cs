using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Die_State : MapObjectFSMBaseState
{
    public bool isAnimationPlayOverDie = true;
    public float delayTime;
    protected WaitTimeTrigger trigger;
    public override void EnterState(MapObjectFSM MapObjectFSM)
    {
        base.EnterState(MapObjectFSM);
        MapObjectFSM.collider.enabled = false;
        MapObjectFSM.rigidbody.velocity = Vector2.zero;
        trigger = new WaitTimeTrigger();
        trigger.maxTime = delayTime;
    }
    public override void Act_State(MapObjectFSM MapObjectFSM)
    {
        base.Act_State(MapObjectFSM);
        if (isAnimationPlayOverDie)
        {
            if (MapObjectFSM.isCurrentAnimationOver())
                if (trigger.IsTriggerReachInUpdate(MapObjectFSM))
                    ResourceManager.Release(MapObjectFSM.gameObject);
        }
        else
        {
            if (trigger.IsTriggerReachInUpdate(MapObjectFSM))
                ResourceManager.Release(MapObjectFSM.gameObject);
        }
    }
}
