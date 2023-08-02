using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementClock_RevertElementTrigger : MapObjectFSMBaseTrigger
{
    public float timer;
    protected ElementClock elementClock;
    private float time;
    public override void InitTrigger(MapObjectFSM mapObjectFSM, MapObjectFSMBaseState state)
    {
        base.InitTrigger(mapObjectFSM, state);
        elementClock = mapObjectFSM.GetComponentInParent<ElementClock>();
    }
    public override bool IsTriggerReachInFixUpdate(MapObjectFSM mapObjectFSM)
    {
        time += Time.fixedDeltaTime;
        if(time>timer)
        {
            time = 0;
            elementClock.attachedElement = EElement.NONE;
            return true;
        }
        else return false;
    }
}
