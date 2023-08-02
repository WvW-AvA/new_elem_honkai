using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EDamageBaseOn
{
    SelfDamage,
    SelfHP,
    TargetHP,
    constValue
}

public struct DamageToken
{
    public GameObject Giver;
    public EElement hittedElement;
    public EElement hitElement;
    public int baseDam;
    public float rate;

    public int Calculate()
    {
        float eleRate = Element.ElementRate(hitElement);
        float eleResistRate = Element.GetElementResistRate(hitElement, hittedElement);
        return (int)(baseDam * eleRate * eleResistRate * rate);
    }
}
