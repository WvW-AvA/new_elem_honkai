using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
public class Player_AnimationPlayOverTrigger : PlayerFSMBaseTrigger
{
    public bool isCheckTargetAnimation = false;
    [ShowIf("isCheckTargetAnimation")]
    public string checkAnimationName;
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if ((isCheckTargetAnimation == false || playerFSM.animacerCurrentPlayingName == checkAnimationName) && playerFSM.animancerCurrentPlaying.NormalizedTime >= 0.95)
            return true;
        else
            return false;
    }
}
