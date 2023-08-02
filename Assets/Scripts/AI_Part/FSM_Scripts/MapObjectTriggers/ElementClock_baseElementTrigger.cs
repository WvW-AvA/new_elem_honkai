using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementClock_baseElementTrigger : MapObjectFSMBaseTrigger
{
    protected ElementClock elementClock;
    public EElement element;
    public override void InitTrigger(MapObjectFSM mapObjectFSM, MapObjectFSMBaseState state)
    {
        base.InitTrigger(mapObjectFSM, state);
        elementClock = mapObjectFSM.GetComponentInParent<ElementClock>();
    }
    public override bool IsTriggerReachInFixUpdate(MapObjectFSM mapObjectFSM)
    {
        return IsTargetDamage(mapObjectFSM);
    }

    public bool IsTargetDamage(MapObjectFSM mapObjectFSM)
    {
        if (elementClock.attachedElement == element)
        {
            elementClock.attachedElement = EElement.NONE;
            return true;
        }
        else return false;
    }
}
