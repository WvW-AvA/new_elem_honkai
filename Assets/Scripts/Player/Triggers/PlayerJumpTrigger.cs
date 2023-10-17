using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
public class PlayerJumpTrigger : PlayerFSMBaseTrigger
{
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if ((InputManager.Commands[ECommand.Jump].Check()))
            return true;
        return false;
    }
}
