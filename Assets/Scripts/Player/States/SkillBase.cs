using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
public class SkillBase : PlayerFSMBaseState
{
    public string name = "Skill";
    [ShowIf("isShowCDField")]
    public float CD;
    [ReadOnly]
    public TimerNode Timer;
    [HideInInspector]
    public EElement element;
    public bool isEnableChangeState = true;
    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        Timer = TimerManager.Schedule(name, CD, 0, false, TimeoutCallBack, TimerTickCallBack, TimerReloadCallBack);
        element = playerFSM.element;
    }

    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        Timer.Reload();
    }
    protected virtual bool isShowCDField() { return true; }

    public virtual void TimerReloadCallBack()
    {
    }

    public virtual void TimeoutCallBack()
    {

    }
    public virtual void TimerTickCallBack(float duration)
    {

    }
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateInFixUpdate(fsm_Manager);
    }
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateInUpdate(fsm_Manager);
    }
    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnCollisionEnter(fsm_Manager, collision);
    }
    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnCollisionExit(fsm_Manager, collision);
    }
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnCollisionStay(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnTriggerEnter(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnTriggerExit(fsm_Manager, collision);
    }
    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (isEnableChangeState == false)
            return;
        base.TriggerStateOnTriggerStay(fsm_Manager, collision);
    }
}
