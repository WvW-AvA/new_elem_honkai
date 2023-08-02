using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
public class EnemyFSM : FSMManager
{
    [NonSerialized, OdinSerialize]
    public List<EnemyState_SO_Config> stateConfigs;
    [NonSerialized, OdinSerialize]
    public EnemyState_SO_Config anyStateConfig;

    public Monster monster;
    public Collider2D collider;
    public sealed override void InitManager()
    {
        base.InitManager();
        InitWithConfig();
        monster = GetComponent<Monster>();
        collider = GetComponent<Collider2D>();
    }

    private void InitWithConfig()
    {

        anyState = CloneState(anyStateConfig.stateConfig) as EnemyFSMBaseState;
        anyState.InitState(this);
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState_T tem = CloneState(stateConfigs[i].stateConfig) as EnemyFSMBaseState;
            statesDic.Add(stateConfigs[i].StateName, tem);
            tem.InitState(this);
        }
    }

    public override void ChangeState(string state)
    {
        // Log.Info(LogColor.EnemyFSM + "{0} Change state from {1} to {2}.", LogColor.Dye(LogColor.EColor.purple, gameObject.name), LogColor.Dye(LogColor.EColor.blue, currentStateName),
        //                                                            LogColor.Dye(LogColor.EColor.red, state));
        base.ChangeState(state);
    }

    /// <summary>
    ///获得指向玩家位置的vector2(非normalized) 可选同时改变怪物朝向
    /// </summary>
    public Vector2 GetPlayerDirection()
    {
        Vector2 dir = Player.instance.transform.position - transform.position;
        return dir;
    }
    public bool isCurrentAnimationOver()
    {
        if (animancerCurrentPlaying.NormalizedTime >= 0.95)
            return true;
        else
            return false;
    }


}
