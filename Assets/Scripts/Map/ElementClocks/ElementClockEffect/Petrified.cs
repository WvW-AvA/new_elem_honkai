using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petrified : WallEffect
{
    public float bornTime;
    public float height;
    private float timer;
    private float offset;
    private void OnEnable()
    {
        timer = 0;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer<=bornTime)
        offset = height * (timer / bornTime);
        WallUpdate();
    }
    private void OnDisable()
    {
        offset = 0;
        WallUpdate();
    }
    protected override void WallUpdate()
    {
        col.offset = new Vector2(col.offset.x, offset + SqureOffset());
        col.size = new Vector2(squre.transform.localScale.x - 0.2f, offset * 2);
        child.transform.localScale = col.size;
        child.transform.localPosition = col.offset;
    }
}
