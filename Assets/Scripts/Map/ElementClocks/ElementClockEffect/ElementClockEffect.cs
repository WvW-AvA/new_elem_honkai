using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementClockEffect : MonoBehaviour
{
    protected GameObject squre;
    protected BoxCollider2D col;
    protected virtual void Start()
    {
        squre = transform.parent.Find("Square").gameObject;
        col = GetComponent<BoxCollider2D>();
    }
    protected float SqureOffset()
    {
        //Debug.Log(squre.transform.localPosition.y + " " + squre.transform.localScale.y / 2);
        return squre.transform.localPosition.y + squre.transform.localScale.y / 2;
    }
}
