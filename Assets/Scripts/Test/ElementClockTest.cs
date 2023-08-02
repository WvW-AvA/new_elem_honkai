using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;

[Serializable]
public class ElementClockTest : MonoBehaviour
{
    private ElementClock go;
    private DamageToken damage;
    public EElement element;
    public bool flag=false;
    private void Awake()
    {
        go = gameObject.GetComponent<ElementClock>();
    }
    private void FixedUpdate()
    {
        damage.hitElement = element;
        if(flag)
        {
            go.GetHurt(damage);
            flag = false;
        }
    }
}
