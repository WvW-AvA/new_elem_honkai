using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenEndTrigger : EnemyFSMBaseTrigger
{
    public override bool IsTriggerReachInFixUpdate(EnemyFSM EnemyFSM)
    {
        if (DOTween.IsTweening(EnemyFSM.gameObject))
            return false;
        else
            return true;
    }
}
