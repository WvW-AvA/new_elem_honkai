using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowSprite : MonoBehaviour
{
    Material m;
    SpriteRenderer spriteRenderer;

    public bool ifInterect;

    Vector2 currentPos, lastPos;

    float interectIntervel;
    float lastInterectTime;
    float interectTime;

    Vector3 dir;
    float scale;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        m = GetComponent<SpriteRenderer>().material;
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPos = transform.worldToLocalMatrix * Vector3.zero;
        interectIntervel = 3f;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        //InterectWithMouse();
        onMouseEnter();
        m.SetFloat("Time", Time.time);
        m.SetVector("MoveDir", dir);
        m.SetFloat("MoveScale", scale);
        m.SetVector("MovePosition", currentPos);
        m.SetFloat("LastTime", interectTime);

    }

    /*void InterectWithMouse()
    {
        if (!ifInterect) return;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!boxCollider2D.bounds.Contains(mouseWorldPos)) return;

        currentPos = mouseWorldPos;
        if ((lastPos - currentPos).magnitude <= 0.1f) return;
        if (Time.time - lastInterectTime < interectIntervel) return;
        lastInterectTime = Time.time;
        interectTime = lastInterectTime;
        dir = (currentPos - lastPos).normalized;
        scale = Smoothstep(0.0f,2.0f, (currentPos - lastPos).magnitude) *3.0f;
        lastPos = currentPos;


    }*/

    float Smoothstep(float a, float b, float t)
    {
        float k = Mathf.Clamp01((t - a) / (b - a));
        return k * k * (3.0f - 2.0f * k);
    }
    private void onMouseEnter()
    {
        if (Time.time - lastInterectTime < interectIntervel) return;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastPos = currentPos;
        currentPos = mouseWorldPos;

        if (boxCollider2D.bounds.Contains(currentPos) && !boxCollider2D.bounds.Contains(lastPos))
        {
            dir = (currentPos - lastPos).normalized;
            scale = Smoothstep(0.0f, 3.0f, (currentPos - lastPos).magnitude) * 3.0f;
            lastInterectTime = Time.time;
            interectTime = lastInterectTime;
        }


    }
}
