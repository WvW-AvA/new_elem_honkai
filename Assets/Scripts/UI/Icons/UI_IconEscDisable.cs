using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UI_IconEscDisable : UI_Icon
{
    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        GameManager.ChangeGameMode(GameMode.EGameMode.DialogMode);
    }
    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (Input.anyKeyDown)
            IsEnable = false;
    }
    protected override void OnUIDisable()
    {
        base.OnUIDisable();
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
    }
}
