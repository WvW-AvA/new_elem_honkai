using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Player_AndTrigger : PlayerFSMBaseTrigger
{
    public List<PlayerFSMBaseTrigger> andCombination;
    private List<bool> physics_trigger_register;

    private bool physics_update_return()
    {
        bool ret = true;
        foreach (var a in physics_trigger_register)
            ret = ret && a;
        if (ret)
            for (int a = 0; a < andCombination.Count; a++)
                physics_trigger_register[a] = false;
        return physics_trigger_register.Count != 0 && ret;
    }
    public override void InitTrigger(PlayerFSM playerFSM, PlayerFSMBaseState state)
    {
        base.InitTrigger(playerFSM, state);
        foreach (var a in andCombination)
            a.InitTrigger(playerFSM, state);
        physics_trigger_register = new List<bool>();
        for (int i = 0; i < andCombination.Count; i++)
            physics_trigger_register.Add(false);
    }
    public override bool IsTriggerReachInFixUpdate(PlayerFSM playerFSM)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachInFixUpdate(playerFSM))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        bool ret = true;
        foreach (var a in andCombination)
            if (a.IsTriggerReachInUpdate(playerFSM) == false)
                ret = false;
        return andCombination.Count != 0 && ret;
    }
    public override bool IsTriggerReachOnCollisionEnter(PlayerFSM playerFSM, Collision2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnCollisionEnter(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachOnCollisionExit(PlayerFSM playerFSM, Collision2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnCollisionExit(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachOnCollisionStay(PlayerFSM playerFSM, Collision2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnCollisionStay(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachOnTriggerEnter(PlayerFSM playerFSM, Collider2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnTriggerEnter(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachOnTriggerExit(PlayerFSM playerFSM, Collider2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnTriggerExit(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
    public override bool IsTriggerReachOnTriggerStay(PlayerFSM playerFSM, Collider2D collision)
    {
        for (int a = 0; a < andCombination.Count; a++)
        {
            if (andCombination[a].IsTriggerReachOnTriggerStay(playerFSM, collision))
                physics_trigger_register[a] = true;
        }
        return physics_update_return();
    }
}
