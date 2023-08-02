using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
public class ElementSelectDamager : Damager
{
    public List<EElement> invokeElement;

    private bool CheckElementRequired(GameObject target)
    {
        var e = Element.GetElement(target);
        foreach (var ee in invokeElement)
            if (ee == e)
                return true;
        return false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckElementRequired(collision.gameObject) == false)
            return;
        base.OnCollisionEnter2D(collision);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckElementRequired(collision.gameObject) == false)
            return;
        base.OnTriggerEnter2D(collision);
    }
}
