using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UI_Setting : UI_Icon
{
    private bool m_isSelected = false;
    public bool isSelected
    {
        get { return m_isSelected; }
        set
        {
            m_isSelected = value;
        }
    }
    public UI_Setting_Button defaultPointer;
    public float ponterShowDelayTime = 0.6f;
    private UI_Setting_Button currentPointer;
    private List<UI_Setting_Button> m_buttons;
    private List<UI_Setting_Button> buttons
    {
        get
        {
            if (m_buttons == null)
                m_buttons = new List<UI_Setting_Button>(GetComponentsInChildren<UI_Setting_Button>());
            return m_buttons;
        }
    }
    protected override void OnUIInit()
    {
        base.OnUIInit();
    }
    protected override void OnUIEnable()
    {
        base.OnUIEnable();
        foreach (var b in buttons)
            b.interactable = true;
        isSelected = false;
        //ChangeCurrentPointer(defaultPointer, ponterShowDelayTime);
    }
    protected override void OnUIDisable()
    {
        foreach (var b in buttons)
            b.interactable = false;
        QuickCoroutineSystem.StartCoroutine(OnUIDisable_co(0.2f));
    }
    IEnumerator OnUIDisable_co(float delayTime)
    {
        if (currentPointer != null)
            currentPointer.OnPointerExit(new PointerEventData(UIManager.eventSystem));
        isSelected = false;
        yield return new WaitForSeconds(delayTime);
        base.OnUIDisable();
    }

    protected override void OnUIUpdate()
    {
        base.OnUIUpdate();
        if (isSelected == false)
            return;
        //   if (Input.GetKeyDown(KeyCode.W))
        //   {
        //       ChangeCurrentPointer(currentPointer.FindSelectableOnUp());
        //   }
        //   else if (Input.GetKeyDown(KeyCode.S))
        //   {
        //       ChangeCurrentPointer(currentPointer.FindSelectableOnDown());
        //   }
        //   else if (Input.GetKeyDown(KeyCode.J))
        //   {
        //       currentPointer.OnPointerDown(new PointerEventData(UIManager.eventSystem));
        //       currentPointer.onClick.Invoke();
        //   }
        //   else if (Input.GetKeyUp(KeyCode.J))
        //   {
        //       currentPointer.OnPointerUp(new PointerEventData(UIManager.eventSystem));
        //   }
    }
    private void ChangeCurrentPointer(Selectable target, float delayTime)
    {
        if (delayTime == 0)
            ChangeCurrentPointer(target);
        else
            QuickCoroutineSystem.StartCoroutine(ChangeCurrentPointer_co(target, delayTime));
    }
    IEnumerator ChangeCurrentPointer_co(Selectable target, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ChangeCurrentPointer(target);
    }
    private void ChangeCurrentPointer(Selectable target)
    {
        if (target == null)
            return;
        if (currentPointer != null)
            currentPointer.OnPointerExit(new PointerEventData(UIManager.eventSystem));
        currentPointer = target as UI_Setting_Button;
        currentPointer.OnPointerEnter(new PointerEventData(UIManager.eventSystem));
    }
}
