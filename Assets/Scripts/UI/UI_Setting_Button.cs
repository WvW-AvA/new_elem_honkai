using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
public class UI_Setting_Button : Button
{
    public UI_SelectPointer pointer;
    protected override void Start()
    {
        var go = transform.Find("SelectPointer");
        pointer = go.GetComponent<UI_SelectPointer>();
        pointer.gameObject.SetActive(false);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        pointer.gameObject.SetActive(true);
        pointer.IsEnable = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        pointer.IsEnable = false;
    }
}
