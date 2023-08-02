using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideShow : Interactible
{
    public GuideUIParam param;
    protected override void OnPlayerTouch()
    {
        UIManager.CreateGuideUI(param);
    }

    protected override void OnPlayerExit()
    {
        gameObject.SetActive(false);
    }
}
