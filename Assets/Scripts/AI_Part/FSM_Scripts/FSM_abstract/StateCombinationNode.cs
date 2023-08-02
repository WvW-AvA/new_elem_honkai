using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCombinationNode : EnemyFSMBaseState
{
    public enum StateExecuteWay
    {
        Sequence = 0,
        Parallel = 1,
        Random = 2

    }
    //״ִ̬�з�ʽ
    [Tooltip("ע�⣺ͨ��SO�ļ���ӵ�״̬��������trigger list��ʧЧ���ɽڵ��trigger list�ӹܿ���\n" +
        "Sequence:˳��ִ�У�trigger listΪ����State�л�������" +
        "�磺��trigger1Ϊstate1�л�������" +
        "Ҳ����˵���ڵ��˳�������Ϊ���һ��state����Ӧ��trigger������\n" +
        "Parallel���ϸ���ִ�У�Ҫ��֤���е�����״̬�����ڶ�����λ�Ƶĳ�ͻ��trigger list Ϊ�ڵ��˳�����\n" +
        "Random�����ִ�С� trigger listΪ�ڵ��˳�����")]
    public StateExecuteWay executeWay;
    //״̬SO����list
    public List<EnemyState_SO_Config> stateConfigs;

    //״̬ʵ��list
    private List<EnemyFSMBaseState> states;

    private int currSequenceIndex;
    private EnemyFSMBaseState currActingState;
    public override void InitState(EnemyFSM enemyFSM)
    {
        base.InitState(enemyFSM);
        states = new List<EnemyFSMBaseState>();
        InitStateList(enemyFSM);
    }
    public override void EnterState(EnemyFSM enemyFSM)
    {
        base.EnterState(enemyFSM);
        if (executeWay == StateExecuteWay.Sequence)
        {
            SequenceEnter(enemyFSM);
        }
        else if (executeWay == StateExecuteWay.Parallel)
        {
            foreach (var state in states)
            {
                state.EnterState(enemyFSM);
            }
        }
        else if (executeWay == StateExecuteWay.Random)
        {
            RandomEnter(enemyFSM);
        }
    }

    public override void Act_State(EnemyFSM enemyFSM)
    {
        base.Act_State(enemyFSM);
        if (executeWay != StateExecuteWay.Parallel)
            currActingState.Act_State(enemyFSM);
        else
        {
            foreach (var state in states)
            {
                state.Act_State(enemyFSM);
            }
        }
    }

    public override void FixAct_State(EnemyFSM enemyFSM)
    {
        base.FixAct_State(enemyFSM);
        if (executeWay != StateExecuteWay.Parallel)
            currActingState.FixAct_State(enemyFSM);
        else
        {
            foreach (var state in states)
            {
                state.FixAct_State(enemyFSM);
            }
        }
    }

    public override bool ExitState(EnemyFSM enemyFSM)
    {
        bool ret = true;
        if (executeWay != StateExecuteWay.Parallel)
            currActingState.ExitState(enemyFSM);
        else
        {
            foreach (var state in states)
            {
                ret = ret && state.ExitState(enemyFSM);
            }
        }
        return ret;
    }

    private void SequenceChangeState(FSMManager_T fsmManager)
    {
        if (currSequenceIndex < triggers.Count - 1)
        {
            currSequenceIndex++;
            if (currActingState != null)
                currActingState.ExitState(fsmManager);
            currActingState = states[currSequenceIndex];
            //Log.Info(LogColor.EnemyEnemyFSM+"Sequence Turn " + currActingState.defaultAnimationName);
            currActingState.EnterState(fsmManager);
        }
        else if (currSequenceIndex == triggers.Count - 1)
        {
            if (currActingState != null)
                currActingState.ExitState(fsmManager);
            fsmManager.ChangeState(triggers[currSequenceIndex].targetState);
            currSequenceIndex = 0;
        }
    }

    public override void TriggerStateInUpdate(FSMManager_T fsm_Manager)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateInUpdate(fsm_Manager);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachInUpdate(fsm_Manager))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateInFixUpdate(FSMManager_T fsm_Manager)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateInFixUpdate(fsm_Manager);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachInFixUpdate(fsm_Manager))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnCollisionEnter(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnCollisionEnter(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnCollisionExit(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnCollisionExit(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnCollisionStay(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnCollisionStay(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnTriggerEnter(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnTriggerEnter(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnTriggerExit(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnTriggerExit(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }

    public override void TriggerStateOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerStateOnTriggerStay(fsm_Manager, collision);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachOnTriggerStay(fsm_Manager, collision))
            {
                SequenceChangeState(fsm_Manager);
            }
        }
    }


    public override void TriggerState(SubEnemyFSM enemyFSM)
    {
        m_enemyFSM = enemyFSM.m_enemyFSM;
        if (executeWay != StateExecuteWay.Sequence)
            base.TriggerState(enemyFSM);
        else
        {
            if (triggers[currSequenceIndex].IsTriggerReachInUpdate(enemyFSM.m_enemyFSM))
            {
                if (currSequenceIndex < triggers.Count - 1)
                {
                    currSequenceIndex++;
                    Log.Info(LogColor.EnemyFSM + currSequenceIndex);
                    if (currActingState != null)
                        currActingState.ExitState(m_enemyFSM);
                    currActingState = states[currSequenceIndex];
                    currActingState.EnterState(m_enemyFSM);
                }
                else if (currSequenceIndex == triggers.Count - 1)
                {
                    enemyFSM.ChangeState(triggers[currSequenceIndex].targetState);
                    currSequenceIndex = 0;
                    if (currActingState != null)
                        currActingState.ExitState(m_enemyFSM);
                }
            }
        }
    }

    //ֻ������Ϊ��������trigger list��
    private void InitStateList(EnemyFSM enemyFSM)
    {
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            var tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as EnemyFSMBaseState;
            tem.triggers = new List<FSMBaseTrigger_T>();
            states.Add(tem);
            tem.InitState(enemyFSM);
        }
        if (executeWay == StateExecuteWay.Sequence && states.Count > triggers.Count)
        {
            Log.Error(LogColor.EnemyFSM + "Error," + this + "\nstate sequence count error");
        }
    }
    private void SequenceEnter(EnemyFSM enemyFSM)
    {
        currSequenceIndex = 0;
        currActingState = states[currSequenceIndex];
        currActingState.EnterState(enemyFSM);
        //Log.Info(LogColor.EnemyEnemyFSM+"Sequence Enter");
    }
    private void RandomEnter(EnemyFSM enemyFSM)
    {
        int ran = Random.Range(0, states.Count);
        currActingState = states[ran];
        currActingState.EnterState(enemyFSM);
    }


}
