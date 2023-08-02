using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScreen : MonoBehaviour
{
    public bool isOn;
    private SpriteRenderer spr;
    void Start()
    {
        spr=gameObject.GetComponentInParent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(spr!=null)
            if(col.tag=="Player")
                spr.enabled = isOn;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (spr != null)
            if (col.tag == "Player")
                spr.enabled = !isOn;
    }
}
