using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class UI_Window : UI_Base
{
    [ReadOnly]
    public string windowName = "Window";

    public Button CloseButton;
    protected override void OnUIInit()
    {
        base.OnUIInit();
        CloseButton.onClick.AddListener(() => { CloseWindow(); });
    }
    public void CloseWindow()
    {
        ResourceManager.Release(gameObject);
    }

    private void OnEnable()
    {
        base.OnUIEnable();
        UIManager.PushBackWindow(this);
    }

    private void OnDisable()
    {
        base.OnUIDisable();
        UIManager.PopBackWindow();
    }

}
