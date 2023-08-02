using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
/// <summary>
/// 功能：若集合中的所有键都没按下则触发trigger返回true,否则返回false
/// </summary>
public class Player_NoKeyTrigger : PlayerFSMBaseTrigger
{
    [OdinSerialize]
    public List<EKey> detectKeys;

    public override void InitTrigger(PlayerFSM playerFSM, PlayerFSMBaseState state)
    {
        base.InitTrigger(playerFSM, state);
        // detectKeys = (List<EKey>)detectKeys.GroupBy(x => x).Select(y => y.First());
    }

    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        foreach (var k in detectKeys)
        {
            if (InputManager.Keys[k].isKeyDown == true || InputManager.Keys[k].isKeyPressing == true
                || InputManager.Keys[k].isKeyUp == true)
                return false;
        }
        return true;
    }

}
