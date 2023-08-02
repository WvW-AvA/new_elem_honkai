using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementClock_None_State : MapObjectFSMBaseState
{
    public EElement quickChangeElement;
    private SpriteRenderer spr;
    private ElementClock EC;
    public override void InitState(MapObjectFSM mapObjectFSM)
    {
        base.InitState(mapObjectFSM);
        spr = mapObjectFSM.GetComponentInParent<SpriteRenderer>();
        EC = mapObjectFSM.GetComponentInParent<ElementClock>();
    }
    public override void EnterState(MapObjectFSM mapObjectFSM)
    {
        base.EnterState(mapObjectFSM);
        if(quickChangeElement!=EElement.NONE)
        {
            EC.attachedElement = quickChangeElement;
            return;
        }
        spr.color = new Color(232f/255f,251/255f,255/255f);
    }
    public override void Act_State(MapObjectFSM mapObjectFSM)
    {
        base.Act_State(mapObjectFSM);
        if (quickChangeElement != EElement.NONE)
        {
            EC.attachedElement = quickChangeElement;
            return;
        }
    }
}
