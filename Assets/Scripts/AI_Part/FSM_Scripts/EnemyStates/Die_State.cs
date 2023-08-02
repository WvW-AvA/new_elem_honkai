using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_State : EnemyFSMBaseState
{
    public bool isAnimationPlayOverDie = true;
    public float delayTime;
    protected WaitTimeTrigger trigger;
    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);
        trigger = new WaitTimeTrigger();
        trigger.maxTime = delayTime;
    }
    public override void Act_State(EnemyFSM EnemyFSM)
    {
        base.Act_State(EnemyFSM);
        if (isAnimationPlayOverDie)
        {
            if (EnemyFSM.isCurrentAnimationOver())
            {
                if (trigger.IsTriggerReachInUpdate(EnemyFSM))
                {
                    if (EnemyFSM.monster.onMonsterDie != null)
                        EnemyFSM.monster.onMonsterDie.Invoke();
                    ResourceManager.Release(EnemyFSM.gameObject);
                }
            }
        }
        else
        {
            if (trigger.IsTriggerReachInUpdate(EnemyFSM))
            {
                if (EnemyFSM.monster.onMonsterDie != null)
                    EnemyFSM.monster.onMonsterDie.Invoke();
                ResourceManager.Release(EnemyFSM.gameObject);
            }
        }
    }
}

public class BossDie_State : Die_State
{
    public override void EnterState(EnemyFSM EnemyFSM)
    {
        base.EnterState(EnemyFSM);
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
    }
}
