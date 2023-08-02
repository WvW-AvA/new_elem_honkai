using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
class UI_SureWindow : UI_Window
{
    public Button sureButton;
    [SerializeField]
    private Text discription;
    public string text { get { return discription.text; } set { discription.text = value; } }
    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        sureButton.onClick.RemoveAllListeners();
    }

    public void AddSureEvent(UnityAction call)
    {
        sureButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        sureButton.onClick.AddListener(call);
    }

}
