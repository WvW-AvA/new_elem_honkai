using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class SubEnemyFSM : EnemyFSMBaseState
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public EnemyFSMBaseState currentState;
    [ReadOnly]
    public string currentStateName;
    /// <summary>
    /// 任意状态
    /// </summary>
    public EnemyFSMBaseState anyState;
    public string defaultStateName;
    /// <summary>
    /// 当前状态机包含的所以状态列表
    /// </summary>
    public Dictionary<string, EnemyFSMBaseState> statesDic = new Dictionary<string, EnemyFSMBaseState>();


    public List<EnemyState_SO_Config> stateConfigs;
    public EnemyState_SO_Config anyStateConfig;
    /// <summary>
    /// 用于初始化状态机的方法，添加所有状态，及其条件映射表，获取部分组件等。Awake时执行，可不使用基类方法手动编码加载
    /// </summary>
    /// 
    public void InitWithScriptableObject()
    {
        if (anyStateConfig != null)
        {

            anyState = (EnemyFSMBaseState)ObjectClone.CloneObject(anyStateConfig.stateConfig);
            anyState.triggers = new List<FSMBaseTrigger_T>();
            for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger_T);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this.m_enemyFSM, anyState);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this.m_enemyFSM);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            EnemyFSMBaseState tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as EnemyFSMBaseState;
            tem.triggers = new List<FSMBaseTrigger_T>();
            for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger_T);
                tem.triggers[tem.triggers.Count - 1].InitTrigger(this.m_enemyFSM, anyState);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
            }
            statesDic.Add(stateConfigs[i].name, tem);
            tem.InitState(this.m_enemyFSM);
        }
    }



    public override void InitState(EnemyFSM fSM)
    {
        base.InitState(fSM);
        this.m_enemyFSM = fSM;
        statesDic.Clear();
        InitWithScriptableObject();
    }

    public void ChangeState(string state)
    {
        //  Debug.Log(state.ToString()+"  "+gameObject.name);
        if (currentState != null)
            currentState.ExitState(m_enemyFSM);

        if (statesDic.ContainsKey(state))
        {
            currentState = statesDic[state];
            currentStateName = state;
            currentState.EnterState(m_enemyFSM);
        }
        else
        {
            Debug.LogError("敌人状态不存在");
        }

    }
    /// <summary>
    /// 相当于主EnemyFSM里面的Start
    /// </summary>
    /// <param name="fSM_Manager"></param>
    public override void EnterState(EnemyFSM fSM_Manager)
    {
        base.EnterState(fSM_Manager);
        if (statesDic.Count == 0)
            return;
        //默认状态设置
        currentStateName = defaultStateName;
        ChangeState(currentStateName);
        if (anyState != null)
            anyState.EnterState(fSM_Manager);

    }
    /// <summary>
    /// 相当于主EnemyFSM里面的Update
    /// </summary>
    /// <param name="fSM_Manager"></param>
    public override void Act_State(EnemyFSM fSM_Manager)
    {
        this.m_enemyFSM = fSM_Manager;
        base.Act_State(fSM_Manager);
        if (currentState != null)
        {
            //执行状态内容
            currentState.Act_State(fSM_Manager);
            //检测状态条件列表
            currentState.TriggerState(this);
        }
        else
        {
            Debug.LogError("currentState为空");
        }

        if (anyState != null)
        {
            anyState.Act_State(fSM_Manager);
            anyState.TriggerState(this);
        }
    }
    /// <summary>
    /// 子状态机的状态退出
    /// </summary>
    /// <param name="fSM_Manager"></param>
    public override bool ExitState(EnemyFSM fSM_Manager)
    {
        if (currentState != null)
            currentState.ExitState(fSM_Manager);
        return true;
    }

    #region collider event
    public override void OnCollisionEnter2D(EnemyFSM enemyEnemyFSM, Collision2D collision)
    {
        base.OnCollisionEnter2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnCollisionEnter2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnCollisionEnter(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionEnter2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnCollisionEnter(enemyEnemyFSM, collision);
        }
    }

    public override void OnCollisionExit2D(EnemyFSM enemyEnemyFSM, Collision2D collision)
    {
        base.OnCollisionExit2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnCollisionExit2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnCollisionExit(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionExit2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnCollisionExit(enemyEnemyFSM, collision);
        }
    }

    public override void OnCollisionStay2D(EnemyFSM enemyEnemyFSM, Collision2D collision)
    {
        base.OnCollisionStay2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnCollisionStay2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnCollisionStay(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionStay2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnCollisionStay(enemyEnemyFSM, collision);
        }
    }

    public override void OnTriggerEnter2D(EnemyFSM enemyEnemyFSM, Collider2D collision)
    {
        base.OnTriggerEnter2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnTriggerEnter2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnTriggerEnter(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerEnter2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnTriggerEnter(enemyEnemyFSM, collision);
        }
    }

    public override void OnTriggerExit2D(EnemyFSM enemyEnemyFSM, Collider2D collision)
    {
        base.OnTriggerExit2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnTriggerExit2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnTriggerExit(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerExit2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnTriggerExit(enemyEnemyFSM, collision);
        }
    }

    public override void OnTriggerStay2D(EnemyFSM enemyEnemyFSM, Collider2D collision)
    {
        base.OnTriggerStay2D(enemyEnemyFSM, collision);
        if (currentState != null)
        {
            currentState.OnTriggerStay2D(enemyEnemyFSM, collision);
            currentState.TriggerStateOnTriggerStay(enemyEnemyFSM, collision);
        }
        else { Debug.LogError("current State is null.."); }
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerStay2D(enemyEnemyFSM, collision);
            anyState.TriggerStateOnTriggerStay(enemyEnemyFSM, collision);
        }
    }
    #endregion
}

