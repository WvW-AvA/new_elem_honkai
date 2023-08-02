using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class ElementClock_ReactionBaseState : ElementClock_AttachedBaseState
{
    protected string _name;
    protected GameObject effectGO;
    public override void InitState(MapObjectFSM mapObjectFSM)
    {
        base.InitState(mapObjectFSM);
        Type t = this.GetType();
        _name = t.Name.Split('_')[1];
        if (mapObjectFSM.transform.Find(_name) != null)
            effectGO = mapObjectFSM.transform.Find(_name).gameObject;
    }
    public override void EnterState(MapObjectFSM mapObjectFSM)
    {
        base.EnterState(mapObjectFSM);
        effectGO.SetActive(true);
    }
    public override bool ExitState(MapObjectFSM mapObjectFSM)
    {
        effectGO.SetActive(false);
        return base.ExitState(mapObjectFSM);
    }
}
