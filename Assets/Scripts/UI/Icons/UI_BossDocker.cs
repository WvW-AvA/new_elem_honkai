using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;

public class UI_BossDocker : UI_Icon
{
    public Image hpBarLayer1;
    public float hpBarLayer1ChangeSpeed;
    public Image hpBarLayer2;
    public float hpBarLayer2ChangeSpeed;
    public Boss targetBoss;

    public UI_ElementAttach_Icon ElementAttach_Icon;
    private float layer1LastHP;
    private float layer2LastHP;
    //0:not change
    //1:1st layer decrease
    //2:2nd layer decrease
    private int hpStateFlag;
    private float m_hpBarLayer1ChangeSpeed;
    private float m_hpBarLayer2ChangeSpeed;
    public void SetTargetBoss(Boss target)
    {
        targetBoss = target;
        layer1LastHP = target.maxHp;
        layer2LastHP = target.maxHp;
        ElementAttach_Icon.target = target;
        m_hpBarLayer1ChangeSpeed = hpBarLayer1ChangeSpeed * target.maxHp;
        m_hpBarLayer2ChangeSpeed = hpBarLayer2ChangeSpeed * target.maxHp;
    }
    public override void Update()
    {
        base.Update();

        if (hpStateFlag == 0)
        {
            if ((int)layer1LastHP == targetBoss.currentHp && targetBoss.currentHp == (int)layer2LastHP)
                return;
            hpStateFlag = 1;
        }

        if (hpStateFlag == 1)
        {
            if ((int)layer1LastHP != targetBoss.currentHp)
            {
                layer1LastHP += (Mathf.Sign(targetBoss.currentHp - layer1LastHP) * Time.deltaTime * m_hpBarLayer1ChangeSpeed);
                hpBarLayer1.fillAmount = layer1LastHP / targetBoss.maxHp;
            }
            else
            {
                hpStateFlag = 2;
            }
        }
        else if (hpStateFlag == 2)
        {
            if ((int)layer1LastHP != targetBoss.currentHp)
                hpStateFlag = 1;

            if ((int)layer2LastHP != targetBoss.currentHp)
            {
                layer2LastHP += (Mathf.Sign(targetBoss.currentHp - layer2LastHP) * Time.deltaTime * m_hpBarLayer2ChangeSpeed);
                hpBarLayer2.fillAmount = layer2LastHP / targetBoss.maxHp;
            }
            else
            {
                hpStateFlag = 0;
            }
        }

    }
}
