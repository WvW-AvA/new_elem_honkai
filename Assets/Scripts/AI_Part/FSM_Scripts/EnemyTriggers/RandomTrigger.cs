using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomTrigger : EnemyFSMBaseTrigger
{
    public EnemyFSMBaseTrigger insideTrigger;
    public List<string> targetList;

    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        bool tem = insideTrigger.IsTriggerReachInUpdate(enemyFSM);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
        insideTrigger.InitTrigger(enemyFSM, state);
    }
    public override bool IsTriggerReachInFixUpdate(EnemyFSM enemyFSM)
    {
        bool tem = insideTrigger.IsTriggerReachInFixUpdate(enemyFSM);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override bool IsTriggerReachOnCollisionEnter(EnemyFSM enemyFSM, Collision2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnCollisionEnter(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override bool IsTriggerReachOnCollisionExit(EnemyFSM enemyFSM, Collision2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnCollisionExit(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override bool IsTriggerReachOnCollisionStay(EnemyFSM enemyFSM, Collision2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnCollisionStay(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override bool IsTriggerReachOnTriggerEnter(EnemyFSM enemyFSM, Collider2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnTriggerEnter(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
    public override bool IsTriggerReachOnTriggerExit(EnemyFSM enemyFSM, Collider2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnTriggerExit(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }

    public override bool IsTriggerReachOnTriggerStay(EnemyFSM enemyFSM, Collider2D collision)
    {
        bool tem = insideTrigger.IsTriggerReachOnTriggerStay(enemyFSM, collision);
        if (tem)
        {
            targetState = targetList[Random.Range(0, targetList.Count)];
        }
        return tem;
    }
}

