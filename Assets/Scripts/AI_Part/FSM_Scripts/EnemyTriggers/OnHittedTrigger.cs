using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OnHittedTrigger : EnemyFSMBaseTrigger
{
    [ReadOnly]
    public bool isHitted = false;

    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
    }

    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        if (isHitted)
        {
            isHitted = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
