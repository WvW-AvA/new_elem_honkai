using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Player_InTheAirTrigger : PlayerFSMBaseTrigger
{
    public float threshold = 0.05f;
    public override bool IsTriggerReachInFixUpdate(PlayerFSM playerFSM)
    {
        if (Mathf.Abs(playerFSM.rigidbody.velocity.y) >= threshold)
            return true;
        return false;
    }
}
