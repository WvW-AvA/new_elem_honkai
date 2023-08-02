using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using XNode;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Reflection;

/// <summary>
/// ScriptableObject状态配置，可参考 State_SO_Config的配置方式
/// </summary>
/// <typeparam name="T3">StateBase</typeparam>
/// <typeparam name="T4">TriggerBase</typeparam>
/// 
public class State_SO_Config_T<T3, T4> : Node where T3 : FSMBaseState_T where T4 : FSMBaseTrigger_T
{
    [Tooltip("状态名称与你的配置文件的名称是同步的，只可复制不能更改哦~")]
    [OnValueChanged("KeepStateName")]
    public string StateName;
    [Header("----------------------State Config Area------------------------")]
    public T3 stateConfig;
    [Header("-----------------------Triggers List Area----------------------")]
    [OnValueChanged("SyncTriggerList")]
    public List<T4> triggerList = new List<T4>();

    private void OnValidate()
    {
        KeepStateName();
        SyncTriggerList();
    }
    public void KeepStateName()
    {
        StateName = this.name;
    }
    public void SyncTriggerList()
    {
        Log.Info(LogColor.Dye(LogColor.EColor.yellow, "Sync Trigger List"));
        if (stateConfig.triggers == null)
            stateConfig.triggers = new List<FSMBaseTrigger_T>();
        stateConfig.triggers.Clear();
        foreach (var tr in triggerList)
            stateConfig.triggers.Add(tr);
    }
}

