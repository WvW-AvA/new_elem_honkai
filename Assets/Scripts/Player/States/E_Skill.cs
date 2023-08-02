using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Animancer;
public class E_Skill : SkillBase
{
    public override void TimeoutCallBack()
    {
        base.TimeoutCallBack();
        //暂不开启e技能
        // Player.instance.parameterDict[element].E_Skill_IsEnable = true;
    }
    public override void TimerTickCallBack(float duration)
    {
        if (element != Player.instance.curr.element)
            return;
        base.TimerTickCallBack(duration);
        UIManager.GetUIInstance<UI_Playing>(UIConst.PlayingUI).ESkill_Icon.CDTimer = duration;
    }
    public override void TimerReloadCallBack()
    {
        base.TimerReloadCallBack();
        Player.instance.parameterDict[element].E_Skill_IsEnable = false;
    }
}

public class Fire_E_Skill : E_Skill
{
    public SfxSO sfx;
    public Vector2 offset;
    public float fireEnchantmentTime;

    private TimerNode timer;
    public override void InitState(PlayerFSM playerFSM)
    {
        base.InitState(playerFSM);
        timer = TimerManager.Schedule("Fire_Player_E_Skill_Enchantment", fireEnchantmentTime, 0, false,
            () =>
            {
                Player.instance.curr.isFireEnchantment = false;
            }, null, () =>
            {
                Player.instance.curr.isFireEnchantment = true;
            });
    }
    public override void EnterState(PlayerFSM playerFSM)
    {
        base.EnterState(playerFSM);
        SfxManager.CreateSfx(sfx, offset, playerFSM.gameObject);
        timer.Reload();
    }
}
