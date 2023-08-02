using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animancer;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 构建Player状态机管理器，默认没有添加SO配置功能，
/// 如需要，
/// 首先取消掉下面的注释
/// 然后打开Player_State_SO_Config脚本，取消关于Player_State_SO_Config类的注释即可。
/// 
/// </summary

public class PlayerFSM : FSMManager
{
    public List<string> configInitedList;
    public EElement element;
    public float jumpMaxHight = 0;
    public float horizontalMaxPositiveSpeed = 0;
    public float horizontalMaxNegativeSpeed = 0;
    public float verticalMaxPositiveSpeed = 0;
    public float verticalMaxNegativeSpeed = 0;
    public bool isAllowDash;
    public sealed override void InitManager()
    {
        base.InitManager();
        configInitedList = new List<string>();
    }

    public sealed override void ChangeState(string state)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        if (statesDic.ContainsKey(state))
        {
            //Log.Info(LogColor.PlayerFSM + "Change state from {0} to {1}.", LogColor.Dye(LogColor.EColor.blue, currentStateName),
            //                                                             LogColor.Dye(LogColor.EColor.red, state));
            currentState = statesDic[state];
            currentStateName = state;
            currentState.EnterState(this);
            currentStateType = currentState.GetType().Name;
        }
        else
        {
            Log.Error(LogColor.PlayerFSM + this.gameObject.name + "不存在状态 " + state);
        }
    }

    private void RefreshManager()
    {
        statesDic.Clear();
    }
    public void ConfigWithScriptableObject(Player_FSM_SO_Config config)
    {
        RefreshManager();
        element = config.element;
        bool isFirstInit = false;
        if (configInitedList.Contains(config.name) == false)
        {
            isFirstInit = true;
            configInitedList.Add(config.name);
        }
        defaultStateName = config.defaultStateName;
        var anyStateConfig = config.anyStateConfig;
        var stateConfigs = config.stateConfigs;
        if (anyStateConfig != null)
        {
            anyState = anyStateConfig.stateConfig;
            if (isFirstInit)
                anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState_T tem = stateConfigs[i].stateConfig;
            statesDic.Add(stateConfigs[i].StateName, tem);
            if (isFirstInit)
                tem.InitState(this);
        }
        ChangeState(defaultStateName);
    }
}
