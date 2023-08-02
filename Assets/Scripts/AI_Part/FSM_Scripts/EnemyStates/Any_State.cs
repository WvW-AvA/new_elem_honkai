using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class Any_State : EnemyFSMBaseState
{
    [NonSerialized, OdinSerialize]
    public List<string> exceptStateList = new List<string>();
    protected override bool isHideDefaultAnimation()
    {
        return true;
    }
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;
        base.TriggerStateInFixUpdate(fsm_Manager);

    }
    public override void TriggerState(SubEnemyFSM fsm_Manager)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;
        base.TriggerState(fsm_Manager);
    }
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateInUpdate(fsm_Manager);
    }

    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnCollisionEnter(fsm_Manager, collision);
    }

    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;
        base.TriggerStateOnCollisionExit(fsm_Manager, collision);
    }


    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerStay(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerExit(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerEnter(fsm_Manager, collision);
    }
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnCollisionStay(fsm_Manager, collision);
    }
}

public class Player_AnyState : PlayerFSMBaseState
{
    [NonSerialized, OdinSerialize]
    public List<string> exceptStateList = new List<string>();
    protected override bool isHideDefaultAnimation()
    {
        return true;
    }
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;
        base.TriggerStateInFixUpdate(fsm_Manager);

    }
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateInUpdate(fsm_Manager);
    }

    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnCollisionEnter(fsm_Manager, collision);
    }

    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;
        base.TriggerStateOnCollisionExit(fsm_Manager, collision);
    }


    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerStay(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerExit(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnTriggerEnter(fsm_Manager, collision);
    }
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (exceptStateList.Contains(fsm_Manager.currentStateName))
            return;

        base.TriggerStateOnCollisionStay(fsm_Manager, collision);
    }


}
