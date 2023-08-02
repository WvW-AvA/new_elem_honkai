using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
public class UI_SelectPointer : UI_Base
{
    public UI_Icon pointer;
    public UI_Icon cycle;

    protected override void OnUIInit()
    {
        if (pointer != null)
            pointer.onEffectDoneEvent.AddListener(() => { gameObject.SetActive(false); });
        else if (cycle != null)
            cycle.onEffectDoneEvent.AddListener(() => { gameObject.SetActive(false); });
        base.OnUIInit();
    }
    protected override void OnUIEnable()
    {
        if (pointer != null)
            pointer.IsEnable = true;
        if (cycle != null)
            cycle.IsEnable = true;
        base.OnUIEnable();
    }

    protected override void OnUIDisable()
    {
        if (pointer != null)
            pointer.IsEnable = false;
        if (cycle != null)
            cycle.IsEnable = false;
        base.OnUIDisable();
    }
}
