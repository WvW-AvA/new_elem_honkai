using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    public float gravityChangeRate;
    public GravityChangeBuff GCB = new GravityChangeBuff();
    private void Awake()
    {
        GCB.buffName = "Steam_GravityChangeBuff";
        GCB.gravityChangeRate = gravityChangeRate;
        GCB.DuringTime = 10000f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BuffManager.AddBuff(GCB, this.gameObject, collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BuffManager.RemoveBuff(GCB, collision.gameObject);
        }
    }
}
