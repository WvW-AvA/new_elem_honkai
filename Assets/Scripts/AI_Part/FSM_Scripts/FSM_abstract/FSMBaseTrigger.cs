using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
/// <summary>
/// 状态机中对Trigger的配置。可重写InitTrigger，IsTriggerReach方法。
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
[Serializable]
public class FSMBaseTrigger_T
{
    public string targetState = "";

    public virtual void InitTrigger(FSMManager_T fSMManager, FSMBaseState_T state) { }
    #region Update 
    /// <summary>
    /// 是否达到该条件的判断方法,在Update中轮询
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(FSMManager_T fsm_Manager) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,在FixUpdate中轮询
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(FSMManager_T fsm_Manager) { return false; }
    #endregion
    #region collider2D Event
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerEnter时调用。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerStay时调用。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerExit时调用。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderEnter时调用。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderStay时轮询。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision) { return false; }

    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderExit时调用。
    /// </summary>
    /// <param name="fsm_Manager">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision) { return false; }

    #endregion
}

public class FSMBaseTrigger : FSMBaseTrigger_T
{
}

public class PlayerFSMBaseTrigger : FSMBaseTrigger
{
    public PlayerFSMBaseTrigger() { }
    #region Update sealed override

    public sealed override bool IsTriggerReachInUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInUpdate(fsm_Manager as PlayerFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在Update中轮询
    /// </summary>
    /// <param name="playerFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(PlayerFSM playerFSM) { return false; }

    public sealed override bool IsTriggerReachInFixUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInFixUpdate(fsm_Manager as PlayerFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在FixUpdate中轮询
    /// </summary>
    /// <param name="playerFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(PlayerFSM playerFSM) { return false; }
    #endregion
    #region Colider2D sealed override
    public sealed override bool IsTriggerReachOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionEnter(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderEnter时调用。
    /// </summary>
    /// <param name="playerFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(PlayerFSM playerFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionExit(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderExit时调用。
    /// </summary>
    /// <param name="playerFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(PlayerFSM playerFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionStay(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderStay时轮询。
    /// </summary>
    /// <param name="playerFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(PlayerFSM playerFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerEnter(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerEnter时调用。
    /// </summary>
    /// <param name="playerFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(PlayerFSM playerFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerExit(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerExit时调用。
    /// </summary>
    /// <param name="playerFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(PlayerFSM playerFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerStay(fsm_Manager as PlayerFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerStay时调用。
    /// </summary>
    /// <param name="playerFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(PlayerFSM playerFSM, Collider2D collision) { return false; }

    #endregion
    public sealed override void InitTrigger(FSMManager_T fSMManager, FSMBaseState_T state)
    {
        base.InitTrigger(fSMManager, state);
        InitTrigger(fSMManager as PlayerFSM, state as PlayerFSMBaseState);
    }
    public virtual void InitTrigger(PlayerFSM playerFSM, PlayerFSMBaseState state) { }

}


public class EnemyFSMBaseTrigger : FSMBaseTrigger
{
    public EnemyFSMBaseTrigger() { }
    #region Update sealed override

    public sealed override bool IsTriggerReachInUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInUpdate(fsm_Manager as EnemyFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在Update中轮询
    /// </summary>
    /// <param name="enemyFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(EnemyFSM enemyFSM) { return false; }

    public sealed override bool IsTriggerReachInFixUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInFixUpdate(fsm_Manager as EnemyFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在FixUpdate中轮询
    /// </summary>
    /// <param name="enemyFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(EnemyFSM enemyFSM) { return false; }
    #endregion
    #region Colider2D sealed override
    public sealed override bool IsTriggerReachOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionEnter(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderEnter时调用。
    /// </summary>
    /// <param name="enemyFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(EnemyFSM enemyFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionExit(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderExit时调用。
    /// </summary>
    /// <param name="enemyFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(EnemyFSM enemyFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionStay(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderStay时轮询。
    /// </summary>
    /// <param name="enemyFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(EnemyFSM enemyFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerEnter(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerEnter时调用。
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(EnemyFSM enemyFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerExit(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerExit时调用。
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(EnemyFSM enemyFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerStay(fsm_Manager as EnemyFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerStay时调用。
    /// </summary>
    /// <param name="enemyFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(EnemyFSM enemyFSM, Collider2D collision) { return false; }

    #endregion
    public sealed override void InitTrigger(FSMManager_T fSMManager, FSMBaseState_T state)
    {
        base.InitTrigger(fSMManager, state);
        InitTrigger(fSMManager as EnemyFSM, state as EnemyFSMBaseState);
    }
    public virtual void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state) { }

}

public class MapObjectFSMBaseTrigger : FSMBaseTrigger
{
    public MapObjectFSMBaseTrigger() { }
    #region Update sealed override

    public sealed override bool IsTriggerReachInUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInUpdate(fsm_Manager as MapObjectFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在Update中轮询
    /// </summary>
    /// <param name="mapObjectFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInUpdate(MapObjectFSM mapObjectFSM) { return false; }

    public sealed override bool IsTriggerReachInFixUpdate(FSMManager_T fsm_Manager)
    {
        return IsTriggerReachInFixUpdate(fsm_Manager as MapObjectFSM);

    }
    /// <summary>
    /// 是否达到该条件的判断方法,在FixUpdate中轮询
    /// </summary>
    /// <param name="mapObjectFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachInFixUpdate(MapObjectFSM mapObjectFSM) { return false; }
    #endregion
    #region Colider2D sealed override
    public sealed override bool IsTriggerReachOnCollisionEnter(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionEnter(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderEnter时调用。
    /// </summary>
    /// <param name="mapObjectFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionEnter(MapObjectFSM mapObjectFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionExit(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionExit(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderExit时调用。
    /// </summary>
    /// <param name="mapObjectFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionExit(MapObjectFSM mapObjectFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnCollisionStay(FSMManager_T fsm_Manager, Collision2D collision)
    {
        return IsTriggerReachOnCollisionStay(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnColliderStay时轮询。
    /// </summary>
    /// <param name="mapObjectFSM">管理相应状态类的fsm_manager</param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnCollisionStay(MapObjectFSM mapObjectFSM, Collision2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerEnter(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerEnter(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerEnter时调用。
    /// </summary>
    /// <param name="mapObjectFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerEnter(MapObjectFSM mapObjectFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerExit(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerExit(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerExit时调用。
    /// </summary>
    /// <param name="mapObjectFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerExit(MapObjectFSM mapObjectFSM, Collider2D collision) { return false; }

    public sealed override bool IsTriggerReachOnTriggerStay(FSMManager_T fsm_Manager, Collider2D collision)
    {
        return IsTriggerReachOnTriggerStay(fsm_Manager as MapObjectFSM, collision);
    }
    /// <summary>
    /// 是否达到该条件的判断方法,OnTriggerStay时调用。
    /// </summary>
    /// <param name="mapObjectFSM"></param>
    /// <returns></returns>
    public virtual bool IsTriggerReachOnTriggerStay(MapObjectFSM mapObjectFSM, Collider2D collision) { return false; }

    #endregion
    public sealed override void InitTrigger(FSMManager_T fSMManager, FSMBaseState_T state)
    {
        base.InitTrigger(fSMManager, state);
        InitTrigger(fSMManager as MapObjectFSM, state as MapObjectFSMBaseState);
    }
    public virtual void InitTrigger(MapObjectFSM mapObjectFSM, MapObjectFSMBaseState state) { }


}

