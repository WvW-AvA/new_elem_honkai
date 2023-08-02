using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player_VelocityTrigger : PlayerFSMBaseTrigger
{
    public enum ECheckVelocityType
    {
        Horizental,
        Vertical,
        Both
    }

    public ECheckVelocityType checkVelocityType;
    public float value;
    public bool returnTrueIfLessThenValue;
    public bool isUseDelayVelocity;
    [ShowIf("isUseDelayVelocity")]
    [Range(1, 20)]
    public int vBufferSize = 3;
    private Queue<Vector2> buffer_v;
    private Vector2 last_v;
    public override void InitTrigger(PlayerFSM playerFSM, PlayerFSMBaseState state)
    {
        base.InitTrigger(playerFSM, state);
        buffer_v = new Queue<Vector2>();
        Log.ConsoleLog("Init");
    }
    public override bool IsTriggerReachInFixUpdate(PlayerFSM playerFSM)
    {
        float curr_v = 0;
        if (isUseDelayVelocity == false)
        {
            if (checkVelocityType == ECheckVelocityType.Horizental)
                curr_v = playerFSM.rigidbody.velocity.x;
            else if (checkVelocityType == ECheckVelocityType.Vertical)
                curr_v = playerFSM.rigidbody.velocity.y;
            else if (checkVelocityType == ECheckVelocityType.Both)
                curr_v = playerFSM.rigidbody.velocity.magnitude;
        }
        else
        {
            if (buffer_v.Count > vBufferSize)
            {
                last_v = buffer_v.Dequeue();
            }
            buffer_v.Enqueue(playerFSM.rigidbody.velocity);
            Log.ConsoleLog("last_v:{0} curr_v:{1}", last_v, playerFSM.rigidbody.velocity);
            if (checkVelocityType == ECheckVelocityType.Horizental)
                curr_v = last_v.x;
            else if (checkVelocityType == ECheckVelocityType.Vertical)
                curr_v = last_v.y;
            else if (checkVelocityType == ECheckVelocityType.Both)
                curr_v = last_v.magnitude;

        }
        if (curr_v < value)
            return returnTrueIfLessThenValue;
        else
            return !returnTrueIfLessThenValue;
    }
}
