using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : ElementClockEffect
{
    protected GameObject child;
    protected override void Start()
    {
        base.Start();
        child = transform.Find("Square").gameObject;
        col.size = new Vector2(squre.transform.localScale.x - 0.2f, 0.01f);
    }
    protected virtual void WallUpdate()
    {

    }
}
