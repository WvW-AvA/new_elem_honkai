using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRequest : MonoBehaviour
{
    public enum Need
    {
        Position,
        Trigger,
        Count
    }
    public enum Check
    {
        Actual,
        Recorded
    }
    [SerializeField]
    [EnumToggleButtons]
    [Tooltip("What do you need?")]
    public Need need;

    public Check check;

    //[HideInInspector]
    public bool complete;

    [ShowIf("need",Need.Position)]
    public float radius;

    [ShowIf("need", Need.Position)]
    public Transform tra;

    private Collider2D col;

    public void Update()
    {
        if(need==Need.Position)
        {
            if(check==Check.Actual)
            {
                col=Physics2D.OverlapCircle(tra.position,radius,LayerMask.NameToLayer("Player"));
                if(col!=null) complete=true;
                else complete=false;

            }
            else
            {
                if (complete) return;
                col = Physics2D.OverlapCircle(tra.position, radius, LayerMask.NameToLayer("Player"));
                if (col != null) complete = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(need==Need.Trigger)
        {
            if(collision!=null)
            if(collision.tag=="Player")
            complete=true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(need==Need.Trigger)
        {
            if(check==Check.Actual)
            if(collision!=null)
            if(collision.tag=="Player")
            {
                complete=false;
            }
        }
    }
}
