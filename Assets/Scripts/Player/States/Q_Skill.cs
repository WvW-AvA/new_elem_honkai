using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Animancer;
public class Q_Skill : SkillBase
{
    public int powerConsume;
    private int m_currentPower;
    public int currentPower
    {
        get { return m_currentPower; }
        set
        {
            if (value >= powerConsume)
                m_currentPower = powerConsume;
            else
                m_currentPower = value;
        }
    }
    public override void TimeoutCallBack()
    {
        base.TimeoutCallBack();
        if (currentPower == powerConsume)
            Player.instance.parameterDict[element].Q_Skill_IsEnable = true;
    }
    public override void TimerTickCallBack(float duration)
    {
        if (element != Player.instance.curr.element)
            return;
        base.TimerTickCallBack(duration);
        UIManager.GetUIInstance<UI_Playing>(UIConst.PlayingUI).QSkill_Icon.CDTimer = duration;
    }

}
