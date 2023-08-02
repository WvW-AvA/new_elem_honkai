using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareColor : MonoBehaviour
{
    public SpriteRenderer spr;
    private SpriteRenderer spr2;
    private void Awake()
    {
        spr2 = gameObject.GetComponentInParent<SpriteRenderer>();
    }
    private void Update()
    {
        spr.color = spr2.color;
    }

}
