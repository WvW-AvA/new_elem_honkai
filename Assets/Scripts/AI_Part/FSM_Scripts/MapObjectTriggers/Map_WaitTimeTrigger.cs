using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class Map_WaitTimeTrigger : MapObjectFSMBaseTrigger
{
    public float maxTime;
    [ReadOnly]
    public float timer;


    public override bool IsTriggerReachInUpdate(MapObjectFSM mapObjectFSM)
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            timer = 0;
            return true;
        }
        return false;
    }

    public override void InitTrigger(MapObjectFSM mapObjectFSM, MapObjectFSMBaseState state)
    {
        base.InitTrigger(mapObjectFSM, state);
        timer = 0;
    }
}
