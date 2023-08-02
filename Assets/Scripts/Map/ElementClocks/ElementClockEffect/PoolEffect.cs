using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEffect : ElementClockEffect
{
    protected GameObject child;
    protected override void Start()
    {
        base.Start();
        child = transform.Find("Square").gameObject;
        col.size = new Vector2(squre.transform.localScale.x - 0.2f, squre.transform.localScale.y / 2f);
        col.offset = new Vector2(col.offset.x,SqureOffset());
        child.transform.localScale = col.size;
        child.transform.localPosition = col.offset;
    }
}
