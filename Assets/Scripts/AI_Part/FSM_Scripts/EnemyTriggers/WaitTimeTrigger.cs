using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public class WaitTimeTrigger : EnemyFSMBaseTrigger
{
    public float maxTime;
    [ReadOnly]
    public float timer;


    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            timer = 0;
            return true;
        }
        return false;
    }

    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
        timer = 0;
    }
}

public class StrategyWaitTimeTrigger : EnemyFSMBaseTrigger
{

    public class WaitTimeStrategy
    {
        public bool isAddRandomOffset;
        [ShowIf("isAddRandomOffset")]
        public float waitTimeMaxOffset;
        public float waitTime;

        public float GetWaitTime()
        {
            float ret = waitTime;
            if (isAddRandomOffset)
                ret += Random.Range(-1, 1) * waitTimeMaxOffset;
            return ret;
        }
    }

    public List<WaitTimeStrategy> waitTimeStrategies;
    private float currTime;
    private float timer;
    private int index = 0;
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        timer += Time.deltaTime;
        if (timer > currTime)
        {
            timer = 0;
            index++;
            if (index >= waitTimeStrategies.Count)
                index = 0;
            currTime = waitTimeStrategies[index].GetWaitTime();
            return true;
        }
        return false;
    }

    public override void InitTrigger(EnemyFSM enemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(enemyFSM, state);
        timer = 0;
        currTime = waitTimeStrategies[0].GetWaitTime();
    }
}
