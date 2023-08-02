using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementClock_AttachedBaseState : MapObjectFSMBaseState
{
    public Color _color;
    protected SpriteRenderer spr;
    public override void InitState(MapObjectFSM mapObjectFSM)
    {
        base.InitState(mapObjectFSM);
        spr = mapObjectFSM.GetComponentInParent<SpriteRenderer>();
    }
    public override void EnterState(MapObjectFSM mapObjectFSM)
    {
        base.EnterState(mapObjectFSM);
        spr.color =_color;
    }
}
