using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
/// <summary>
/// ״̬���ж�״̬�ĳ���,�����÷��ɲο� ״̬���Ĺ���ģʽ��
/// </summary>
/// <typeparam name="T1">������ö�����ͣ�����ΪStateö�١�</typeparam>
/// <typeparam name="T2">������ö�����ͣ�����ΪTriggerö�١�</typeparam>
[Serializable]
public class FSMBaseState_T
{
    [NonSerialized, OdinSerialize, HideInInspector]
    public List<FSMBaseTrigger_T> triggers = new List<FSMBaseTrigger_T>();
    /// <summary>
    /// ״̬��ʼ��
    /// </summary>
    public virtual void InitState(FSMManager_T fSMManager) { }

    /// <summary>
    /// ����״̬ʱ����
    /// </summary>
    public virtual void EnterState(FSMManager_T fSM_Manager) { }

    /// <summary>
    /// �˳�״̬ʱ����,����ֵ��ʾ�Ƿ��˳����
    /// </summary>
    public virtual bool ExitState(FSMManager_T fSM_Manager) => true;

    /// <summary>
    /// ״̬������ˢ��
    /// </summary>
    public virtual void Act_State(FSMManager_T fSM_Manager) { }
    /// <summary>
    /// Acting in fixUpdate 
    /// </summary>
    public virtual void FixAct_State(FSMManager_T fSM_Manager) { }

    #region Colider Event
    /// <summary>
    /// invoke when TriggerEnter2D
    /// </summary>
    /// 
    public virtual void OnTriggerEnter2D(FSMManager_T fSM_Manager, Collider2D collision) { }
    public virtual void OnTriggerStay2D(FSMManager_T fSM_Manager, Collider2D collision) { }
    public virtual void OnTriggerExit2D(FSMManager_T fSM_Manager, Collider2D collision) { }

    /// <summary>
    /// invoke when ColiderEnter2D
    /// </summary>
    public virtual void OnCollisionEnter2D(FSMManager_T fSM_Manager, Collision2D collision) { }
    public virtual void OnCollisionExit2D(FSMManager_T fSM_Manager, Collision2D collision) { }
    public virtual void OnCollisionStay2D(FSMManager_T fSM_Manager, Collision2D collision) { }
    #endregion

    #region TriggerInvoke
    /// <summary>
    /// ��Update�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInUpdate(fsm_Manager))
            {
                if (triggers[i].targetState == fsm_Manager.currentStateName)
                    continue;
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��FixUpdate�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInFixUpdate(fsm_Manager))
            {
                if (triggers[i].targetState == fsm_Manager.currentStateName)
                    continue;
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionEnter(fsm_Manager, collision))
            {
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerEnter(fsm_Manager, collision))
            {
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public virtual void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    #endregion
}


public class FSMBaseState : FSMBaseState_T
{

    public UnityEvent onStateEnterEvents;

    protected virtual bool isHideDefaultAnimation() { return false; }
    [HideIf("isHideDefaultAnimation")]
    public ClipTransition defaultAnimation;
    [HideIf("isHideDefaultAnimation")]
    public AudioClip defaultAnimationSoundEffect;
}




public class PlayerFSMBaseState : FSMBaseState
{

    [NonSerialized]
    public PlayerFSM fsmManager;

    //��һЩ�������ж��η�װ
    //////////////////////////////////////////////////////////////////////////////////////////
    public sealed override void InitState(FSMManager_T fSMManager)
    {
        base.InitState(fSMManager);
        fsmManager = fSMManager as PlayerFSM;
        foreach (var t in triggers)
            t.InitTrigger(fsmManager, this);
        InitState(fSMManager as PlayerFSM);
    }
    public virtual void InitState(PlayerFSM playerFSM) { }
    public sealed override void EnterState(FSMManager_T fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        if (onStateEnterEvents != null)
            onStateEnterEvents.Invoke();
        EnterState(fSM_Manager as PlayerFSM);
    }
    public virtual void EnterState(PlayerFSM playerFSM)
    {
        if (defaultAnimation != null)
        {
            playerFSM.AnimationPlay(defaultAnimation);
            AudioManager.PlayOneShot(defaultAnimationSoundEffect, playerFSM.transform.position);
        }
    }
    public sealed override void Act_State(FSMManager_T fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        Act_State(fSM_Manager as PlayerFSM);
    }
    public virtual void Act_State(PlayerFSM playerFSM) { }

    public sealed override void FixAct_State(FSMManager_T fSM_Manager)
    {
        base.FixAct_State(fSM_Manager);
        FixAct_State(fSM_Manager as PlayerFSM);
    }
    public virtual void FixAct_State(PlayerFSM playerFSM) { }
    #region Colider Events
    public sealed override void OnCollisionStay2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionStay2D(fSM_Manager, collision);
        OnCollisionStay2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnCollisionStay2D(PlayerFSM playerFSM, Collision2D collision) { }

    public sealed override void OnCollisionEnter2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionEnter2D(fSM_Manager, collision);
        OnCollisionEnter2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnCollisionEnter2D(PlayerFSM playerFSM, Collision2D collision) { }

    public sealed override void OnCollisionExit2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionExit2D(fSM_Manager, collision);
        OnCollisionExit2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnCollisionExit2D(PlayerFSM playerFSM, Collision2D collision) { }

    public sealed override void OnTriggerEnter2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerEnter2D(fSM_Manager, collision);
        OnTriggerEnter2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnTriggerEnter2D(PlayerFSM playerFSM, Collider2D collision) { }

    public sealed override void OnTriggerExit2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerExit2D(fSM_Manager, collision);
        OnTriggerExit2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnTriggerExit2D(PlayerFSM playerFSM, Collider2D collision) { }

    public sealed override void OnTriggerStay2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerStay2D(fSM_Manager, collision);
        OnTriggerStay2D(fSM_Manager as PlayerFSM, collision);
    }
    public virtual void OnTriggerStay2D(PlayerFSM playerFSM, Collider2D collision) { }
    #endregion
    public sealed override bool ExitState(FSMManager_T fSM_Manager)
    {

        return ExitState(fSM_Manager as PlayerFSM);
    }
    public virtual bool ExitState(PlayerFSM playerFSM) { return true; }
    #region TriggerInvoke
    /// <summary>
    /// ��Update�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInUpdate(fsm_Manager))
            {
                //if (triggers[i].targetState == fsm_Manager.currentStateName)
                //  continue;
                //Log.Info(LogColor.PlayerFSM+triggers[i] + "     " + triggers[i].targetState);

                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);

                break;
            }
        }
    }
    /// <summary>
    /// ��FixUpdate�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInFixUpdate(fsm_Manager))
            {
                //  if (triggers[i].targetState == fsm_Manager.currentStateName)
                //    continue;
                //Log.Info(LogColor.PlayerFSM+triggers[i] + "     " + triggers[i].targetState);
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionEnter(fsm_Manager, collision))
            {
                //Log.Info(LogColor.PlayerFSM+triggers[i] + "     " + triggers[i].targetState);
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.PlayerFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.PlayerFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerEnter(fsm_Manager, collision))
            {
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.PlayerFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.PlayerFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((PlayerFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    #endregion

}

public class EnemyFSMBaseState : FSMBaseState
{
    [NonSerialized]
    public EnemyFSM m_enemyFSM;

    public EnemyFSM.EFaceMode faceMode = FSMManager.EFaceMode.FaceWithSpeed;
    //��һЩ�������ж��η�װ
    //////////////////////////////////////////////////////////////////////////////////////////
    public sealed override void InitState(FSMManager_T fSMManager)
    {
        base.InitState(fSMManager);
        m_enemyFSM = fSMManager as EnemyFSM;
        foreach (var t in triggers)
            t.InitTrigger(m_enemyFSM, this);
        InitState(fSMManager as EnemyFSM);
    }
    public virtual void InitState(EnemyFSM enemyFSM) { }
    public sealed override void EnterState(FSMManager_T fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        if (onStateEnterEvents != null)
            onStateEnterEvents.Invoke();
        EnterState(fSM_Manager as EnemyFSM);
    }
    public virtual void EnterState(EnemyFSM enemyFSM)
    {
        enemyFSM.FaceMode = faceMode;
        if (defaultAnimation != null)
        {
            enemyFSM.AnimationPlay(defaultAnimation);
            AudioManager.PlayOneShot(defaultAnimationSoundEffect, enemyFSM.transform.position);
        }
    }
    public sealed override void Act_State(FSMManager_T fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        Act_State(fSM_Manager as EnemyFSM);
    }
    public virtual void Act_State(EnemyFSM enemyFSM) { }

    public sealed override void FixAct_State(FSMManager_T fSM_Manager)
    {
        base.FixAct_State(fSM_Manager);
        FixAct_State(fSM_Manager as EnemyFSM);
    }
    public virtual void FixAct_State(EnemyFSM enemyFSM) { }
    #region Colider Events
    public sealed override void OnCollisionStay2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionStay2D(fSM_Manager, collision);
        OnCollisionStay2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnCollisionStay2D(EnemyFSM enemyFSM, Collision2D collision) { }

    public sealed override void OnCollisionEnter2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionEnter2D(fSM_Manager, collision);
        OnCollisionEnter2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnCollisionEnter2D(EnemyFSM enemyFSM, Collision2D collision) { }

    public sealed override void OnCollisionExit2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionExit2D(fSM_Manager, collision);
        OnCollisionExit2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnCollisionExit2D(EnemyFSM enemyFSM, Collision2D collision) { }

    public sealed override void OnTriggerEnter2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerEnter2D(fSM_Manager, collision);
        OnTriggerEnter2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnTriggerEnter2D(EnemyFSM enemyFSM, Collider2D collision) { }

    public sealed override void OnTriggerExit2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerExit2D(fSM_Manager, collision);
        OnTriggerExit2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnTriggerExit2D(EnemyFSM enemyFSM, Collider2D collision) { }

    public sealed override void OnTriggerStay2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerStay2D(fSM_Manager, collision);
        OnTriggerStay2D(fSM_Manager as EnemyFSM, collision);
    }
    public virtual void OnTriggerStay2D(EnemyFSM enemyFSM, Collider2D collision) { }
    #endregion
    public sealed override bool ExitState(FSMManager_T fSM_Manager)
    {

        return ExitState(fSM_Manager as EnemyFSM);
    }
    public virtual bool ExitState(EnemyFSM enemyFSM) { return true; }
    #region TriggerInvoke
    /// <summary>
    /// ��Update�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInUpdate(fsm_Manager))
            {
                //if (triggers[i].targetState == fsm_Manager.currentStateName)
                //  continue;
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);

                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);

                break;
            }
        }
    }
    /// <summary>
    /// ��FixUpdate�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInFixUpdate(fsm_Manager))
            {
                //  if (triggers[i].targetState == fsm_Manager.currentStateName)
                //    continue;
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionEnter(fsm_Manager, collision))
            {
                //Log.Info(LogColor.EnemyFSM+triggers[i] + "     " + triggers[i].targetState);
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerEnter(fsm_Manager, collision))
            {
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.EnemyFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((EnemyFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    #endregion


    public virtual void TriggerState(SubEnemyFSM fsm_Manager)
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInUpdate(fsm_Manager.m_enemyFSM))
            {
                Debug.Log(triggers[i] + "     " + triggers[i].targetState);
                fsm_Manager.ChangeState(triggers[i].targetState);
                break;
            }
        }
    }


}

public class MapObjectFSMBaseState : FSMBaseState
{
    [NonSerialized]
    public MapObjectFSM m_mapObjectFSM;

    public MapObjectFSM.EFaceMode faceMode = FSMManager.EFaceMode.FaceWithSpeed;
    //��һЩ�������ж��η�װ
    //////////////////////////////////////////////////////////////////////////////////////////
    public sealed override void InitState(FSMManager_T fSMManager)
    {
        base.InitState(fSMManager);
        m_mapObjectFSM = fSMManager as MapObjectFSM;
        foreach (var t in triggers)
            t.InitTrigger(m_mapObjectFSM, this);
        InitState(fSMManager as MapObjectFSM);
    }
    public virtual void InitState(MapObjectFSM mapObjectFSM) { }
    public sealed override void EnterState(FSMManager_T fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        if (onStateEnterEvents != null)
            onStateEnterEvents.Invoke();
        EnterState(fSM_Manager as MapObjectFSM);
    }
    public virtual void EnterState(MapObjectFSM mapObjectFSM)
    {
        mapObjectFSM.FaceMode = faceMode;
        if (defaultAnimation != null)
        {
            mapObjectFSM.AnimationPlay(defaultAnimation);
            AudioManager.PlayOneShot(defaultAnimationSoundEffect, mapObjectFSM.transform.position);
        }
    }
    public sealed override void Act_State(FSMManager_T fSM_Manager)
    {
        base.Act_State(fSM_Manager);
        Act_State(fSM_Manager as MapObjectFSM);
    }
    public virtual void Act_State(MapObjectFSM mapObjectFSM) { }

    public sealed override void FixAct_State(FSMManager_T fSM_Manager)
    {
        base.FixAct_State(fSM_Manager);
        FixAct_State(fSM_Manager as MapObjectFSM);
    }
    public virtual void FixAct_State(MapObjectFSM mapObjectFSM) { }
    #region Colider Events
    public sealed override void OnCollisionStay2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionStay2D(fSM_Manager, collision);
        OnCollisionStay2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnCollisionStay2D(MapObjectFSM mapObjectFSM, Collision2D collision) { }

    public sealed override void OnCollisionEnter2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionEnter2D(fSM_Manager, collision);
        OnCollisionEnter2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnCollisionEnter2D(MapObjectFSM mapObjectFSM, Collision2D collision) { }

    public sealed override void OnCollisionExit2D(FSMManager_T fSM_Manager, Collision2D collision)
    {
        base.OnCollisionExit2D(fSM_Manager, collision);
        OnCollisionExit2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnCollisionExit2D(MapObjectFSM mapObjectFSM, Collision2D collision) { }

    public sealed override void OnTriggerEnter2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerEnter2D(fSM_Manager, collision);
        OnTriggerEnter2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnTriggerEnter2D(MapObjectFSM mapObjectFSM, Collider2D collision) { }

    public sealed override void OnTriggerExit2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerExit2D(fSM_Manager, collision);
        OnTriggerExit2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnTriggerExit2D(MapObjectFSM mapObjectFSM, Collider2D collision) { }

    public sealed override void OnTriggerStay2D(FSMManager_T fSM_Manager, Collider2D collision)
    {
        base.OnTriggerStay2D(fSM_Manager, collision);
        OnTriggerStay2D(fSM_Manager as MapObjectFSM, collision);
    }
    public virtual void OnTriggerStay2D(MapObjectFSM mapObjectFSM, Collider2D collision) { }
    #endregion
    public sealed override bool ExitState(FSMManager_T fSM_Manager)
    {

        return ExitState(fSM_Manager as MapObjectFSM);
    }
    public virtual bool ExitState(MapObjectFSM mapObjectFSM) { return true; }
    #region TriggerInvoke
    /// <summary>
    /// ��Update�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInUpdate(fsm_Manager))
            {
                //if (triggers[i].targetState == fsm_Manager.currentStateName)
                //  continue;
                //Log.Info(LogColor.MapObjectFSM+triggers[i] + "     " + triggers[i].targetState);

                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);

                break;
            }
        }
    }
    /// <summary>
    /// ��FixUpdate�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachInFixUpdate(fsm_Manager))
            {
                //  if (triggers[i].targetState == fsm_Manager.currentStateName)
                //    continue;
                //Log.Info(LogColor.MapObjectFSM+triggers[i] + "     " + triggers[i].targetState);
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionEnter(fsm_Manager, collision))
            {
                //Log.Info(LogColor.MapObjectFSM+triggers[i] + "     " + triggers[i].targetState);
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.MapObjectFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnColliderStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnCollisionStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.MapObjectFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerEnter�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerEnter(fsm_Manager, collision))
            {
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerExit�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerExit(fsm_Manager, collision))
            {
                Log.Info(LogColor.MapObjectFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    /// <summary>
    /// ��OnTriggerStay�б���Trigger����ת�����������Ķ�Ӧtrigger��ָ���״̬��
    /// </summary>
    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {

        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i].IsTriggerReachOnTriggerStay(fsm_Manager, collision))
            {
                Log.Info(LogColor.MapObjectFSM + triggers[i] + "     " + triggers[i].targetState + "  ");
                ((MapObjectFSM)fsm_Manager).ChangeState(triggers[i].targetState);
                break;
            }
        }
    }
    #endregion



}