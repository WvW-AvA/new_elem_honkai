using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Animancer;
/// <summary>
/// 状态机管理器的基类
/// </summary>
/// <typeparam name="T1"></typeparam>  
/// <typeparam name="T2"></typeparam>
public abstract class FSMManager_T : SerializedMonoBehaviour
{
    /// <summary>
    /// 当前状态
    /// </summary>
    protected FSMBaseState_T currentState;
    [ReadOnly]
    public string currentStateName;
    [HideInInspector]
    public string currentStateType;
    /// <summary>
    /// 任意状态
    /// </summary>
    protected FSMBaseState_T anyState;
    public string defaultStateName;
    /// <summary>
    /// 当前状态机包含的所以状态列表
    /// </summary>
    /// 
    [HideInInspector]
    public Dictionary<string, FSMBaseState_T> statesDic = new Dictionary<string, FSMBaseState_T>();

    public virtual void ChangeState(string state)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        if (statesDic.ContainsKey(state))
        {
            currentState = statesDic[state];
            currentStateName = state;
            currentState.EnterState(this);
            currentStateType = currentState.GetType().Name;
        }
        else
        {
            Log.Error(LogColor.BaseFSM + this.gameObject.name + "不存在状态 " + state);
        }
    }

    public virtual void InitWithScriptableObject()
    {
    }
    public virtual void InitManager()
    {
    }

    protected void Awake()
    {
        statesDic.Clear();
        InitManager();
    }

    public virtual void Start()
    {
        if (statesDic.Count == 0)
            return;
        //默认状态设置
        currentStateName = defaultStateName;
        if (anyState != null)
            anyState.EnterState(this);
        ChangeState(currentStateName);

        //// Debug code
        //foreach (var state in statesDic.Values)
        //    foreach (var value in state.triggers)
        //    {
        //        Debug.LogWarning(this + "  " + state + "  " + value + "  " + value.GetHashCode());
        //    }

    }
    protected virtual void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixAct_State(this);
            currentState.TriggerStateInFixUpdate(this);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.FixAct_State(this);
            anyState.TriggerStateInFixUpdate(this);
        }

    }
    #region ColiderEnents
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerEnter2D(this, collision);
            currentState.TriggerStateOnTriggerEnter(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnTriggerEnter2D(this, collision);
            anyState.TriggerStateOnTriggerEnter(this, collision);
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerStay2D(this, collision);
            currentState.TriggerStateOnTriggerStay(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnTriggerStay2D(this, collision);
            anyState.TriggerStateOnTriggerStay(this, collision);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerExit2D(this, collision);
            currentState.TriggerStateOnTriggerExit(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnTriggerExit2D(this, collision);
            anyState.TriggerStateOnTriggerExit(this, collision);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter2D(this, collision);
            currentState.TriggerStateOnCollisionEnter(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnCollisionEnter2D(this, collision);
            anyState.TriggerStateOnCollisionEnter(this, collision);
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionStay2D(this, collision);
            currentState.TriggerStateOnCollisionStay(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnCollisionStay2D(this, collision);
            anyState.TriggerStateOnCollisionStay(this, collision);
        }
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionExit2D(this, collision);
            currentState.TriggerStateOnCollisionExit(this, collision);
        }
        else
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.OnCollisionExit2D(this, collision);
            anyState.TriggerStateOnCollisionExit(this, collision);
        }
    }
    #endregion
    protected virtual void Update()
    {

        if (currentState != null)
        {
            //执行状态内容
            currentState.Act_State(this);
            //检测状态条件列表
            currentState.TriggerStateInUpdate(this);
        }
        else
        {
            Log.Error(LogColor.BaseFSM + "GameObject " + LogColor.Dye(LogColor.EColor.deep_red, "{0}") + " currentState为空", gameObject);
        }

        if (anyState != null)
        {
            // Log.Info(LogColor.EnemyFSM+anyState.triggers.Count);
            anyState.Act_State(this);
            anyState.TriggerStateInUpdate(this);
        }
    }
    protected FSMBaseState_T CloneState(FSMBaseState_T target)
    {
        FSMBaseState_T ret = ObjectClone.CloneObject(target) as FSMBaseState_T;
        ret.triggers = new List<FSMBaseTrigger_T>();
        foreach (var t in target.triggers)
            ret.triggers.Add(ObjectClone.CloneObject(t) as FSMBaseTrigger_T);
        return ret;
    }

}


/// <summary>
///构建 状态机管理器，并为其添加SO配置功能
/// </summary>
public class FSMManager : FSMManager_T
{
    public AnimancerComponent animancer;
    [ReadOnly]
    public AnimancerState animancerCurrentPlaying;
    [ReadOnly]
    public string animacerCurrentPlayingName;

    public enum EFaceMode
    {
        FaceWithSpeed,
        FaceToPlayer,
        NoFaceChange,
        FaceWithADInput
    }
    public EFaceMode FaceMode;
    public float curr_x_scale
    {
        get { return transform.localScale.x; }
        set
        {
            var v = transform.localScale;
            v.x = value;
            transform.localScale = v;
        }
    }
    private Rigidbody2D m_rigidbody;
    public Rigidbody2D rigidbody
    {
        get
        {
            if (m_rigidbody == null)
                m_rigidbody = GetComponent<Rigidbody2D>();
            return m_rigidbody;
        }
    }

    public void AnimationPlay(ClipTransition clip)
    {
        animacerCurrentPlayingName = clip.name;
        animancerCurrentPlaying = animancer.Play(clip);
    }
    public void AnimationPlay(LinearMixerTransition clip)
    {
        animacerCurrentPlayingName = clip.name;
        animancerCurrentPlaying = animancer.Play(clip);
    }

    public override void InitManager()
    {
        base.InitManager();
        animancer = GetComponent<AnimancerComponent>();
    }

    protected override void Update()
    {
        base.Update();

        if (FaceMode == EFaceMode.FaceWithSpeed)
            FaceWithSpeedUpdate();
        else if (FaceMode == EFaceMode.FaceToPlayer)
            FaceToPlayerUpdate();
        else if (FaceMode == EFaceMode.FaceWithADInput)
            FaceWithADInputUpdate();

    }
    private void FaceWithSpeedUpdate()
    {
        float x = rigidbody.velocity.x;
        if (x > 0.1f)
            curr_x_scale = Mathf.Abs(curr_x_scale);
        else if (x < -0.1f)
            curr_x_scale = -Mathf.Abs(curr_x_scale);
    }

    private void FaceToPlayerUpdate()
    {
        float x = Player.instance.transform.position.x - transform.position.x;
        if (x > 0.1f)
            curr_x_scale = Mathf.Abs(curr_x_scale);
        else if (x < -0.1f)
            curr_x_scale = -Mathf.Abs(curr_x_scale);
    }
    private void FaceWithADInputUpdate()
    {
        float x = InputManager.Axises[EAxis.Horizontal].value;
        if (x > 0.1f)
            curr_x_scale = Mathf.Abs(curr_x_scale);
        else if (x < -0.1f)
            curr_x_scale = -Mathf.Abs(curr_x_scale);

    }

}

