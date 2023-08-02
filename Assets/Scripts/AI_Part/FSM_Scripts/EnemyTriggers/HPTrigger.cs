using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHPTrigger : EnemyFSMBaseTrigger
{
    public int Percent;
    public bool isLower;
    [HideInInspector]
    public Monster monster;
    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
        monster = enemyFSM.gameObject.GetComponent<Monster>();
    }

    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        base.IsTriggerReachInUpdate(enemyFSM);

        float tem = monster.currentHp * 100 / monster.maxHp;

        if (isLower)
        {
            if (tem < Percent)
                return true;
            else
                return false;
        }
        else
        {
            if (tem < Percent)
                return false;
            else
                return true;
        }
    }
}
