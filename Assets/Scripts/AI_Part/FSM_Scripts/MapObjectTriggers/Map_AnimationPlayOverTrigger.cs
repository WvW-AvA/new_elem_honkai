using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_AnimationPlayOverTrigger : MapObjectFSMBaseTrigger
{
    public string checkAnimationName;

    public override void InitTrigger(MapObjectFSM MapObjectFSM, MapObjectFSMBaseState state)
    {
        base.InitTrigger(MapObjectFSM, state);
        if (checkAnimationName == string.Empty || checkAnimationName == null)
        {
            checkAnimationName = state.defaultAnimation.name;
        }
    }
    public override bool IsTriggerReachInUpdate(MapObjectFSM mapObjectFSM)
    {
        if (mapObjectFSM.animacerCurrentPlayingName != checkAnimationName)
            return false;
        if (mapObjectFSM.animancerCurrentPlaying.NormalizedTime >= 0.95)
            return true;
        else
            return false;
    }
}
