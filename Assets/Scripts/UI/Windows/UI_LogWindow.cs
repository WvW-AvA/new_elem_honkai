using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class UI_LogWindow : UI_Window
{
    public Text text;
    public Image background;
    private bool isActive = true;
    protected override void OnUIInit()
    {
        IsEnable = true;
        CloseButton.onClick.AddListener(() => { HideWindow(); });

    }
    public void HideWindow()
    {
        transform.SetAsLastSibling();
        isActive = !isActive;
        background.enabled = isActive;
        text.enabled = isActive;
    }
    public void Clear()
    {
        text.text = "";
    }

    public void WriteLine(string str)
    {
        text.text += str + "\n";
    }

}
