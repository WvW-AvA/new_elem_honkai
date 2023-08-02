using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Sirenix.Serialization;

public class Player_HPTrigger : PlayerFSMBaseTrigger
{
    public int Percent;
    public bool isLower;
    public override void InitTrigger(PlayerFSM playerFSM, PlayerFSMBaseState state)
    {
        base.InitTrigger(playerFSM, state);
    }

    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        base.IsTriggerReachInUpdate(playerFSM);

        float tem = Player.instance.curr.hp * 100 / Player.parameter.maxHP;

        if (tem < Percent)
            return isLower;
        else
            return !isLower;
    }

}
