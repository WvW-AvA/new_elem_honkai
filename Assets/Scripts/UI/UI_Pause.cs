using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UI_Pause : UI_Base
{
    public float delayTime;
    public UI_Icon background;
    public UI_Icon title;
    public UI_Icon keysTips;

    public UI_Setting rightBox;
    public UI_Setting middleBox;
    public UI_Setting leftBox;

    private float timer;
    private UI_Setting currrentSelectedSetting;
    protected override void OnUIInit()
    {
        base.OnUIInit();
    }
    protected override void OnUIEnable()
    {
        background.IsEnable = true;
        title.IsEnable = true;
        keysTips.IsEnable = true;

        SelectUISetting(rightBox);
        base.OnUIEnable();
        timer = 0;
    }

    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        timer += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.K)) && timer > delayTime)
            IsEnable = false;
    }
    protected override void OnUIDisable()
    {
        title.IsEnable = false;
        rightBox.IsEnable = false;
        keysTips.IsEnable = false;
        middleBox.IsEnable = false;
        leftBox.IsEnable = false;
        background.IsEnable = false;
        InputManager.SetPlayerBehaviousActive(true);
        UIManager.playingUI.IsEnable = true;
        base.OnUIDisable();
    }

    void SelectUISetting(UI_Setting ui)
    {
        if (currrentSelectedSetting != null)
            currrentSelectedSetting.isSelected = false;
        currrentSelectedSetting = ui;
        currrentSelectedSetting.IsEnable = true;
        currrentSelectedSetting.isSelected = true;
    }
    public void ReturnMainMenu()
    {
        SaveSystem.Save();
        GameMode.BeginMenuMode.isReleaseAll = true;
        GameManager.ChangeGameMode(GameMode.EGameMode.BeginMenuMode);
    }
    public void OpenSetting()
    {
        middleBox.gameObject.SetActive(true);
        leftBox.gameObject.SetActive(true);
        SelectUISetting(middleBox);
    }
    public void OpenNote()
    {

    }
}

