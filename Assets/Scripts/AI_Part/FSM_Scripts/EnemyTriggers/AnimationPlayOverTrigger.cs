using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayOverTrigger : EnemyFSMBaseTrigger
{
    public string checkAnimationName;

    public override void InitTrigger(EnemyFSM EnemyFSM, EnemyFSMBaseState state)
    {
        base.InitTrigger(EnemyFSM, state);
        if (checkAnimationName == string.Empty || checkAnimationName == null)
        {
            checkAnimationName = state.defaultAnimation.name;
        }
    }
    public override bool IsTriggerReachInUpdate(EnemyFSM enemyFSM)
    {
        if (enemyFSM.animacerCurrentPlayingName != checkAnimationName)
            return false;
        if (enemyFSM.animancerCurrentPlaying.NormalizedTime >= 0.95)
            return true;
        else
            return false;
    }
}
