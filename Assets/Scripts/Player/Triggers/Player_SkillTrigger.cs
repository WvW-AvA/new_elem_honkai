using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Player_SkillTrigger : PlayerFSMBaseTrigger
{
    public EKey key;
    public EElement element;

    public override bool IsTriggerReachInUpdate(PlayerFSM playerFSM)
    {
        if (InputManager.Keys[key].isKeyDown)
        {
            if (key == EKey.Q && Player.instance.parameterDict[element].Q_Skill_IsEnable)
            {
                return true;
            }
            if (key == EKey.E && Player.instance.parameterDict[element].E_Skill_IsEnable)
            {
                return true;
            }
        }
        return false;
    }
}
