using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerJumpTrigger : PlayerFSMBaseTrigger
{
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if ((InputManager.Keys[EKey.SPACE].isKeyDown) && playerFSM.currentStateType != "inTheAir")
            return true;
        return false;
    }
}
