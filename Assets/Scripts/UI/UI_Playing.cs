using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UI_Playing : UI_Base
{
    public UI_ESkill_Icon ESkill_Icon;
    public UI_QSkill_Icon QSkill_Icon;
    public UI_PieChart_Icon Fire_Energy_Icon;
    public UI_PieChart_Icon Water_Energy_Icon;
    public UI_PieChart_Icon Terrain_Energy_Icon;
    public UI_PlayerDocker playerDocker;

    protected override void OnUIInit()
    {
        base.OnUIInit();
    }

    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        ESkill_Icon.IsEnable = true;
        QSkill_Icon.IsEnable = true;
        Fire_Energy_Icon.IsEnable = true;
        Water_Energy_Icon.IsEnable = true;
        Terrain_Energy_Icon.IsEnable = true;
        playerDocker.IsEnable = true;
    }

    protected override void OnUIDisable()
    {
        ESkill_Icon.IsEnable = false;
        QSkill_Icon.IsEnable = false;
        Fire_Energy_Icon.IsEnable = false;
        Water_Energy_Icon.IsEnable = false;
        Terrain_Energy_Icon.IsEnable = false;
        playerDocker.IsEnable = false;
        base.OnUIDisable();
    }


}
