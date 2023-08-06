using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
public enum EKeyTriggerMode
{
    Down,
    Pressing,
    Up,
    LongPress
}
public class Player_KeyTrigger : PlayerFSMBaseTrigger
{
    public EKey key;
    public EKeyTriggerMode triggerMode;
    [ShowIf("isCurrentModeIsLongPress")]
    public float longPressThreshold = 1f;

    public bool isCurrentModeIsLongPress() { return triggerMode == EKeyTriggerMode.LongPress; }
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if (triggerMode == EKeyTriggerMode.Down && InputManager.Keys[key].isKeyDown)
            return true;
        if (triggerMode == EKeyTriggerMode.Pressing && InputManager.Keys[key].isKeyPressing)
            return true;
        if (triggerMode == EKeyTriggerMode.Up && InputManager.Keys[key].isKeyUp)
            return true;
        if (triggerMode == EKeyTriggerMode.LongPress && InputManager.Keys[key].isKeyPressing && InputManager.Keys[key].pressDuration > longPressThreshold)
            return true;
        return false;
    }
}

public class Player_CommandTrigger : PlayerFSMBaseTrigger
{
    public ECommand command;
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if (InputManager.Commands[command].Check())
            return true;
        else
            return false;
    }
}

public class Player_DashTrigger : Player_KeyTrigger
{
    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if (base.IsTriggerReachInUpdate(playerFSM) && playerFSM.isAllowDash)
        {
            return true;
        }
        return false;
    }
}
